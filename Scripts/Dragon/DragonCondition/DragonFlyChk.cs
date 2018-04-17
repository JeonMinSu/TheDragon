using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlyChk : DragonConTask
{
    public override bool Run()
    {
        bool isFlyingEnd = BlackBoard.Instance.IsFlying;

        if (isFlyingEnd)
        {
            BlackBoard.Instance.IsRadiusChk = false;
            return true;
        }
        return false;
    }

}
