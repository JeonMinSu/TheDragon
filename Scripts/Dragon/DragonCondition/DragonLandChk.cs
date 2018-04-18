using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLandChk : DragonConTask
{
    public override bool Run()
    {
        float MaxHP = BlackBoard.Instance.Stat.MaxHP;
        float CurHp = BlackBoard.Instance.Stat.HP;          //현재HP
        float TakeOffHP = BlackBoard.Instance.HpTakeOff; //날기 위한 HP퍼센트

        bool IsTakeOffAct = BlackBoard.Instance.IsTakeOffAct;
        bool IsFlyingAct = BlackBoard.Instance.FlyingAct;
        bool IsStage = BlackBoard.Instance.IsStage;

        if (((CurHp % TakeOffHP != 0) && !IsFlyingAct) || CurHp == MaxHP)
        {
            BlackBoard.Instance.IsStage = true;
            BlackBoard.Instance.IsTakeOff = false;
            Debug.Log("땅땅치킨");
            return true;
        }
        BlackBoard.Instance.IsTakeOff = true;
        return false;
    }
}
