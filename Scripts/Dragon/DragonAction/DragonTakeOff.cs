using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTakeOff : DragonAction {

    public override bool Run()
    {
        float curDir = BlackBoard.Instance.Stat.CurTakeOffDir;
        float LimitDir = BlackBoard.Instance.TakeOffLimitDir;

        if (!BlackBoard.Instance.IsStage && curDir < LimitDir)
        {
            if (!BlackBoard.Instance.IsTakeOffAct)
                CoroutineManager.DoCoroutine(TakeOffStartCor());

            return false;
        }
        BlackBoard.Instance.IsTakeOff = false;
        BlackBoard.Instance.IsHovering = true;
        StopCoroutine(TakeOffStartCor());
        return true;
    }

    IEnumerator TakeOffStartCor()
    {
        Transform Dragon = BlackBoard.Instance.Manager.transform;
        float speed = 0.0f;
        float MaxSpeed = BlackBoard.Instance.Stat.MaxTakeOffSpeed;
        float AccSpeed = BlackBoard.Instance.Stat.AccTakeOffeSpeed;

        float LimitDir = BlackBoard.Instance.TakeOffLimitDir;
        float curDir = speed * Time.deltaTime;

        BlackBoard.Instance.IsTakeOffAct = true;

        while (curDir < LimitDir)
        {
            speed = BlackBoard.Instance.Acceleration(speed, MaxSpeed, AccSpeed);
            curDir += speed;
            BlackBoard.Instance.Stat.CurTakeOffDir = curDir;
            Dragon.Translate(Vector3.up * speed);
            yield return CoroutineManager.EndOfFrame;
        }

    }

}
