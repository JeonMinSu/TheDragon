using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonIceBullet : DragonAction {

    public override bool Run()
    {
        bool IsFlying = BlackBoard.Instance.IsFlying;

        if (IsFlying)
        {
            Debug.Log("IceBullet");
            return false;
        }

        return true;
    }

    IEnumerator IceBulletCor()
    {
        yield return new WaitForEndOfFrame();
    }
}
