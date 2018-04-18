using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMissile : DragonAction
{

    public override bool Run()
    {
        float PlayerHP = 70.0f;
        float PlayerMaxHp = 100.0f;
        bool PatternChk = BlackBoard.Instance.HoveringAct;

        float preTime = BlackBoard.Instance.FlyingPatternTime.PreMissileTime;
        float afterTime = BlackBoard.Instance.FlyingPatternTime.AfterMissileTime;

        if (!PatternChk && PlayerHP > PlayerMaxHp * 0.5f)
        {
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
        BlackBoard.Instance.IsFlying = true;
    }

}