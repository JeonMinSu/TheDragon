﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLanding : DragonAction
{
    public override bool Run()
    {
        if (BlackBoard.Instance.IsLandingAct)
        { 
            Debug.Log("Landing");
            return false;
        }
        return true;
    }
}
