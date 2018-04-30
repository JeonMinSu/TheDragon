using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHoverChk : DragonConTask
{
    public override bool Run()
    {

        bool isHovering = BlackBoard.Instance.IsHovering;
        bool FlyingAct = BlackBoard.Instance.FlyingAct;
        bool isTakeOff = BlackBoard.Instance.IsTakeOff;

        if (isHovering && !FlyingAct)
        {
            Debug.Log("Hovering");
            return false;
        }
        return true;

    }

}
