using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTakeOffChk : DragonConTask
{
    public override bool Run()
    {

        int MoveIndex = (int)MoveManagers.TakeOff;

        bool IsTakeOff = BlackBoard.Instance.IsTakeOff;
        bool IsState = BlackBoard.Instance.IsStage;
        bool IsFly = BlackBoard.Instance.IsFlying;

        bool IsTakeOffEnd = BlackBoard.Instance.GetNodeManager(MoveIndex).IsMoveEnd;

        float MaxHP = BlackBoard.Instance.Stat.MaxHP;
        float CurHP = BlackBoard.Instance.Stat.HP;      //현재HP
        float LandHP = BlackBoard.Instance.HpLand;    //땅으로 가기 위한 퍼센트
        float TakeOffHP = BlackBoard.Instance.HpTakeOff; //날기 위한 HP퍼센트

        if (CurHP % TakeOffHP == 0 && !IsTakeOffEnd && CurHP != MaxHP)
        {
            Debug.Log("하늘날기");
            BlackBoard.Instance.Manager.Ani.SetTrigger("TakeOff");
            BlackBoard.Instance.IsStage = false;
            BlackBoard.Instance.IsTakeOff = true;
            return false;
        }
        return true;

    }

}
