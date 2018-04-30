using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour {
    public float DelayTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (DelayTime <= 0.0f)
            GameObject.Destroy(this.gameObject);
	}
}
