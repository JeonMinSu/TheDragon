﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlyChk : DragonConTask
{

    public override bool Run()
    {
        bool isFlying = BlackBoard.Instance.IsFlying;

        if (isFlying)
        {
            return true;
        }
        return true;
    }

}
