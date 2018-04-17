using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBreath : DragonAction {

    public override bool Run()
    {
        float PlayerHP = 70.0f;
        float PlayerMaxHp = 100.0f;

        bool PatternChk = BlackBoard.Instance.FlyingPatternChk;
        
        float preTime = BlackBoard.Instance.FlyingPatternTime.PreBreathTime;
        float afterTime = BlackBoard.Instance.FlyingPatternTime.PreBreathTime;

        if (!PatternChk && BlackBoard.Instance.BreathNum < 50)
        {
            Debug.Log("breath");
            return false;
        }
        return true;
    }

    IEnumerator MissileStart(float preTime, float afterTime)
    {
        yield return new WaitForSeconds(preTime);

        yield return CoroutineManager.FiexdUpdate;

        yield return new WaitForSeconds(afterTime);
        BlackBoard.Instance.BreathNum++;
        BlackBoard.Instance.IsFlying = false;
        BlackBoard.Instance.FlyingPatternChk = true;
    }
}
