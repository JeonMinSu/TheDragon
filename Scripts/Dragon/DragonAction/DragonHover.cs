using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHover : DragonAction {

    public override bool Run()
    {
        float MaxTime = BlackBoard.Instance.FlyingTime.HoveringTime;
        float CurTime = BlackBoard.Instance.FlyingTime.CurHoveringTime;

        if (CurTime < MaxTime && !BlackBoard.Instance.IsHovering)
        {
            BlackBoard.Instance.HoveringAct = false;
            CurTime += Time.deltaTime;
            BlackBoard.Instance.FlyingTime.CurHoveringTime = CurTime;
            return false;
        }
        BlackBoard.Instance.IsHovering = true;
        return true;
    }
}
