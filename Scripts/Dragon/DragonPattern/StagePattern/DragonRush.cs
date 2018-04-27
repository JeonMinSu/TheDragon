using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonRush : DragonAction {

    public override bool Run()
    {
        Transform Dragon = BlackBoard.Instance.Manager.transform;
        Transform Player = BlackBoard.Instance.Manager.Player;

        //float preTime = BlackBoard.Instance.GetLandTime().PreRushTime;
        //float afterTime = BlackBoard.Instance.GetLandTime().AfterRushTime;

        bool IsStage = BlackBoard.Instance.IsStage;
        //bool IsStageAct = BlackBoard.Instance.IsStageAct;

        if (!BlackBoard.Instance.DistanceCalc(Dragon, Player, 30.0f) && IsStage)
        {
            //if (!IsStageAct)
                //CoroutineManager.DoCoroutine(DragonRushStart(preTime, afterTime));
            Debug.Log("Rush");

            return false;
        }
        return true;

    }
    /*
    IEnumerator DragonRushStart(float _preTime, float _afterTime)
    {
        float Curtime = 0;
        float RunTime = BlackBoard.Instance.GetLandTime().RushRunTime;

        yield return new WaitForSeconds(_preTime);

        while (Curtime < RunTime)
        {
            Curtime += Time.fixedDeltaTime;
            yield return CoroutineManager.EndOfFrame;
        }

        yield return new WaitForSeconds(_afterTime);
        BlackBoard.Instance.GetLandTime().CurLandWalkTime = 0.0f;
        BlackBoard.Instance.IsStageAct = false;

    }
    */



}
