using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    만 든 날 : 2018-04-02 - 01:00
    작 성 자 : 전민수

    베지어곡선을 이용한 노드를 사용하여 오브젝트 이동    
*/

public class MoveManager : MonoBehaviour {

    public List<NodeManager> NodeManager;

    public GameObject MoveObject;
    
    private int _nodesIndex = 0;
    //private int _nodesCount = 0;

    private void Awake()
    {

    }

    public void DragonStick(int Index)
    {
        if (NodeManager[Index].IsDragonStick)
        { 
            Vector3 forward = NodeManager[Index].transform.position - MoveObject.transform.position;
            NodeManager[Index].transform.rotation = Quaternion.LookRotation(forward);
            NodeManager[Index].transform.position = MoveObject.transform.position;
        }
        else
        {
            Vector3 forward = (NodeManager[Index].transform.position - MoveObject.transform.position).normalized;

            MoveObject.transform.rotation =
                Quaternion.RotateTowards(
                    MoveObject.transform.rotation,
                    Quaternion.LookRotation(forward),
                    360.0f * Time.fixedDeltaTime);

            MoveObject.transform.position =
                Vector3.MoveTowards(
                    MoveObject.transform.position,
                    NodeManager[Index].transform.position,
                    10.0f * Time.deltaTime);
        }

    }

    //계산 함수
    public void MoveMentReady(int Index)
    {
        DragonStick(Index);

        NodeManager[Index].AllNodesCalc();

        int NodesCount = NodeManager[Index].NodesSpeed.Count;
        
        for (int nodeIndex = 0; nodeIndex < NodesCount; nodeIndex++)
        {
            NodeManager[Index].Stat.NodeDir.Add(NodeManager[Index].NodesDir[nodeIndex]);
            NodeManager[Index].Stat.NodeSpeed.Add(NodeManager[Index].NodesSpeed[nodeIndex]);
            NodeManager[Index].Stat.NodeRot.Add(NodeManager[Index].NodesRot[nodeIndex]);
        }
        NodeManager[Index].IsMoveReady = true;

    }

    private void Update()
    {
    }

    //노드를 따라서 이동
    public void NodeMovement(int Index)
    {
        int NodesCount = NodeManager[Index].NodesSpeed.Count;

        if (_nodesIndex < NodesCount)
        {
            float moveDistance = NodeManager[Index].Stat.NodeSpeed[_nodesIndex] * Time.deltaTime;
            float nextDistance = Vector3.Distance(NodeManager[Index].Stat.NodeDir[_nodesIndex], MoveObject.transform.position);

            Vector3 dir = (NodeManager[Index].Stat.NodeDir[_nodesIndex] - MoveObject.transform.position).normalized;

            for (; moveDistance > nextDistance;)
            {

                MoveObject.transform.position += dir * nextDistance;

                moveDistance -= nextDistance;
                _nodesIndex++;

                if (_nodesIndex + 1 >= NodesCount)
                    return;
                dir = (NodeManager[Index].Stat.NodeDir[_nodesIndex + 1] - MoveObject.transform.position).normalized;
                nextDistance = Vector3.Distance(NodeManager[Index].Stat.NodeDir[_nodesIndex + 1], MoveObject.transform.position);

            }

            Vector3 forward = dir - MoveObject.transform.position;
            MoveObject.transform.rotation =
                Quaternion.RotateTowards(
                    MoveObject.transform.rotation,
                    Quaternion.LookRotation(dir),
                    90.0f * Time.fixedDeltaTime);

            MoveObject.transform.position += dir * NodeManager[Index].Stat.NodeSpeed[_nodesIndex] * Time.deltaTime;


            if (_nodesIndex + 1 >= NodesCount)
                return;
            dir = (NodeManager[Index].Stat.NodeDir[_nodesIndex] - MoveObject.transform.position).normalized;

        }

    }
}
