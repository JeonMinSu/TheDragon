using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLanding : DragonAction
{
    public override bool Run()
    {

        int MoveIndex = (int)MoveManagers.Landing;

        float curDir = BlackBoard.Instance.Stat.CurTakeOffDir;
        float LimitDir = BlackBoard.Instance.TakeOffLimitDir;

        bool IsLanding = BlackBoard.Instance.IsLanding;
        bool IsFlyingReady = BlackBoard.Instance.IsMoveReady(MoveIndex);
        bool IsLandEnd = BlackBoard.Instance.GetNodeManager(MoveIndex).IsMoveEnd;

        if (IsLanding && !IsLandEnd)
        {
            if (!IsFlyingReady)
                BlackBoard.Instance.FlyingMoveReady(MoveIndex);

            if (!BlackBoard.Instance.IsLandingAct)
                CoroutineManager.DoCoroutine(LandingStartCor(MoveIndex));
        BlackBoard.Instance.IsStage = true;

            return false;
        }
        return false;
    }

    IEnumerator LandingStartCor(int moveIndex)
    {
        BlackBoard.Instance.IsLandingAct = true;
        BlackBoard.Instance.Clocks.InitFlyingTime();

        while (!BlackBoard.Instance.GetNodeManager(moveIndex).IsMoveEnd)
        {
            BlackBoard.Instance.FlyingMovement(moveIndex);
            yield return CoroutineManager.EndOfFrame;
        }
        BlackBoard.Instance.Manager.Ani.ResetTrigger("Landing");
        BlackBoard.Instance.IsLanding = false;
        BlackBoard.Instance.IsLandingAct = false;

    }

}
