using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlying : DragonAction
{
    public override bool Run()
    {
        float MaxTime = BlackBoard.Instance.GetFlyingTime().FlyTime;
        float curTime = BlackBoard.Instance.GetFlyingTime().CurFlyTime;
        float Angle = BlackBoard.Instance.Theta;

        int MoveIndex = (int)MoveManagers.FlyingCircle;

        bool IsFlying = BlackBoard.Instance.IsFlying;
        bool IsFlyingReady = BlackBoard.Instance.IsMoveReady(MoveIndex);


        Transform Dragon = BlackBoard.Instance.Manager.transform;
        Transform Player = BlackBoard.Instance.Manager.Player;

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
