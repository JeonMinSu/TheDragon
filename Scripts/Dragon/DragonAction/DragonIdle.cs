using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonIdle : DragonAction
{
    public override bool Run()
    {
        float Curtime = BlackBoard.Instance.GetLandTime().CurIdleTime;
        float MaxTime = BlackBoard.Instance.GetLandTime().IdleTime;

        bool IsStage = BlackBoard.Instance.IsStage;
        
        if (IsStage && Curtime < MaxTime)
        {
            BlackBoard.Instance.GetLandTime().CurIdleTime += Time.deltaTime;
            BlackBoard.Instance.Manager.Ani.SetTrigger("Idle");
            return false;
        }
        return true;

    }
}