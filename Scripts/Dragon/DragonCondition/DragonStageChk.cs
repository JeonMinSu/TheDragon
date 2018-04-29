using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonStageChk : DragonConTask {

    public override bool Run()
    {
        bool isStage = BlackBoard.Instance.IsStage;
        bool isTakeOff = BlackBoard.Instance.IsTakeOff;

        if (isStage && !isTakeOff)
        {
            return false;
        }
        return true;
    }
}
