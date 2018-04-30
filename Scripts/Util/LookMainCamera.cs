using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookMainCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = Camera.main.transform.position - this.transform.position;
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
	}
}
