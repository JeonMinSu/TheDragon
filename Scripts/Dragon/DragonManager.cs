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
[RequireComponent(typeof(MoveManager))]
public class DragonManager : MonoBehaviour {

    private DragonPhases _currentPhase;
    public DragonPhases CurrentPhase { get { return _currentPhase; } }

    private AIManager _aiManager;
    public AIManager AIManager { get { return _aiManager; } }

    private MoveManager _movesManager;
    public MoveManager MovesManager { get { return _movesManager; } }

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
        _stat = GetComponent<DragonStat>();
        _movesManager = GetComponent<MoveManager>();
        _ani = GetComponentInChildren<Animator>();


    } 

    // Use this for initialization
    void Start () {
         
        if (Application.isPlaying)
        {
            _isInit = true;
        }
		
	}

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            BlackBoard.Instance.Stat.HP = 95.0f;
        }
        if (Input.GetKey(KeyCode.F2))
        {
            _stat.HP = 80.0f;
        }
        if (Input.GetKey(KeyCode.F3))
        {
            BlackBoard.Instance.CurPlayerHP = 70.0f;
        }
        if (Input.GetKey(KeyCode.F4))
        {
            BlackBoard.Instance.CurPlayerHP = 40.0f;
        }
        if (Input.GetKey(KeyCode.F5))
        {
            BlackBoard.Instance.CurIceCrystalNum = 40;
        }
        if (Input.GetKey(KeyCode.F6))
        {
            BlackBoard.Instance.CurIceCrystalNum = 60;
        }

    }

    public void SetPhase(DragonPhases _newPhase)
    {
        _currentPhase = _newPhase;
    }

    public void FindPaths()
    {

    }

}
//