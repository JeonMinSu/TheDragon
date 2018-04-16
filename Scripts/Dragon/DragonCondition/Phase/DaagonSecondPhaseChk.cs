using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaagonSecondPhaseChk : DragonConTask {

    public override bool Run()
    {
        if (BlackBoard.Instance.CurrentPhase == DragonPhases.SecondPhase)
        {
            Debug.Log("SecondPhase");
            return false;
        }
        return true;
    }

}
