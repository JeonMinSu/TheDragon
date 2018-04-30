using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAniManager : MonoBehaviour {

    private DragonManager _manager;
    public DragonManager Manager { get { return _manager; } }

    private string _currentAniName;
    public string CurrentAniName { set { _currentAniName = value; } get { return _currentAniName; } }

    bool _isInit;

    private void Awake()
    {
        _manager = GetComponentInParent<DragonManager>();
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            SwicthAnimation("Idle");
            _isInit = true;
        }
    }

    public void SwicthAnimation(string _newAniName)
    {
        if (_isInit)
        {
            Manager.Ani.ResetTrigger(_currentAniName);
        }
        _currentAniName = _newAniName;
        Manager.Ani.SetTrigger(_currentAniName);
    }

    public void TakeOffReadyOn()
    {
        Debug.Log("TakeOffReady");
        BlackBoard.Instance.IsTakeOffReady = true;
    }

}
