using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHoverChk : DragonConTask
{
    public override bool Run()
    {
        float CurHp = BlackBoard.Instance.Stat.HP;          //현재HP
        float LandPercentHP = BlackBoard.Instance.HpLand;   //땅으로 가기 위한 HP

        bool IsTakeOff = BlackBoard.Instance.IsTakeOff;
        bool IsTakeOffAct = BlackBoard.Instance.IsTakeOffAct;

        bool isHovering = BlackBoard.Instance.IsHovering;

        if (!IsTakeOff && isHovering && IsTakeOffAct)
        {
            BlackBoard.Instance.Manager.Ani.ResetTrigger("TakeOff");
            BlackBoard.Instance.Manager.Ani.SetTrigger("Hovering");
            BlackBoard.Instance.IsStage = false;
            return true;
        }
        BlackBoard.Instance.IsStage = true;
        return false;

    }

}
