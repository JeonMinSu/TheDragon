﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlyChk : DragonConTask
{
    public override bool Run()
    {
        float CurHp = BlackBoard.Instance.Stat.HP;          //현재HP
        float FlyPercentHP = BlackBoard.Instance.HpTakeOff; //날기 위한 HP퍼센트
        float LandPercentHP = BlackBoard.Instance.HpLand;   //땅으로 가기 위한 HP

        if (BlackBoard.Instance.IsFlying && CurHp % LandPercentHP !=0)
        {
            BlackBoard.Instance.Manager.Ani.ResetTrigger("Idle");
            BlackBoard.Instance.Manager.Ani.SetTrigger("Hovering");
            BlackBoard.Instance.IsStage = false;
            Debug.Log("하늘로 훨훨");
            return true;
        }
        BlackBoard.Instance.IsStage = true;
        return false;

    }

}
