using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTimer : MonoBehaviour {
    public GameObject activeTarget;
    public float delayTime;
    public bool activeValue;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (delayTime < 0)
        {
            if(activeTarget == null)
                    GameObject.Destroy(this.gameObject);
            else
                activeTarget.SetActive(true);
        }

        delayTime -= Time.deltaTime;
	}
}
