using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlyChk : DragonConTask
{
    public override bool Run()
    {
        if (BlackBoard.Instance.Stat.HP % BlackBoard.Instance.HpLand != 0.0f ||
            BlackBoard.Instance.Stat.HP % BlackBoard.Instance.HpTakeOff == 0.0f)
        {
            Debug.Log("하늘에 있을래...ㅎ");
            return true;
        }
        else
        {
            Debug.Log("test");
        }
        return false;
    }

}
