using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTakeOff : DragonAction {

    public override bool Run()
    {

        int MoveIndex = (int)MoveManagers.TakeOff;

        float curDir = BlackBoard.Instance.Stat.CurTakeOffDir;
        float LimitDir = BlackBoard.Instance.TakeOffLimitDir;

        bool IsTakeOff = BlackBoard.Instance.IsTakeOff;
        bool IsFlyingReady = BlackBoard.Instance.IsMoveReady(MoveIndex);
        bool IsTakeOffEnd = BlackBoard.Instance.GetNodeManager(MoveIndex).IsMoveEnd;

        if (IsTakeOff && !IsTakeOffEnd)
        {
            if (!IsFlyingReady)
                BlackBoard.Instance.FlyingMoveReady(MoveIndex);
            if (!BlackBoard.Instance.IsTakeOffAct)
                CoroutineManager.DoCoroutine(TakeOffStartCor(MoveIndex));

            BlackBoard.Instance.Clocks.InitLandTimes();
            BlackBoard.Instance.IsHovering = true;

            return false;
        }
        return true;
    }

    IEnumerator TakeOffStartCor(int Index)
    {
        Transform Dragon = BlackBoard.Instance.Manager.transform;
        float speed = 0.0f;
        float MaxSpeed = BlackBoard.Instance.Stat.MaxTakeOffSpeed;
        float AccSpeed = BlackBoard.Instance.Stat.AccTakeOffeSpeed;

        float LimitDir = BlackBoard.Instance.TakeOffLimitDir;
        float curDir = speed * Time.deltaTime;

        BlackBoard.Instance.IsTakeOffAct = true;


        while (!BlackBoard.Instance.GetNodeManager(Index).IsMoveEnd)
        {

            BlackBoard.Instance.FlyingMovement(Index);
            //speed = BlackBoard.Instance.Acceleration(speed, MaxSpeed, AccSpeed);
            //curDir += speed;
            //BlackBoard.Instance.Stat.CurTakeOffDir = curDir;
            //Dragon.Translate(Vector3.up * speed);
            yield return CoroutineManager.EndOfFrame;
        }
        BlackBoard.Instance.Manager.Ani.ResetTrigger("TakeOff");
        BlackBoard.Instance.IsTakeOff = false;
        BlackBoard.Instance.IsTakeOffAct = false;

    }

}
