using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPhaseChk : DragonConTask
{

    public override bool Run()
    {
        if (BlackBoard.Instance.Stat.HP <
            BlackBoard.Instance.Stat.MaxHP * BlackBoard.Instance.HpPhaseThird )
        {
            BlackBoard.Instance.SetPhase(DragonPhases.ThridPhase);
            Debug.Log("체력이 15% 미만");
        }
        else if (BlackBoard.Instance.Stat.HP <
                BlackBoard.Instance.Stat.MaxHP * BlackBoard.Instance.HpPhaseSecond)
        {
            BlackBoard.Instance.SetPhase(DragonPhases.SecondPhase);
            Debug.Log("체력이 50% 미만");
        }
        else
        {
            BlackBoard.Instance.SetPhase(DragonPhases.FirstPhase);
            Debug.Log("체력이 50% 이상");
        }
        return false;

    }
}
