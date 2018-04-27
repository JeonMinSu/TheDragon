using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonStageChk : DragonConTask {

    public override bool Run()
    {
        bool isStage = BlackBoard.Instance.IsStage;

        if (isStage)
        {
            return false;
        }
        return true;
    }
}
