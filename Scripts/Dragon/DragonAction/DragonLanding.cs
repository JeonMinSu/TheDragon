using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLanding : DragonAction
{
    public override bool Run()
    {
        Debug.Log("Landing");
        return false;
    }
}
