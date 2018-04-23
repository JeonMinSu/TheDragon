using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonIceBullet : DragonAction {

    public override bool Run()
    {
        bool IsFlying = BlackBoard.Instance.IsFlying;

        int MaxCrystal = BlackBoard.Instance.MaxIceBulletCrystalNum;
        int MinCrystal = BlackBoard.Instance.MinIceBulletCrtystalNum;
        int CurCrystal = BlackBoard.Instance.CurIceCrystalNum;

        if (IsFlying && MaxCrystal >= CurCrystal && CurCrystal > MinCrystal)
        {
            Debug.Log("IceBullet");
            return false;
        }

        return true;
    }

    IEnumerator IceBulletCor(float preTime, float AfterTime)
    {

        //얼음 탄환 선딜 애니메이션 넣는 곳
        yield return new WaitForSeconds(preTime);

        yield return new WaitForEndOfFrame();

        //얼음 탄환 후딜 애니메이션 넣는 곳
        yield return new WaitForSeconds(AfterTime);
    }

}
