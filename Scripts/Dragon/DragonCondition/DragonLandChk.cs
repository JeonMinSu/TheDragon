using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLandChk : DragonConTask
{
    public override bool Run()
    {
        float CurHP = BlackBoard.Instance.Stat.HP;          //현재HP
        float TakeOffHP = BlackBoard.Instance.HpTakeOff; //날기 위한 HP퍼센트
        float LandHP = BlackBoard.Instance.HpLand;    //땅으로 가기 위한 퍼센트

        bool IsFly = BlackBoard.Instance.IsFlying;
        bool IsHovering = BlackBoard.Instance.IsHovering;

        if (CurHP % LandHP == 0 && (IsFly || IsHovering))
        {
            BlackBoard.Instance.IsLanding = true;
            Debug.Log("땅땅치킨으로 가자");
            return false;
        }
        return true;
    }
}
