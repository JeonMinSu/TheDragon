using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHover : DragonAction
{

    public override bool Run()
    {
        Transform Player = BlackBoard.Instance.Manager.Player;
        Transform Dragon = BlackBoard.Instance.Manager.transform;

        float MaxTime = BlackBoard.Instance.GetFlyingTime().HoveringTime;
        float CurTime = BlackBoard.Instance.GetFlyingTime().CurHoveringTime;

        bool IsHovering = BlackBoard.Instance.IsHovering;
        //if (CurTime < MaxTime && IsHovering)

        if (IsHovering && CurTime < MaxTime)
        {
            Vector3 forward = (Player.position - Dragon.position).normalized;

            //기본값 = 기본값 + (바뀔값 - 기본값) / (2 이상의 숫자) 
            Debug.Log("Hovering");

            Quaternion rotation =
                Quaternion.Lerp(
                    Dragon.rotation,
                    Quaternion.LookRotation(forward),
                    CurTime / MaxTime);


            Dragon.rotation =
                Quaternion.RotateTowards(
                    Dragon.rotation,
                    rotation,
                    90.0f * Time.deltaTime
                    );

            CurTime += Time.deltaTime;
            BlackBoard.Instance.GetFlyingTime().CurHoveringTime = CurTime;
            BlackBoard.Instance.Manager.Ani.SetTrigger("Hovering");

            //return false;
            return Quaternion.Equals(Dragon.rotation, Quaternion.LookRotation(forward));
        }
        return true;
    }
}