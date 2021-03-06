﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHomingBullet : DragonAction
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
            if (!BlackBoard.Instance.HoveringAct)
                CoroutineManager.DoCoroutine(MissileShot(preTime, afterTime));
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
        BlackBoard.Instance.BulletManager.DragonHomingBulletFire(Mouth.position, (Mouth.forward + Mouth.right * 5 + Vector3.up * 1).normalized);//우
        BlackBoard.Instance.BulletManager.DragonHomingBulletFire(Mouth.position, (Mouth.forward - Mouth.right * 5 + Vector3.up * 1).normalized);//좌
            
        BlackBoard.Instance.BulletManager.DragonHomingBulletFire(Mouth.position, (Mouth.forward + Mouth.right * 1.5f - Vector3.up).normalized);//아 우
        BlackBoard.Instance.BulletManager.DragonHomingBulletFire(Mouth.position, (Mouth.forward - Mouth.right * 1.5f - Vector3.up).normalized);//아 좌

        BlackBoard.Instance.BulletManager.DragonHomingBulletFire(Mouth.position, (Mouth.forward + Mouth.right * 1.0f + Vector3.up * 1).normalized);//위 우
        BlackBoard.Instance.BulletManager.DragonHomingBulletFire(Mouth.position, (Mouth.forward - Mouth.right * 1.0f + Vector3.up * 1).normalized);//위 좌

        BlackBoard.Instance.BulletManager.DragonHomingBulletFire(Mouth.position, (Mouth.forward + Mouth.right * 0.5f - Vector3.up).normalized);//아 우
        BlackBoard.Instance.BulletManager.DragonHomingBulletFire(Mouth.position, (Mouth.forward - Mouth.right * 0.5f - Vector3.up).normalized);//아 좌

        BlackBoard.Instance.BulletManager.DragonHomingBulletFire(Mouth.position, (Mouth.forward + Vector3.up * 2).normalized);//위
        BlackBoard.Instance.BulletManager.DragonHomingBulletFire(Mouth.position, (Mouth.forward - Vector3.up).normalized);//아래

        //BlackBoard.Instance.BulletManager.DragonHomingBulletFire(Mouth.position, (Mouth.forward + Vector3.up * 3).normalized);
        //BlackBoard.Instance.BulletManager.DragonHomingBulletFire(Mouth.position, (Mouth.forward + Mouth.right * 2 + Vector3.up * 2).normalized);
        //BlackBoard.Instance.BulletManager.DragonHomingBulletFire(Mouth.position, (Mouth.forward - Mouth.right * 2 + Vector3.up * 2).normalized);

        yield return new WaitForSeconds(1.5f);

        //용 유도탄 후딜 애니메이션 넣는 곳
        yield return new WaitForSeconds(afterTime);
        BlackBoard.Instance.Manager.Ani.ResetTrigger("Hovering");
        BlackBoard.Instance.IsHovering = false;
        BlackBoard.Instance.HoveringAct = false;
        BlackBoard.Instance.IsFlying = true;
    }


    

}