using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlying : DragonAction
{
    public override bool Run()
    {
        int MoveIndex = (int)MoveManagers.FlyingCircle;

        float MaxTime = BlackBoard.Instance.GetFlyingTime().FlyTime;
        float curTime = BlackBoard.Instance.GetFlyingTime().CurFlyTime;

        bool IsFlying = BlackBoard.Instance.IsFlying;
        bool IsFlyingReady = BlackBoard.Instance.IsMoveReady(MoveIndex);

        if (IsFlying && curTime < MaxTime)
        {
            BlackBoard.Instance.Manager.Ani.SetTrigger("Flying");
            if (!IsFlyingReady)
                BlackBoard.Instance.FlyingMoveReady(MoveIndex);

            BlackBoard.Instance.FlyingMovement(MoveIndex);

            return false;
        }
        return true;
    }

}
