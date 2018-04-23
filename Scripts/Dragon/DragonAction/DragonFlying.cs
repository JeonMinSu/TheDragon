using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlying : DragonAction
{
    public override bool Run()
    {
        int MoveIndex = (int)MoveManagers.FlyingCircle;

        float curTime = BlackBoard.Instance.GetFlyingTime().CurFlyTime;
        float MaxTime = BlackBoard.Instance.GetFlyingTime().FlyTime;

        bool IsFlying = BlackBoard.Instance.IsFlying;
        bool IsFlyingReady = BlackBoard.Instance.IsMoveReady(MoveIndex);
        bool IsTakeOffEnd = BlackBoard.Instance.GetNodeManager(MoveIndex).IsMoveEnd;

        if (IsFlying && !IsTakeOffEnd)
        {
            BlackBoard.Instance.Manager.Ani.SetTrigger("Flying");

            if (!IsFlyingReady)
                BlackBoard.Instance.FlyingMoveReady(MoveIndex);

            if (!BlackBoard.Instance.IsTakeOffAct)
                CoroutineManager.DoCoroutine(FlyingStartCor(MoveIndex));

            return false;
        }
        return true;
    }

    IEnumerator FlyingStartCor(int Index)
    {
        while (!BlackBoard.Instance.GetNodeManager(Index).IsMoveEnd)
        {
            BlackBoard.Instance.FlyingMovement(Index);
            yield return CoroutineManager.EndOfFrame;
        }
    }

}
