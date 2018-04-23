using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMissile : DragonAction
{

    public override bool Run()
    {

        bool IsHovering = BlackBoard.Instance.IsHovering;
        bool HoveringAct = BlackBoard.Instance.HoveringAct;

        float PlayerHP = BlackBoard.Instance.CurPlayerHP;
        float PlayerMaxHp = BlackBoard.Instance.PlayerMaxHP;

        int MaxCrystal = BlackBoard.Instance.MissileCrystalNum;
        int CurCrystal = BlackBoard.Instance.CurIceCrystalNum;

        float preTime = BlackBoard.Instance.GetFlyingTime().PreMissileTime;
        float afterTime = BlackBoard.Instance.GetFlyingTime().AfterMissileTime;


        if (IsHovering &&
            PlayerHP >= PlayerMaxHp * 0.5f &&
            CurCrystal < MaxCrystal)
        {
            Debug.Log("Missile");
            if (!BlackBoard.Instance.HoveringAct)
                CoroutineManager.DoCoroutine(MissileShot(preTime, afterTime));
            BlackBoard.Instance.IsFlying = true;
            return false;
        }
        return true;
    }

    IEnumerator MissileShot(float preTime, float afterTime)
    {
        Transform Mouth = BlackBoard.Instance.DragonMouth;
        BlackBoard.Instance.HoveringAct = true;

        //용 유도탄 선딜 애니메이션 넣는 곳
        yield return new WaitForSeconds(preTime);

        //용 유도탄 실행 애니메이션 넣는 곳
        for (int i = 0; i < 5; i++)
        {
            BlackBoard.Instance.BulletManager.HomingBulletFire(Mouth);
            yield return new WaitForSeconds(1.5f);
        }
        //용 유도탄 후딜 애니메이션 넣는 곳
        yield return new WaitForSeconds(afterTime);
        BlackBoard.Instance.Manager.Ani.ResetTrigger("Hovering");
        BlackBoard.Instance.IsHovering = false;
        BlackBoard.Instance.HoveringAct = false;
    }

}