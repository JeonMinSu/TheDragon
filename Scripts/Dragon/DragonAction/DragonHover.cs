using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHover : DragonAction {

    public override bool Run()
    {
        float MaxTime = BlackBoard.Instance.GetFlyingTime().HoveringTime;
        float CurTime = BlackBoard.Instance.GetFlyingTime().CurHoveringTime;
        bool IsHovering = BlackBoard.Instance.IsHovering;

        if (CurTime < MaxTime && IsHovering)
        {
            BlackBoard.Instance.Manager.Ani.SetTrigger("Hovering");
            CurTime += Time.deltaTime;
            BlackBoard.Instance.GetFlyingTime().CurHoveringTime = CurTime;
            return false;
        }
        return true;
    }
}
