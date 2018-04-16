using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHover : DragonAction {

    public override bool Run()
    {
        return false;
    }

    IEnumerator DragonHoveringCor()
    {
        yield return CoroutineManager.Instance.EndOfFrame;
    }
}
