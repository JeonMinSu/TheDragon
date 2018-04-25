using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHoverChk : DragonConTask
{
    public override bool Run()
    {

        bool isHovering = BlackBoard.Instance.IsHovering;

        if (isHovering)
        {
            return false;
        }
        return true;

    }

}
