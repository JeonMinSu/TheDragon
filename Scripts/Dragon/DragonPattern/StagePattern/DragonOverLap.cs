using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonOverLap : DragonAction {

    public override bool Run()
    {

        Transform Dragon = BlackBoard.Instance.Manager.transform;
        Transform Player = BlackBoard.Instance.Manager.Player;

        if (BlackBoard.Instance.DistanceCalc(Dragon, Player, 30.0f))
        {
            return false;
        }

        BlackBoard.Instance.GetLandTime().CurLandWalkTime = 0.0f;
        BlackBoard.Instance.GetLandTime().CurIdleTime = 0.0f;
        return true;
    }

}
