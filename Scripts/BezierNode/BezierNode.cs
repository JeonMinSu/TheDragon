using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
    만 든 날 : 2018-03-29 - 21:15
    작 성 자 : 전민수

    기본 노드 Node

    베지어곡선을 이용하여 노드 만들기
    
*/

public class BezierNode : MonoBehaviour {

    public Transform CurveNode;         //커브노드(중간노드

    [SerializeField]
    private float _nodeSpeed = 5.0f;             //노드 스피드
    public float NodeSpeed { set { _nodeSpeed = value; } get { return _nodeSpeed; } }

    [SerializeField]
    private float _nodeDiv;
    public float NodeDiv { set { _nodeDiv = value; } get { return _nodeDiv; } }

    public Quaternion GetRotate { get { return transform.rotation; } }

}
