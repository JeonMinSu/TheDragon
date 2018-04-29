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

        if (IsHovering && CurTime < MaxTime)
        {
            Vector3 forward = (Player.position - Dragon.position).normalized;

            Dragon.rotation =
                Quaternion.Lerp(
                    Dragon.rotation,
                    Quaternion.LookRotation(forward),
                    CurTime / MaxTime * 0.5f);

            CurTime += Time.deltaTime;

            BlackBoard.Instance.GetFlyingTime().CurHoveringTime = CurTime;
            BlackBoard.Instance.Manager.Ani.SetTrigger("Hovering");

            return false;
        }
        return true;
    }
}