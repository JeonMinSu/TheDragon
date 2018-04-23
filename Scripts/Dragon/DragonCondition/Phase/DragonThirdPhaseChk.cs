using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonThirdPhaseChk : DragonConTask {

    public override bool Run()
    {
        if (BlackBoard.Instance.CurrentPhase == DragonPhases.ThridPhase)
        {
            Debug.Log("ThridPhase");
            return true;
        }
        return false;
    }

}
