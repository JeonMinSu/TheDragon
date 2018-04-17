using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlying : DragonAction
{
    public override bool Run()
    {
        float MaxTime = BlackBoard.Instance.FlyingTime.FlyTime;
        float curTime = BlackBoard.Instance.FlyingTime.CurFlyTime;
        float Angle = BlackBoard.Instance.Theta;

        Transform Dragon = BlackBoard.Instance.Manager.transform;
        Transform Player = BlackBoard.Instance.Manager.Player;

        if (curTime < MaxTime)
        {
            BlackBoard.Instance.Manager.Ani.ResetTrigger("Hovering");
            BlackBoard.Instance.Manager.Ani.SetTrigger("Flying");

            float moveSpeed = BlackBoard.Instance.Stat.CurFlySpeed;
            float turnSpeed = BlackBoard.Instance.Stat.LandTurnSpeed;

            if (BlackBoard.Instance.DistanceCalc(Dragon, Player, 0.0f))
 { 
                BlackBoard.Instance.Move(Player, moveSpeed, turnSpeed);
            }

            BlackBoard.Instance.FlyingTime.CurFlyTime += Time.deltaTime;
            return false;
        }
        return true;
    }

}
