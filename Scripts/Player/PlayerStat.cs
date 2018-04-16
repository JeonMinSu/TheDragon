using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour {

    private int _hp;
    public int HP { set { _hp = value; } get { return _hp; } }

    private PlayerManager _manager;
    public PlayerManager Manager { get { return _manager; } }

    private void Awake()
    {
        _manager = GetComponent<PlayerManager>();
    }
}
