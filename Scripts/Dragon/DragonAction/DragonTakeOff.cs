using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTakeOff : DragonAction {

    public override bool Run()
    {
        if (!BlackBoard.Instance.IsFlying)
        {
            CoroutineManager.DoCoroutine(TakeOffStartCor());
            return false;
        }
        return true;
    }

    IEnumerator TakeOffStartCor()
    {
        Transform Dragon = BlackBoard.Instance.Manager.transform;

        float speed = BlackBoard.Instance.Stat.CurRushSpeed;
        float MaxSpeed = BlackBoard.Instance.Stat.MaxTakeOffSpeed;
        float AccSpeed = BlackBoard.Instance.Stat.AccTakeOffeSpeed;

        float LimitDir = BlackBoard.Instance.TakeOffLimitDir;
        float curDir = speed * Time.deltaTime;

        BlackBoard.Instance.IsFlying = true;

        while (curDir < LimitDir)
        {
            BlackBoard.Instance.Stat.CurRushSpeed =
                BlackBoard.Instance.Acceleration(
                    speed, MaxSpeed, AccSpeed);

            speed = BlackBoard.Instance.Stat.CurRushSpeed;

            curDir = speed * Time.deltaTime;

            Dragon.Translate(Vector3.up * speed * Time.deltaTime);
            yield return CoroutineManager.Instance.EndOfFrame;
        }

    }

}
