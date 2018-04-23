using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonRush : DragonAction {

    public override bool Run()
    {
        Transform Dragon = BlackBoard.Instance.Manager.transform;
        Transform Player = BlackBoard.Instance.Manager.Player;

        float preTime = BlackBoard.Instance.GetLandTime().PreRushTime;
        float afterTime = BlackBoard.Instance.GetLandTime().AfterRushTime;

        bool IsStage = BlackBoard.Instance.IsStage;
        bool IsStageAct = BlackBoard.Instance.IsStageAct;

        if (BlackBoard.Instance.DistanceCalc(Dragon, Player, 30.0f) && IsStage)
        {
            if (!IsStageAct)
                CoroutineManager.DoCoroutine(DragonRushStart(preTime, afterTime));

            return false;
        }
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

        Vector3 dir = (fixPos.position - Dragon.position).normalized;

        BlackBoard.Instance.IsStageAct = true;

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
        BlackBoard.Instance.GetLandTime().CurLandWalkTime = 0.0f;
        BlackBoard.Instance.IsStageAct = false;

    }



}
