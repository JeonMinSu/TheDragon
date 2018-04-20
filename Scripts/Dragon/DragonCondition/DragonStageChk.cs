using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonStageChk : DragonConTask {

    public override bool Run()
    {
        bool isStage = BlackBoard.Instance.IsStage;

        if (isStage)
        {
            Debug.Log("stage");
            return true;
        }

        return true;
    }
}
