using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMissile : DragonAction {

    public override bool Run()
    {
        float PlayerHP = 70.0f;
        float PlayerMaxHp = 100.0f;
        bool PatternChk = BlackBoard.Instance.HoveringPatternChk;

        float preTime = BlackBoard.Instance.FlyingPatternTime.PreMissileTime;
        float afterTime = BlackBoard.Instance.FlyingPatternTime.AfterMissileTime;

        if (!PatternChk && PlayerHP > PlayerMaxHp * 0.5f)
        {
            Debug.Log("Missile");
            BlackBoard.Instance.HoveringPatternChk = true;
            BlackBoard.Instance.IsFlying = true;
            return false;
        }
        return true;
    }

    IEnumerator MissileStart(float preTime, float afterTime)
    {
        yield return new WaitForSeconds(preTime);

        yield return CoroutineManager.FiexdUpdate;

        yield return new WaitForSeconds(afterTime);
    }

}
