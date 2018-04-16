using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonGliding : DragonAction {

    public override bool Run()
    {
        Debug.Log("글라이딩 공격");
        return false;
    }

}
