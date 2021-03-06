﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonOverLap : DragonAction {

    public override bool Run()
    {
        Transform Dragon = BlackBoard.Instance.Manager.transform;
        Transform Player = BlackBoard.Instance.Manager.Player;

        bool IsStage = BlackBoard.Instance.IsStage;

        //int MoveIndex = (int)MoveManagers.OverLap;


        float preTime = BlackBoard.Instance.GetLandTime().PreOverLapTime;
        float afterTime = BlackBoard.Instance.GetLandTime().AfterOverLapTime;

        //bool IsFlyingReady = BlackBoard.Instance.IsMoveReady(MoveIndex);

        bool IsStageAct = BlackBoard.Instance.IsStageAct;
        
        if (BlackBoard.Instance.DistanceCalc(Dragon, Player, 30.0f) && IsStage)
        {
            BlackBoard.Instance.aniManager.SwicthAnimation("Idle");
            //if (!IsFlyingReady)
            //    //BlackBoard.Instance.FlyingMoveReady(MoveIndex);
            if (!IsStageAct)
                CoroutineManager.DoCoroutine(OverLapCor(preTime, afterTime));

            Debug.Log("OverLap");

            return false;
        }
        return true;
    }

    IEnumerator OverLapCor(float preTime, float afterTime)
    {
        float Curtime = 0;
        float RunTime = BlackBoard.Instance.GetLandTime().OverLapRunTime;

        BlackBoard.Instance.IsStageAct = true;

        yield return new WaitForSeconds(preTime);

        while (Curtime < RunTime)
        {
            Curtime += Time.fixedDeltaTime;
            yield return CoroutineManager.EndOfFrame;
        }

        yield return new WaitForSeconds(afterTime);
        BlackBoard.Instance.GetLandTime().CurLandWalkTime = 0.0f;
        BlackBoard.Instance.IsStageAct = false;

    }

}
