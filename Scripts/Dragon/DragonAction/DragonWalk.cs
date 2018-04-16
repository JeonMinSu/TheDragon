using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWalk : DragonAction
{
    public override bool Run()
    {
        Transform Dragon = BlackBoard.Instance.Manager.transform;
        Transform Player = BlackBoard.Instance.Manager.Player;
        
        float MaxTime = BlackBoard.Instance.IdleTimes.ChangeTime;

        if (BlackBoard.Instance.IdleTimes.CurLandWalkTime <  MaxTime)
        {
            float moveSpeed = BlackBoard.Instance.Stat.MoveSpeed;
            float turnSpeed = BlackBoard.Instance.Stat.LandTurnSpeed;

            bool IsRadiusChk = BlackBoard.Instance.IsRadiusChk;
            Debug.Log("Walk");

            if (!BlackBoard.Instance.DistanceCalc(Dragon, Player, 30.0f))
            {
                BlackBoard.Instance.IsRadiusChk = false;
                BlackBoard.Instance.Move(Player, moveSpeed, turnSpeed);
            }
            else
            {

                if (!IsRadiusChk)
                {
                    Vector3 fixTransPos = new Vector3();

                    fixTransPos = Player.position;

                    Vector3 forward = (Player.position - Dragon.position).normalized;
                    Dragon.rotation = Quaternion.LookRotation(forward);

                    BlackBoard.Instance.Radius =
                        Vector3.Distance(Dragon.position, fixTransPos);

                    float Dot = Vector3.Dot(Vector3.left, forward);

                    if (Dot >= Mathf.Cos(Mathf.Deg2Rad * 180.0f * 0.5f))
                    {
                        Vector3 cross = Vector3.Cross(Vector3.left, forward);
                        float Result = Vector3.Dot(cross, Vector3.up);

                        if (Result >= 0)
                        {
                            forward = (Dragon.position - Player.position).normalized;
                            Dot = Vector3.Dot(Vector3.left, forward);
                            BlackBoard.Instance.Theta = Mathf.Acos(Dot) + Mathf.PI;
                        }
                        else { BlackBoard.Instance.Theta = Mathf.Acos(Dot);  }
                    }
                    else
                    {
                        Vector3 cross = Vector3.Cross(Vector3.left, forward);
                        float Result = Vector3.Dot(cross, Vector3.up);

                        if (Result >= 0)
                        {
                            forward = (Dragon.position - Player.position).normalized;
                            Dot = Vector3.Dot(Vector3.left, forward);
                            BlackBoard.Instance.Theta = Mathf.Acos(Dot) + Mathf.PI;
                        }
                        else { BlackBoard.Instance.Theta = Mathf.Acos(Dot); }

                    }

                    BlackBoard.Instance.IsRadiusChk = true;
                    BlackBoard.Instance.FixTargetPos = fixTransPos;

                }

                BlackBoard.Instance.CircleMove(BlackBoard.Instance.FixTargetPos,
                BlackBoard.Instance.Theta);

                float angleSpeed = moveSpeed * Mathf.Deg2Rad * Time.deltaTime;

                BlackBoard.Instance.Theta += angleSpeed;

                BlackBoard.Instance.IdleTimes.CurLandWalkTime += Time.deltaTime;

            }
            return false;
        }
        return true;
    }

}
