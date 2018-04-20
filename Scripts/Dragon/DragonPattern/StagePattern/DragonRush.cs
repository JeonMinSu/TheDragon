using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonRush : DragonAction {

    public override bool Run()
    {
        Transform Dragon = BlackBoard.Instance.Manager.transform;
        Transform Player = BlackBoard.Instance.Manager.Player;

        float preTime = BlackBoard.Instance.GetLandTime().PreRushDelay;
        float afterTime = BlackBoard.Instance.GetLandTime().AfterRushDelay;

        bool Ready = BlackBoard.Instance.IsPatternChk;

        if (BlackBoard.Instance.DistanceCalc(Dragon, Player, 30.0f) ||
            BlackBoard.Instance.IsPatternChk)
        {
            if (!Ready)
            {
                Ready = true;
                BlackBoard.Instance.IsPatternChk = Ready;
                CoroutineManager.DoCoroutine(DragonRushStart(preTime, afterTime));
            }
            StopCoroutine(DragonRushStart(preTime, afterTime));
            return false;
        }

        BlackBoard.Instance.GetLandTime().CurLandWalkTime = 0.0f;
        BlackBoard.Instance.IsRadiusChk = false;
        return true;

    }

    IEnumerator DragonRushStart(float _preTime, float _afterTime)
    {
        Transform fixPos = BlackBoard.Instance.Manager.Player;
        Transform Dragon = BlackBoard.Instance.Manager.transform;

        float LimitDir = BlackBoard.Instance.RushLimitDir;
        float RunTime = BlackBoard.Instance.GetLandTime().RushRunTime;
        float CurTime = 0.0f;
        float AccSpeed = BlackBoard.Instance.Stat.AccRushSpeed;

        Vector3 dir = (Dragon.position - fixPos.position).normalized;

        yield return new WaitForSeconds(_preTime);

        while (CurTime < RunTime)
        {
            float speed = LimitDir * RunTime;

            BlackBoard.Instance.Stat.CurRushSpeed =
                BlackBoard.Instance.Acceleration(
                    BlackBoard.Instance.Stat.CurRushSpeed, speed, AccSpeed);

            Dragon.Translate(
                dir *
                BlackBoard.Instance.Stat.CurRushSpeed *
                Time.deltaTime);

            CurTime += Time.deltaTime;

            yield return CoroutineManager.EndOfFrame;
            
        }

        yield return new WaitForSeconds(_afterTime);
    }



}
