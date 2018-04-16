using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLandChk : DragonConTask
{
    public override bool Run()
    {
        float MaxHP = BlackBoard.Instance.Stat.MaxHP;
        float CurHp = BlackBoard.Instance.Stat.HP;          //현재HP
        float FlyPercentHP = BlackBoard.Instance.HpTakeOff; //날기 위한 HP퍼센트
        float LandPercentHP = BlackBoard.Instance.HpLand;   //땅으로 가기 위한 HP

        if ((CurHp % FlyPercentHP != 0) && BlackBoard.Instance.IsStage || 
            CurHp == MaxHP)
        {
            BlackBoard.Instance.IsFlying = false;
            Debug.Log("땅땅치킨");
            return true;
        }
        BlackBoard.Instance.IsFlying = true;
        return false;
    }
}
