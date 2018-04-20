using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLanding : DragonAction
{
    public override bool Run()
    {
        if (BlackBoard.Instance.IsLandingAct)
        { 
            Debug.Log("Landing");
            return false;
        }
        return true;
    }

    IEnumerator LandingCor()
    {
        Transform Dragon = BlackBoard.Instance.Manager.transform;

        float speed = 0.0f;
        float MaxSpeed = BlackBoard.Instance.Stat.MaxTakeOffSpeed;
        float AccSpeed = BlackBoard.Instance.Stat.AccTakeOffeSpeed;

        float LimitDir = BlackBoard.Instance.TakeOffLimitDir;
        float curDir = speed * Time.deltaTime;

        BlackBoard.Instance.IsLandingAct = true;

        while (curDir < LimitDir)
        {
            speed = BlackBoard.Instance.Acceleration(speed, MaxSpeed, AccSpeed);
            curDir += speed;
            BlackBoard.Instance.Stat.CurTakeOffDir = curDir;
            Dragon.Translate(Vector3.down * speed);
            yield return CoroutineManager.EndOfFrame;
        }

    }

}
