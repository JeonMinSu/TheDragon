using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTakeOffChk : DragonConTask
{
    public override bool Run()
    {
        int MoveIndex = (int)MoveManagers.TakeOff;

        float MaxHP = BlackBoard.Instance.Stat.MaxHP;
        float CurHP = BlackBoard.Instance.Stat.HP;      //현재HP
        float TakeOffHP = BlackBoard.Instance.HpTakeOff; //날기 위한 HP퍼센트

        float ChangedHP = BlackBoard.Instance.ChangedHP;

        bool IsFly = BlackBoard.Instance.IsFlying;
        bool IsStage = BlackBoard.Instance.IsStage;

        bool IsStageAct = BlackBoard.Instance.IsStageAct;
        bool IsLandingAct = BlackBoard.Instance.IsLandingAct;
        bool IsTakeOffEnd = BlackBoard.Instance.GetNodeManager(MoveIndex).IsMoveEnd;

        if (ChangedHP - TakeOffHP >= CurHP && !IsLandingAct && !IsStageAct && IsStage && CurHP != MaxHP)
        {
            Debug.Log("TakeOff");
            BlackBoard.Instance.Manager.Ani.SetTrigger("TakeOff");
            BlackBoard.Instance.IsTakeOff = true;

            return false;
        }
        return true;


    }

}
