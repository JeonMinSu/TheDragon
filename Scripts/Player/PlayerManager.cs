using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerStat))]
public class PlayerManager : MonoBehaviour {

    private PlayerStat _stat;
    public PlayerStat Stat { get { return _stat; } }

    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
    }

    // Use this for initialization
    void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
