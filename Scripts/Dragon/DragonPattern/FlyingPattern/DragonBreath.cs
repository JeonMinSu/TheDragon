﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBreath : DragonAction {

    public override bool Run()
    {
        bool PatternChk = BlackBoard.Instance.FlyingPatternChk;
        
        float preTime = BlackBoard.Instance.FlyingPatternTime.PreBreathTime;
        float afterTime = BlackBoard.Instance.FlyingPatternTime.PreBreathTime;

        if (!PatternChk && BlackBoard.Instance.BreathNum < 50)
        {
            CoroutineManager.DoCoroutine(MissileStart(preTime, afterTime));
            return false;
        }
        return true;
    }

    IEnumerator MissileStart(float preTime, float afterTime)
    {
        Transform Mouth = BlackBoard.Instance.DragonMouth;

        yield return new WaitForSeconds(preTime);

        BlackBoard.Instance.BulletManager.BreathOn(Mouth);
        yield return CoroutineManager.FiexdUpdate;

        yield return new WaitForSeconds(afterTime);
        BlackBoard.Instance.BreathNum++;
        BlackBoard.Instance.IsFlying = false;
        BlackBoard.Instance.FlyingPatternChk = true;
    }
}
