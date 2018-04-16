using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMissile : DragonAction {

    public override bool Run()
    {
        return false;
    }

    IEnumerator MissileStart(float preTime, float afterTime)
    {
        yield return new WaitForSeconds(preTime);

        while (true) { 
            yield return CoroutineManager.FiexdUpdate;
        }
        yield return new WaitForSeconds(afterTime);
    }

}
