using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayEndDestroy : MonoBehaviour {

    private AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(!audioSource.isPlaying)
        {
            GameObject.Destroy(this.gameObject);
        }
	}
}
