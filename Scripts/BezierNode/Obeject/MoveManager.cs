using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    만 든 날 : 2018-04-02 - 01:00
    작 성 자 : 전민수

    베지어곡선을 이용한 노드를 사용하여 오브젝트 이동    
*/


[RequireComponent(typeof(MoveStat))]
public class MoveManager : MonoBehaviour {

    private NodeManager _nodeManager;
    public NodeManager NodeManager { get { return _nodeManager; } }

    private DragonManager _manager;
    public DragonManager Manager { get { return _manager; } }

    private MoveStat _stat;
    public MoveStat Stat { get { return _stat; } }

    private int _nodesIndex = 0;
    private int _nodesCount;

    bool _isMoveMentReady = false;


    private void Awake()
    {
        _nodeManager = GameObject.FindWithTag("NodeManager").GetComponent<NodeManager>();
        _manager = GetComponent<DragonManager>();
        _stat = GetComponent<MoveStat>();
    }

    //노드메니저를 돌린다.
    public void IsMoveMentReady()
    {
        Vector3 forward = transform.position - _manager.Player.position;

        NodeManager.transform.rotation = Quaternion.LookRotation(forward);
        NodeManager.transform.position = transform.position;

        _nodeManager.AllNodesCalc();

        _nodesCount = _nodeManager.NodesSpeed.Count;

        for (int nodeIndex = 0; nodeIndex < _nodesCount; nodeIndex++)
        {
            //Stat.NodeRot.Add(_nodeManager.NodesRot[nodeIndex]);
            Stat.NodeDir.Add(_nodeManager.NodesDir[nodeIndex]);
            Stat.NodeSpeed.Add(_nodeManager.NodesSpeed[nodeIndex]);
        }
        _isMoveMentReady = true;

    }

    //노드를 따라서 이동
    public void NodeMovement()
    {
        if (_nodesIndex < _nodesCount)
        {
            float moveDistance = Stat.NodeSpeed[_nodesIndex] * Time.deltaTime;
            float nextDistance = Vector3.Distance(Stat.NodeDir[_nodesIndex], transform.position);

            Vector3 dir = (Stat.NodeDir[_nodesIndex] - transform.position).normalized;

            for (; moveDistance > nextDistance;)
            {
                
                transform.position += dir * nextDistance;

                moveDistance -= nextDistance;
                _nodesIndex++;

                if (_nodesIndex + 1 >= _nodesCount)
                    return;
                dir = (Stat.NodeDir[_nodesIndex + 1] - transform.position).normalized;
                nextDistance = Vector3.Distance(Stat.NodeDir[_nodesIndex + 1], transform.position);

            }
            transform.position += dir * Stat.NodeSpeed[_nodesIndex] * Time.deltaTime;
            //기본값 = 기본값 + (바뀔값 - 기본값) / (2 이상의 숫자)
            //transform.rotation = transform.rotation * (Stat.NodeRot[_nodesIndex]);
            //transform.rotation =
                //Quaternion.RotateTowards(
                    //transform.rotation,
                    //Stat.NodeRot[_nodesIndex],
                    //360.0f * Time.deltaTime);
            //transform.rotation = transform.rotation + (Stat.NodeRot[_nodesIndex] * transform.rotation) / 2;

            if (_nodesIndex + 1 >= _nodesCount)
                return;
            dir = (Stat.NodeDir[_nodesIndex] - transform.position).normalized;

        }

    }
}
