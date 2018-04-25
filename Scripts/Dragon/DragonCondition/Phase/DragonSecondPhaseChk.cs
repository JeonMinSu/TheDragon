using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSecondPhaseChk : DragonConTask {

    public override bool Run()
    {
        if (BlackBoard.Instance.CurrentPhase == DragonPhases.SecondPhase)
        {
            Debug.Log("SecondPhase");
            return true;
        }
        return false;
    }

}
