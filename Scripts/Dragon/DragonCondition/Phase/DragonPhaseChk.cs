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
        }
        else if (BlackBoard.Instance.Stat.HP <
                BlackBoard.Instance.Stat.MaxHP * BlackBoard.Instance.HpPhaseSecond)
        {
            BlackBoard.Instance.SetPhase(DragonPhases.SecondPhase);
        }
        else
        {
            BlackBoard.Instance.SetPhase(DragonPhases.FirstPhase);
        }
        return false;

    }
}
