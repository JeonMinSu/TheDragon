using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMissile : DragonAction
{

    public override bool Run()
    {
        float PlayerHP = 70.0f;
        float PlayerMaxHp = 100.0f;
        bool IsHovering = BlackBoard.Instance.IsHovering;
        bool HoveringAct = BlackBoard.Instance.HoveringAct;

        float preTime = BlackBoard.Instance.GetFlyingTime().PreMissileTime;
        float afterTime = BlackBoard.Instance.GetFlyingTime().AfterMissileTime;

        if (IsHovering && PlayerHP > PlayerMaxHp * 0.5f)
        {
            Debug.Log("Missile");
            if (!HoveringAct)
                CoroutineManager.DoCoroutine(MissileStart(preTime, afterTime));
            return false;
        }
        StopCoroutine(MissileStart(preTime, afterTime));
        return true;
    }

    IEnumerator MissileStart(float preTime, float afterTime)
    {
        BlackBoard.Instance.HoveringAct = true;
        Transform Mouth = BlackBoard.Instance.DragonMouth;
        yield return new WaitForSeconds(preTime);
        for (int i = 0; i < 5; i++)
        {
            BlackBoard.Instance.BulletManager.HomingBulletFire(Mouth);
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(afterTime);

        BlackBoard.Instance.Manager.Ani.ResetTrigger("Hovering");
        BlackBoard.Instance.HoveringAct = false;
        BlackBoard.Instance.IsFlying = true;
        BlackBoard.Instance.IsHovering = false;
    }

}