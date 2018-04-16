using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHover : DragonAction {

    public override bool Run()
    {
        float MaxTime = BlackBoard.Instance.FlyingTime.HoveringTime;
        float CurTime = BlackBoard.Instance.FlyingTime.CurHoveringTime;

        if (CurTime < MaxTime && !BlackBoard.Instance.HoveringChk)
        {
            CurTime += Time.deltaTime;
            BlackBoard.Instance.FlyingTime.CurHoveringTime = CurTime;
            Debug.Log("Hovering");
            return false;
        }
        BlackBoard.Instance.HoveringChk = true;
        return true;
    }
}
