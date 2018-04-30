using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLandChk : DragonConTask
{
    public override bool Run()
    {
        float CurHP = BlackBoard.Instance.Stat.HP;    //현재HP
        float LandHP = BlackBoard.Instance.HpLand;    //땅으로 가기 위한 퍼센트

        float ChangedHP = BlackBoard.Instance.ChangedHP;

        bool IsFly = BlackBoard.Instance.IsFlying;
        bool IsHovering = BlackBoard.Instance.IsHovering;

        bool IsFlyingAct = BlackBoard.Instance.FlyingAct;
        bool IsHoveringAct = BlackBoard.Instance.HoveringAct;
        
        if (ChangedHP - LandHP >= CurHP && (IsFly || IsHovering) && (!IsFlyingAct && !IsHoveringAct))
        {
            Debug.Log("Landing");
            BlackBoard.Instance.IsLanding = true;
            return false;
        }
        return true;
    }
}
