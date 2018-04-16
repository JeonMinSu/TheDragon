using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFristPhaseChk : DragonConTask {

    public override bool Run()
    {
        if (BlackBoard.Instance.CurrentPhase == DragonPhases.FirstPhase)
        {
            return false;
        }
        return true;
    }


}
