using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    만 든 날 : 2018-04-02
    작 성 자 : 전민수

    베지어곡선을 이용한 노드를 사용하여 오브젝트 이동    
*/

[RequireComponent(typeof(DragonManager))]
public class MoveManager : MonoBehaviour {

    public List<NodeManager> NodesManager;

    private DragonManager _dragon;
    public DragonManager Dragon { get { return _dragon; } }

    private void Awake()
    {
        _dragon = GetComponent<DragonManager>();
    }

    /* 노드위치를 용에게 */
    public void Stick(int Index)
    {
        if (NodesManager[Index].IsDragonStick)
        { 
            Vector3 forward = Dragon.transform.position - NodesManager[Index].transform.position;
            NodesManager[Index].transform.rotation = Quaternion.LookRotation(forward);
            NodesManager[Index].transform.position = Dragon.transform.position;
        }
        else
        {
            Vector3 forward = (NodesManager[Index].transform.position - Dragon.transform.position).normalized;

            Dragon.transform.rotation =
                Quaternion.Lerp(
                    Dragon.transform.rotation,
                    Quaternion.LookRotation(forward),
                    Dragon.Stat.TurnSpeed * Time.deltaTime);
        }

    } 

    /* 중심축으로 회전 */
    public void AxisRotation(int Index)
    {
        if (NodesManager[Index].IsRotation)
        {
            Vector3 forward = (Dragon.transform.position - NodesManager[Index].transform.position).normalized;
            float dot = Vector3.Dot(forward, Vector3.forward);
            float Theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

            NodesManager[Index].transform.Rotate(0.0f, Theta, 0.0f);
        }
    }

    /* 이동 반복 */
    public void MovementLoop(int Index)
    {
        if (NodesManager[Index].IsMoveLoop)
        {
            if (NodesManager[Index].IsMoveEnd)
            {
                NodesManager[Index].CurNodesIndex = 0;
            }
        }
    }

    //노드 포지션 및 로테이션 셋팅
    public void MoveMentReady(int Index)
    {
        Stick(Index);
        AxisRotation(Index);

        NodesManager[Index].AllNodesCalc();

        int NodesCount = NodesManager[Index].NodesSpeed.Count;

        for (int nodeIndex = 0; nodeIndex < NodesCount; nodeIndex++)
        {
            NodesManager[Index].Stat.NodeDir.Add(NodesManager[Index].NodesDir[nodeIndex]);
            NodesManager[Index].Stat.NodeSpeed.Add(NodesManager[Index].NodesSpeed[nodeIndex]);
            NodesManager[Index].Stat.NodeRot.Add(NodesManager[Index].NodesRot[nodeIndex]);
        }
        NodesManager[Index].IsMoveReady = true;

    }

    //노드를 따라서 이동 및 회전
    public void NodeMovement(int Index)
    {
        int NodesCount = NodesManager[Index].NodesSpeed.Count;
        int NodesIndex = NodesManager[Index].CurNodesIndex;

        if (NodesIndex < NodesCount)
        {
            NodesManager[Index].IsMoveEnd = false;

            float moveDistance = NodesManager[Index].Stat.NodeSpeed[NodesIndex] * Time.deltaTime;
            float nextDistance = Vector3.Distance(NodesManager[Index].Stat.NodeDir[NodesIndex], Dragon.transform.position);

            Vector3 dir = (NodesManager[Index].Stat.NodeDir[NodesIndex] - Dragon.transform.position).normalized;
            Vector3 eulerAngle = Vector3.zero;

            if (NodesIndex + 1 < NodesCount)
            {
                eulerAngle = (NodesManager[Index].NodesRot[NodesIndex] - NodesManager[Index].Stat.NodeRot[NodesIndex + 1]);
            }


            for (; moveDistance > nextDistance;)
            {

                Dragon.transform.position += dir * nextDistance;

                moveDistance -= nextDistance;

                NodesManager[Index].CurNodesIndex++;

                NodesIndex = NodesManager[Index].CurNodesIndex;

                if (NodesIndex + 1 >= NodesCount)
                    return;
                dir = (NodesManager[Index].Stat.NodeDir[NodesIndex + 1] - Dragon.transform.position).normalized;

                eulerAngle = 
                    (NodesManager[Index].Stat.NodeRot[NodesIndex + 1] - NodesManager[Index].Stat.NodeRot[NodesIndex]);

                nextDistance = Vector3.Distance(NodesManager[Index].Stat.NodeDir[NodesIndex + 1],
                    Dragon.transform.position);

            }

            if (NodesManager[Index].CenterAxisRot != null)
            {
                Vector3 CentralAxis = (NodesManager[Index].CenterAxisRot.position - Dragon.transform.position).normalized;
                
                Dragon.transform.rotation =
                    Quaternion. Slerp(
                        Dragon.transform.rotation,
                        Quaternion.LookRotation(dir, CentralAxis),
                        45.0f * Time.fixedDeltaTime);
            }
            else
            { 
                Quaternion rot = (Quaternion.LookRotation(dir, Dragon.transform.up) 
                    * Quaternion.Euler(eulerAngle));

                //Debug.Log(rot);

                //Quaternion rot = Quaternion.LookRotation(dir, Dragon.transform.up) 
                //    * Quaternion.Euler(eulerAngle);

                Dragon.transform.rotation = rot;
            }

            Dragon.transform.position += dir * NodesManager[Index].Stat.NodeSpeed[NodesIndex] * Time.deltaTime;

            if (NodesIndex + 1 >= NodesCount)
                return;
            dir = (NodesManager[Index].Stat.NodeDir[NodesIndex] - Dragon.transform.position).normalized;

        }
        else
        {
            NodesManager[Index].IsMoveEnd = true;
            MovementLoop(Index);
        }
    }

}
