using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonIdle : DragonAction
{
    public override bool Run()
    {
        float Curtime = BlackBoard.Instance.IdleTimes.CurIdleTime;
        float MaxTime = BlackBoard.Instance.IdleTimes.MaxIdleTime;


        if (Curtime < MaxTime)
        {
            BlackBoard.Instance.IdleTimes.CurIdleTime += Time.deltaTime;
            BlackBoard.Instance.Manager.Ani.SetTrigger("Idle");
            return false;
        }
        return true;

    }}