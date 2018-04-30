using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTakeOff : DragonAction {

    public override bool Run()
    {

        int MoveIndex = (int)MoveManagers.TakeOff;

        float CurHP = BlackBoard.Instance.Stat.HP;

        bool IsTakeOff = BlackBoard.Instance.IsTakeOff;
        bool IsFlyingReady = BlackBoard.Instance.IsMoveReady(MoveIndex);
        bool IsTakeOffEnd = BlackBoard.Instance.GetNodeManager(MoveIndex).IsMoveEnd;

        bool IsTakeOffAct = BlackBoard.Instance.IsTakeOffAct;
        bool IstakeOffReady = BlackBoard.Instance.IsTakeOffReady;

        if (IsTakeOff && !IsTakeOffEnd)
        {
            BlackBoard.Instance.aniManager.SwicthAnimation("TakeOff");
            if (!IsFlyingReady && IstakeOffReady)
                BlackBoard.Instance.MoveMentReady(MoveIndex);

            else
            {
                if (!IsTakeOffAct && IsFlyingReady) { 
                    CoroutineManager.DoCoroutine(TakeOffStartCor(MoveIndex));
                    BlackBoard.Instance.IsHovering = true;
                }
            }

            return false;
        }
        return true;
    }

    IEnumerator TakeOffStartCor(int Index)
    {
        BlackBoard.Instance.IsTakeOffAct = true;
        BlackBoard.Instance.Clocks.InitLandTimes();

        while (!BlackBoard.Instance.GetNodeManager(Index).IsMoveEnd)
        {
            BlackBoard.Instance.FlyingMovement(Index);
            yield return CoroutineManager.EndOfFrame;
        }
        BlackBoard.Instance.Manager.Ani.ResetTrigger("TakeOff");
        BlackBoard.Instance.ChangedHP = BlackBoard.Instance.Stat.HP;
        BlackBoard.Instance.IsTakeOff = false;
        BlackBoard.Instance.IsTakeOffAct = false;
        BlackBoard.Instance.IsStage = false;
        BlackBoard.Instance.IsTakeOffReady = false;
        BlackBoard.Instance.GetNodeManager(Index).IsMoveEnd = false;


    }

}
