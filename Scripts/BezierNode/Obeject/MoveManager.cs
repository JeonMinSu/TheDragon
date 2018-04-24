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
    
    private int _nodesIndex = 0;
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

            float MoveSpeed = BlackBoard.Instance.Stat.MaxFlySpeed;

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

    //계산 함수
    public void MoveMentReady(int Index)
    {
        DragonStick(Index);

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

    private void Update()
    {
    }

    //노드를 따라서 이동
    public void NodeMovement(int Index)
    {
        int NodesCount = Manager[Index].NodesSpeed.Count;

        if (_nodesIndex < NodesCount)
        {
            Manager[Index].IsMoveEnd = false;

            float moveDistance = Manager[Index].Stat.NodeSpeed[_nodesIndex] * Time.deltaTime;
            float nextDistance = Vector3.Distance(Manager[Index].Stat.NodeDir[_nodesIndex], MoveObject.transform.position);

            Vector3 dir = (Manager[Index].Stat.NodeDir[_nodesIndex] - MoveObject.transform.position).normalized;
            
            Quaternion rotation = (Manager[Index].NodesRot[_nodesIndex] *
                MoveObject.transform.rotation);

            for (; moveDistance > nextDistance;)
            {

                MoveObject.transform.position += dir * nextDistance;

                moveDistance -= nextDistance;
                _nodesIndex++;

                if (_nodesIndex + 1 >= NodesCount)
                    return;
                dir = (Manager[Index].Stat.NodeDir[_nodesIndex + 1] - MoveObject.transform.position).normalized;
                rotation = (Manager[Index].Stat.NodeRot[_nodesIndex + 1] * MoveObject.transform.rotation);
                nextDistance = Vector3.Distance(Manager[Index].Stat.NodeDir[_nodesIndex + 1], MoveObject.transform.position);

            }

            Vector3 forward = dir - MoveObject.transform.position;


            MoveObject.transform.rotation =
                Quaternion.RotateTowards(
                    MoveObject.transform.rotation,
                    rotation,
                    //Quaternion.LookRotation(dir),
                    //Manager[Index].NodesRot[_nodesIndex],
                    360.0f * Time.fixedDeltaTime);

            MoveObject.transform.position += dir * Manager[Index].Stat.NodeSpeed[_nodesIndex] * Time.deltaTime;


            if (_nodesIndex + 1 >= NodesCount)
                return;
            dir = (Manager[Index].Stat.NodeDir[_nodesIndex] - MoveObject.transform.position).normalized;

        }
        else
        {
            Manager[Index].IsMoveEnd = true;
        }
    }
}
