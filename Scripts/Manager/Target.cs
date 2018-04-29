using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Target : MonoBehaviour {

    TargetManager targetManager;

	// Use this for initialization
	void Start ()
    {
        targetManager = FindObjectOfType<TargetManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Hit()
    {
        targetManager.SendMessage("Hit");
    }

}
