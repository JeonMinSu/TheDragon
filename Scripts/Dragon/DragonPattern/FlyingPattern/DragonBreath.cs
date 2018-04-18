using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBreath : DragonAction {

    public override bool Run()
    {
        bool PatternChk = BlackBoard.Instance.FlyingAct;
        
        float preTime = BlackBoard.Instance.FlyingPatternTime.PreBreathTime;
        float afterTime = BlackBoard.Instance.FlyingPatternTime.PreBreathTime;
        
        if (!PatternChk)
        {
            CoroutineManager.DoCoroutine(BreathOn(preTime, afterTime));
            return false;
        }
        return true;
    }

    IEnumerator BreathOn(float preTime, float afterTime)
    {
        Transform Mouth = BlackBoard.Instance.DragonMouth;
        BlackBoard.Instance.FlyingAct = true;

        yield return new WaitForSeconds(preTime);

        BlackBoard.Instance.BulletManager.BreathOn(Mouth);
        yield return CoroutineManager.FiexdUpdate;

        yield return new WaitForSeconds(afterTime);
        BlackBoard.Instance.BulletManager.BreathOff();
        BlackBoard.Instance.IsFlying = false;
    }
}
