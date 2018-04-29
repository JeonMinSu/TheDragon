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

        bool IsFlyAct = BlackBoard.Instance.FlyingAct;
        bool IsHoveringAct = BlackBoard.Instance.HoveringAct;

        if (CurHP % LandHP == 0 && (IsFlyAct || IsHoveringAct))
        {
            BlackBoard.Instance.IsLanding = true;
            Debug.Log("땅으로~");
            return false;
        }
        return true;
    }
}
