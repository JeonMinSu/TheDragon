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

        if (IsHovering &&
            PlayerHP < PlayerMaxHp * 0.5 && 
            CurCrystal < MaxCrystal)
        {
            if (!HoveringAct)
                CoroutineManager.DoCoroutine(BreathShot(preTime, afterTime));
            Debug.Log("Breath");

            return false;
        }
        return true;
    }

    IEnumerator BreathShot(float preTime, float afterTime)
    {
        Transform Player = BlackBoard.Instance.Manager.Player;
        Transform Dragon = BlackBoard.Instance.Manager.transform;

        Transform Mouth = BlackBoard.Instance.DragonMouth;
        BlackBoard.Instance.HoveringAct = true;

        Vector3 forward = Player.position - Dragon.position;

        float RunTime = BlackBoard.Instance.GetFlyingTime().RunBreathTime;
        
        //용 브레스 선딜 애니메이션 넣는 곳
        yield return new WaitForSeconds(preTime);

        BlackBoard.Instance.Manager.Ani.ResetTrigger("Hovering");
        BlackBoard.Instance.Manager.Ani.SetTrigger("Breath");

        BlackBoard.Instance.BulletManager.DragonBreathOn(Mouth.position, forward);
        //용 브레스 실행 애니메이션 넣는 곳
        //BlackBoard.Instance.BulletManager.DragonBreathOn(Mouth);
        yield return new WaitForSeconds(RunTime);
        //BlackBoard.Instance.BulletManager.DragonBreathOff();

        //용 브레스 후딜 애니메이션 넣는 곳
        yield return new WaitForSeconds(afterTime);

        BlackBoard.Instance.Manager.Ani.ResetTrigger("Breath");
        BlackBoard.Instance.HoveringAct = false;
        BlackBoard.Instance.IsHovering = false;
        BlackBoard.Instance.IsFlying = true;
        StopCoroutine(BreathShot(preTime, afterTime));
    }
}
