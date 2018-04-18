using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTakeOffChk : DragonConTask
{
    public override bool Run()
    {
        bool IsTakeOff = BlackBoard.Instance.IsTakeOff;
        bool IsLandAct = BlackBoard.Instance.IsLandingAct;
        bool IsState = BlackBoard.Instance.IsStage;

        float CurHp = BlackBoard.Instance.Stat.HP;      //현재HP
        float IsLandHP = BlackBoard.Instance.HpLand;    //땅으로 가기 위한 퍼센트
        
        if (IsTakeOff && !IsLandAct && (CurHp % IsLandHP != 0))
        {
            Debug.Log("하늘치킨");
            BlackBoard.Instance.Manager.Ani.ResetTrigger("Idle");
            BlackBoard.Instance.Manager.Ani.SetTrigger("TakeOff");
            BlackBoard.Instance.IsStage = false;
            return true;
        }
        return false;

    }

}
