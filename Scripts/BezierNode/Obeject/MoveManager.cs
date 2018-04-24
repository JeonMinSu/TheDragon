using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    만 든 날 : 2018-04-02 - 01:00
    작 성 자 : 전민수

    베지어곡선을 이용한 노드를 사용하여 오브젝트 이동    
*/

public class MoveManager : MonoBehaviour {

    public List<NodeManager> Manager;

    public GameObject MoveObject;
    
    //private int _nodesCount = 0;

    public void DragonStick(int Index)
    {
        if (Manager[Index].IsDragonStick)
        { 
            Vector3 forward = Manager[Index].transform.position - MoveObject.transform.position;
            Manager[Index].transform.rotation = Quaternion.LookRotation(forward);
            Manager[Index].transform.position = MoveObject.transform.position;
        }
        else
        {
            Vector3 forward = (Manager[Index].transform.position - MoveObject.transform.position).normalized;

            MoveObject.transform.rotation =
                Quaternion.RotateTowards(
                    MoveObject.transform.rotation,
                    Quaternion.LookRotation(forward),
                    360.0f * Time.fixedDeltaTime);

            MoveObject.transform.position =
                Vector3.MoveTowards(
                    MoveObject.transform.position,
                    Manager[Index].transform.position,
                    50.0f * Time.deltaTime);
        }

    }

    public void AxisRotation(int Index)
    {
        if (Manager[Index].IsAxisRot)
        {
            Vector3 forward = (MoveObject.transform.position - Manager[Index].transform.position).normalized;
            float dot = Vector3.Dot(forward, Vector3.forward);
            float Theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

            Manager[Index].transform.localRotation = Quaternion.Euler(0.0f, Theta, 0.0f);
        }
    }

    public void MovementLoop(int Index)
    {
        if (Manager[Index].IsMoveLoop)
        {
            if (Manager[Index].IsMoveEnd)
            {
                Manager[Index].CurNodesIndex = 0;
            }
        }
    }

    //계산 함수
    public void MoveMentReady(int Index)
    {
        DragonStick(Index);
        AxisRotation(Index);

        Manager[Index].AllNodesCalc();

        int NodesCount = Manager[Index].NodesSpeed.Count;
        
        for (int nodeIndex = 0; nodeIndex < NodesCount; nodeIndex++)
        {
            Manager[Index].Stat.NodeDir.Add(Manager[Index].NodesDir[nodeIndex]);
            Manager[Index].Stat.NodeSpeed.Add(Manager[Index].NodesSpeed[nodeIndex]);
            Manager[Index].Stat.NodeRot.Add(Manager[Index].NodesRot[nodeIndex]);
        }
        Manager[Index].IsMoveReady = true;

    }

    //노드를 따라서 이동
    public void NodeMovement(int Index)
    {
        int NodesCount = Manager[Index].NodesSpeed.Count;
        int NodesIndex = Manager[Index].CurNodesIndex;

        if (NodesIndex < NodesCount)
        {
            Manager[Index].IsMoveEnd = false;

            float moveDistance = Manager[Index].Stat.NodeSpeed[NodesIndex] * Time.deltaTime;
            float nextDistance = Vector3.Distance(Manager[Index].Stat.NodeDir[NodesIndex], MoveObject.transform.position);

            Vector3 dir = (Manager[Index].Stat.NodeDir[NodesIndex] - MoveObject.transform.position).normalized;
            
            Quaternion rotation = (Manager[Index].NodesRot[NodesIndex] *
                MoveObject.transform.rotation);

            for (; moveDistance > nextDistance;)
            {

                MoveObject.transform.position += dir * nextDistance;

                moveDistance -= nextDistance;

                Manager[Index].CurNodesIndex++;

                NodesIndex = Manager[Index].CurNodesIndex;

                if (NodesIndex + 1 >= NodesCount)
                    return;
                dir = (Manager[Index].Stat.NodeDir[NodesIndex + 1] - MoveObject.transform.position).normalized;
                rotation = (Manager[Index].Stat.NodeRot[NodesIndex] * Manager[Index].Stat.NodeRot[NodesIndex + 1]);

                nextDistance = Vector3.Distance(Manager[Index].Stat.NodeDir[NodesIndex + 1],
                    MoveObject.transform.position);

            }

            Vector3 forward = dir - MoveObject.transform.position;

            if (Manager[Index].CenterAxis != null)
            {
                Vector3 CentralAxis = (Manager[Index].CenterAxis.position - MoveObject.transform.position).normalized;

                MoveObject.transform.rotation =
                    Quaternion.RotateTowards(
                        MoveObject.transform.rotation,
                        Quaternion.LookRotation(dir, CentralAxis),
                        360.0f * Time.fixedDeltaTime);

            }
            else
            {
                MoveObject.transform.rotation =
                    Quaternion.RotateTowards(
                        MoveObject.transform.rotation,
                        rotation,
                        360.0f * Time.fixedDeltaTime);
            }

            MoveObject.transform.position += dir * Manager[Index].Stat.NodeSpeed[NodesIndex] * Time.deltaTime;


            if (NodesIndex + 1 >= NodesCount)
                return;
            dir = (Manager[Index].Stat.NodeDir[NodesIndex] - MoveObject.transform.position).normalized;

        }
        else
        {
            Manager[Index].IsMoveEnd = true;
            MovementLoop(Index);
        }
    }
}
