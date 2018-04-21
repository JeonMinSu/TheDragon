using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBreath : DragonAction {

    public override bool Run()
    {
        bool HoveringAct = BlackBoard.Instance.HoveringAct;
        bool IsHovering = BlackBoard.Instance.IsHovering;

        float PlayerHP = BlackBoard.Instance.CurPlayerHP;
        float PlayerMaxHp = BlackBoard.Instance.PlayerMaxHP;

        int MaxCrystal = BlackBoard.Instance.MissileCrystalNum;
        int CurCrystal = BlackBoard.Instance.CurIceCrystalNum;

        float preTime = BlackBoard.Instance.GetFlyingTime().PreBreathTime;
        float afterTime = BlackBoard.Instance.GetFlyingTime().AfterBreathTime;
        float RunTime = BlackBoard.Instance.GetFlyingTime().RunBreathTime;

        CoroutineManager.SetWaitForSeconds(RunTime);

        if (IsHovering &&
            PlayerHP < PlayerMaxHp * 0.5 && 
            CurCrystal < MaxCrystal)
        {
            Debug.Log("Breath");

            if (!HoveringAct)
                CoroutineManager.DoCoroutine(BreathShot(preTime, afterTime));
            return true;
        }
        return false;
    }

    IEnumerator BreathShot(float preTime, float afterTime)
    {
        Transform Mouth = BlackBoard.Instance.DragonMouth;
        BlackBoard.Instance.FlyingAct = true;
        

        //용 브레스 선딜 애니메이션 넣는 곳
        yield return new WaitForSeconds(preTime);

        //용 브레스 실행 애니메이션 넣는 곳
        BlackBoard.Instance.BulletManager.BreathOn(Mouth);
        yield return CoroutineManager.Seconds;
        BlackBoard.Instance.BulletManager.BreathOff();

        //용 브레스 후딜 애니메이션 넣는 곳
        yield return new WaitForSeconds(afterTime);

        BlackBoard.Instance.Manager.Ani.ResetTrigger("Hovering");
        BlackBoard.Instance.HoveringAct = false;
        BlackBoard.Instance.IsHovering = false;
        BlackBoard.Instance.IsFlying = true;
        StopCoroutine(BreathShot(preTime, afterTime));
    }
}
