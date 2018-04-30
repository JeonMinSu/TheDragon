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

            Debug.Log("Hovering");

            Dragon.rotation =
                Quaternion.Lerp(
                    Dragon.rotation,
                    Quaternion.LookRotation(forward + (Vector3.up * 0.4f)),
                    CurTime / MaxTime * 0.5f);

            CurTime += Time.deltaTime;

            BlackBoard.Instance.GetFlyingTime().CurHoveringTime = CurTime;
            BlackBoard.Instance.Manager.Ani.SetTrigger("Hovering");

            return Quaternion.Equals(Dragon.rotation, Quaternion.LookRotation(forward));
        }
        return true;
    }
}