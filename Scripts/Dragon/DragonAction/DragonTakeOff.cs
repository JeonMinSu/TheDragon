﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTakeOff : DragonAction {

    public override bool Run()
    {
        float curDir = BlackBoard.Instance.Stat.CurTakeOffDir;
        float LimitDir = BlackBoard.Instance.TakeOffLimitDir;
        bool IsTakeOff = BlackBoard.Instance.IsTakeOff;

        if (IsTakeOff && curDir < LimitDir)
        {
            if (!BlackBoard.Instance.IsTakeOffAct)
                CoroutineManager.DoCoroutine(TakeOffStartCor());

            return false;
        }
        StopCoroutine(TakeOffStartCor());
        return true;
    }

    IEnumerator TakeOffStartCor()
    {
        Transform Dragon = BlackBoard.Instance.Manager.transform;
        float speed = 0.0f;
        float MaxSpeed = BlackBoard.Instance.Stat.MaxTakeOffSpeed;
        float AccSpeed = BlackBoard.Instance.Stat.AccTakeOffeSpeed;

        float LimitDir = BlackBoard.Instance.TakeOffLimitDir;
        float curDir = speed * Time.deltaTime;

        BlackBoard.Instance.IsTakeOffAct = true;


        while (curDir < LimitDir)
        {
            speed = BlackBoard.Instance.Acceleration(speed, MaxSpeed, AccSpeed);
            curDir += speed;
            BlackBoard.Instance.Stat.CurTakeOffDir = curDir;
            Dragon.Translate(Vector3.up * speed);
            yield return CoroutineManager.EndOfFrame;
        }
        
        BlackBoard.Instance.Clocks.InitLandTimes();
        BlackBoard.Instance.IsTakeOff = false;
        BlackBoard.Instance.IsTakeOffAct = false;
        BlackBoard.Instance.IsHovering = true;
        BlackBoard.Instance.Manager.Ani.ResetTrigger("TakeOff");

    }

}
