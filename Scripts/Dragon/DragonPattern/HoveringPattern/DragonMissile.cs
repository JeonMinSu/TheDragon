using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMissile : DragonAction {

    public override bool Run()
    {
        if (BlackBoard.Instance.HoveringChk)
        {
            Debug.Log("Misslie");

        }
        return false;
    }

    IEnumerator MissileStart(float preTime, float afterTime)
    {
        yield return new WaitForSeconds(preTime);

        yield return CoroutineManager.FiexdUpdate;

        yield return new WaitForSeconds(afterTime);
    }

}
