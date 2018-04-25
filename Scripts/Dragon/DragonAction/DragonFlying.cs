using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlying : DragonAction
{
    public override bool Run()
    {
        int MoveIndex = (int)MoveManagers.FlyingCircle;

        bool IsFlying = BlackBoard.Instance.IsFlying;
        bool IsFlyingReady = BlackBoard.Instance.IsMoveReady(MoveIndex);
        bool IsFlyingEnd = BlackBoard.Instance.GetNodeManager(MoveIndex).IsMoveEnd;

        if (IsFlying)
        {
            BlackBoard.Instance.Manager.Ani.SetTrigger("Flying");
            if (!IsFlyingReady)
                BlackBoard.Instance.FlyingMoveReady(MoveIndex);

            if (!BlackBoard.Instance.FlyingAct)
                CoroutineManager.DoCoroutine(FlyingStartCor(MoveIndex));

            return false;
        }
        return true;
    }

    IEnumerator FlyingStartCor(int Index)
    {
        float curTime = 0.0f;
        float MaxTime = BlackBoard.Instance.GetFlyingTime().FlyTime;

        BlackBoard.Instance.FlyingAct = true;

        while (curTime < MaxTime)
        {
            BlackBoard.Instance.FlyingMovement(Index);
            curTime += Time.deltaTime;
            yield return CoroutineManager.EndOfFrame;
        }
        if (!BlackBoard.Instance.FlyingPatternAct)
            BlackBoard.Instance.HoveringPatternChk();

        BlackBoard.Instance.Manager.Ani.ResetTrigger("Flying");
        BlackBoard.Instance.HoveringPatternChk();
    }

}
