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
            if (!IsFlyingReady)
                BlackBoard.Instance.MoveMentReady(MoveIndex);
            else { 
                if (!BlackBoard.Instance.FlyingAct)
                    CoroutineManager.DoCoroutine(FlyingStartCor(MoveIndex));
            }
            return false;
        }
        return true;
    }

    IEnumerator FlyingStartCor(int Index)
    {
        float curTime = 0.0f;
        float fireTime = 0.0f;
        float MaxTime = BlackBoard.Instance.GetFlyingTime().FlyTime;
        Transform Player = BlackBoard.Instance.Manager.Player;
        Transform Dragon = BlackBoard.Instance.Manager.transform;

        BlackBoard.Instance.FlyingAct = true;

        while (curTime < MaxTime)
        {
            BlackBoard.Instance.FlyingMovement(Index);
            curTime += Time.deltaTime;
            fireTime -= Time.deltaTime;

            Vector3 firePos = Dragon.position + Dragon.up * 5;
            if(fireTime <= 0.0f)
            {
                BlackBoard.Instance.BulletManager.DragonBaseBulletFire(firePos, (Player.position - firePos).normalized);
                BlackBoard.Instance.BulletManager.DragonBaseBulletFire(firePos, ((Player.position + Player.right * 4) - firePos).normalized);
                BlackBoard.Instance.BulletManager.DragonBaseBulletFire(firePos, ((Player.position - Player.right * 4) - firePos).normalized);
                fireTime = 0.5f;
            }
            yield return CoroutineManager.EndOfFrame;
        }
        if (!BlackBoard.Instance.FlyingPatternAct)
            BlackBoard.Instance.HoveringPatternChk();

        BlackBoard.Instance.Manager.Ani.ResetTrigger("Flying");
        BlackBoard.Instance.HoveringPatternChk();
    }

}
