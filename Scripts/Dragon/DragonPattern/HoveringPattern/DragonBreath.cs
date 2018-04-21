using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBreath : DragonAction {

    public override bool Run()
    {
        float PlayerHP = 70.0f;
        float PlayerMaxHp = 100.0f;

        bool HoveringAct = BlackBoard.Instance.HoveringAct;
        bool IsHovering = BlackBoard.Instance.IsHovering;

        float preTime = BlackBoard.Instance.GetFlyingTime().PreBreathTime;
        float afterTime = BlackBoard.Instance.GetFlyingTime().AfterBreathTime;
        
        if (IsHovering && PlayerHP < PlayerMaxHp * 0.5)
        {
            if (!HoveringAct)
                CoroutineManager.DoCoroutine(BreathShot(preTime, afterTime));
            return false;
        }
        return true;
    }

    IEnumerator BreathShot(float preTime, float afterTime)
    {
        Transform Mouth = BlackBoard.Instance.DragonMouth;
        BlackBoard.Instance.FlyingAct = true;

        yield return new WaitForSeconds(preTime);

        BlackBoard.Instance.BulletManager.BreathOn(Mouth);
        yield return CoroutineManager.FiexdUpdate;

        yield return new WaitForSeconds(afterTime);
        BlackBoard.Instance.BulletManager.BreathOff();

        BlackBoard.Instance.Manager.Ani.ResetTrigger("Hovering");
        BlackBoard.Instance.HoveringAct = false;
        BlackBoard.Instance.IsHovering = false;
        BlackBoard.Instance.IsFlying = true;
        StopCoroutine(BreathShot(preTime, afterTime));
    }
}
