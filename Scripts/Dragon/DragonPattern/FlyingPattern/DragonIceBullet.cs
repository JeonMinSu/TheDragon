using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonIceBullet : DragonAction {

    public override bool Run()
    {

        Transform Player = BlackBoard.Instance.Manager.Player;
        Transform Dragon = BlackBoard.Instance.Manager.transform;

        bool IsFlying = BlackBoard.Instance.IsFlying;
        bool IsFlyingPatternAct = BlackBoard.Instance.FlyingPatternAct;

        int MaxCrystal = BlackBoard.Instance.MaxIceBulletCrystalNum;
        int MinCrystal = BlackBoard.Instance.MinIceBulletCrtystalNum;
        int CurCrystal = BlackBoard.Instance.CurIceCrystalNum;

        float preTime = BlackBoard.Instance.GetFlyingTime().PreIceBulletTime;
        float afterTime = BlackBoard.Instance.GetFlyingTime().AfterIceBulletIime;


        if (IsFlying &&
            MaxCrystal >= CurCrystal &&
            CurCrystal > MinCrystal)
        {
            Vector3 forward = (Player.position - Dragon.position).normalized;

            Dragon.rotation =
                Quaternion.RotateTowards(
                    Dragon.rotation,
                    Quaternion.LookRotation(forward),
                    90.0f * Time.deltaTime
                    );

            if (!IsFlyingPatternAct)
                CoroutineManager.DoCoroutine(IceBulletCor(preTime, afterTime));
            return false;
        }
        return true;
    }

    IEnumerator IceBulletCor(float PreTime, float AfterTime)
    {
        Transform Player = BlackBoard.Instance.Manager.Player;
        Transform Dragon = BlackBoard.Instance.Manager.transform;

        Transform Mouth = BlackBoard.Instance.DragonMouth;
        BlackBoard.Instance.FlyingPatternAct = true;

        Vector3 firePos = (Player.position - Mouth.position).normalized;

        float RunTime = BlackBoard.Instance.GetFlyingTime().RunIceBulletTime;

        //얼음 탄환 선딜 애니메이션 넣는 곳
        yield return new WaitForSeconds(PreTime);

        //얼음 탄환 공격 애니메이션 넣는 곳
        for (int i = 0; i < 5; i++)
        {
            BlackBoard.Instance.BulletManager.DragonBaseBulletFire(Mouth.position, firePos);
            yield return new WaitForSeconds(1.5f);
        }

        yield return new WaitForSeconds(RunTime);

        //얼음 탄환 후딜 애니메이션 넣는 곳
        yield return new WaitForSeconds(AfterTime);
        BlackBoard.Instance.FlyingPatternAct = true;
    }

}
