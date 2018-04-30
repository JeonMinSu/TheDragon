using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlyChk : DragonConTask
{

    public override bool Run()
    {
        bool isFlying = BlackBoard.Instance.IsFlying;
        bool HoveringAct = BlackBoard.Instance.HoveringAct;

        if (isFlying && !HoveringAct)
        {
            Debug.Log("Flying");
            return false;
        }
        return true;
    }

}
