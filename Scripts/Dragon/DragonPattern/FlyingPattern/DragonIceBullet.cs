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
        Transform Player = BlackBoard.Instance.Manager.Player;
        Transform Dragon = BlackBoard.Instance.Manager.transform;

        Transform Mouth = BlackBoard.Instance.DragonMouth;
        BlackBoard.Instance.FlyingPatternAct = true;

        Vector3 forward = (Player.position - Mouth.position).normalized;

        float RunTime = BlackBoard.Instance.GetFlyingTime().RunIceBulletTime;

        //얼음 탄환 선딜 애니메이션 넣는 곳
        yield return new WaitForSeconds(preTime);

        //얼음 탄환 공격 애니메이션 넣는 곳
        BlackBoard.Instance.BulletManager.DragonBaseBulletFire(Mouth);
        yield return new WaitForSeconds(RunTime);

        //얼음 탄환 후딜 애니메이션 넣는 곳
        yield return new WaitForSeconds(AfterTime);
    }

}
