using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DragonPhases
{
    FirstPhase,
    SecondPhase,
    ThridPhase
}


[RequireComponent(typeof(DragonStat))]
public class DragonManager : MonoBehaviour {

    private DragonPhases _currentPhase;
    public DragonPhases CurrentPhase { get { return _currentPhase; } }

    private AIManager _aiManager;
    public AIManager AIManager { get { return _aiManager; } }

    private DragonStat _stat;
    public DragonStat Stat { get { return _stat; } }

    private Transform _player;
    public Transform Player { get { return _player; } }

    private Animator _ani;
    public Animator Ani { get { return _ani; } }

    private int _aniParamID;
    public int AniParamID { set { _aniParamID = value; } get { return _aniParamID; } }
        
    bool _isInit;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _aiManager = GetComponent<AIManager>();
        _stat = GetComponent<DragonStat>();
        _ani = GetComponentInChildren<Animator>();


    } 

    // Use this for initialization
    void Start () {
         
        if (Application.isPlaying)
        {
            _isInit = true;
        }
		
	}

    public void SetPhase(DragonPhases _newPhase)
    {
        _currentPhase = _newPhase;
    }


    // Update is called once per frame
    void Update ()
    {
    }

}
//