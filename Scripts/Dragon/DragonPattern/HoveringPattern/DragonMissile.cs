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
            if (!HoveringAct)
                CoroutineManager.DoCoroutine(MissileShot(preTime, afterTime));
            return true;
        }
        return false;
    }

    IEnumerator MissileShot(float preTime, float afterTime)
    {
        BlackBoard.Instance.HoveringAct = true;
        Transform Mouth = BlackBoard.Instance.DragonMouth;
        yield return new WaitForSeconds(preTime);
        for (int i = 0; i < 5; i++)
        {
            BlackBoard.Instance.BulletManager.HomingBulletFire(Mouth);
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(afterTime);

        BlackBoard.Instance.Manager.Ani.ResetTrigger("Hovering");
        BlackBoard.Instance.HoveringAct = false;
        BlackBoard.Instance.IsHovering = false;
        BlackBoard.Instance.IsFlying = true;
        StopCoroutine(MissileShot(preTime, afterTime));
    }

}