﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIManager : MonoBehaviour
{
    [SerializeField]
    private BehaviorTree _tree;
    public BehaviorTree Tree { get { return _tree; } }

    private void Awake()
    {
        _tree = Resources.Load("DragonAI/DragonAI(BTs)", typeof(ScriptableObject)) as BehaviorTree;
        Debug.Log(_tree);
    }

    // Use this for initialization
    void Start()
    {
        if (Application.isPlaying)
        {
            BlackBoard.Instance.InitMamber();
            StartCoroutine(BehaviorTreeCor());
        }

    }

    // Update is called once per frame
    void Update ()
    {
	}

    IEnumerator BehaviorTreeCor()
    {
        while (!_tree.Root.Run()) 
        {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("End");
    }

}
