using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDeadChk : DragonConTask
{
    public override bool Run()
    {
        if (BlackBoard.Instance.Stat.HP <= 0) {
            return true;
        }
        return false;
    }

}