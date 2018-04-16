using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPhase : ActionTask
{
    private DragonManager _manager;
    public DragonManager Manager { get { return _manager; } }

    public void Awake()
    {
        _manager = GameObject.FindWithTag("Dragon").GetComponent<DragonManager>();
    }

    public override bool Run()
    {
        return false;
    }

}
