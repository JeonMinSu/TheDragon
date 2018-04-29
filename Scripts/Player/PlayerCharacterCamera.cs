using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerCharacter
{
    partial class PlayerCharacterController
    {    
        //카메라 회전
        private void CameraSetRot()
        {
            float x = Input.GetAxis("Mouse Y");
            float y = Input.GetAxis("Mouse X");

            CameraRot.x -= x * m_TurnSpeed * Time.deltaTime;
            PlayerRot.y += y * m_TurnSpeed * Time.deltaTime;
            CameraRot.y = PlayerRot.y;
            if (CameraRot.x > _CameraDownAngleX)
                CameraRot.x = _CameraDownAngleX;

            if (CameraRot.x < _CameraUpAngleX)
                CameraRot.x = _CameraUpAngleX;

            this.transform.rotation = Quaternion.Euler(PlayerRot);
            headCamera.transform.rotation = Quaternion.Euler(CameraRot + _CameraEventAngle + _CameraFireAngle);
        }

        //카메라 흔들리는거
        private IEnumerator CorCameraShake()
        {
            float time = ShakingPlayTime;
            while (time > 0)
            {
                Vector3 shakingPos = Random.insideUnitSphere * ShakingRadius;
                _CameraEventAngle.x = shakingPos.x * time / ShakingPlayTime;
                _CameraEventAngle.y = shakingPos.y * time / ShakingPlayTime;
                time -= ShakingWaitTime;
                yield return new WaitForSeconds(ShakingWaitTime);
            }
            _CameraEventAngle = Vector3.zero;
            yield return null;
        }

        //걸을때 위아래 흔들리는거 쭈욱~
        private IEnumerator CorWalkShaking()
        {
            float vertical = 0.1f;
            float moveSpeed = 1.0f;
            float verticalValue = 0.0f;
            bool cameraUP = true;
            Vector3 basePos = headCamera.transform.localPosition;

            while (true)
            {
                if (_PlayerState == PLAYERSTATE.MOVE)
                {
                    if (cameraUP)
                    {
                        verticalValue += Time.fixedDeltaTime * moveSpeed;
                        headCamera.transform.localPosition = basePos + Vector3.up * verticalValue;

                        if (verticalValue > vertical)
                        {
                            cameraUP = false;
                        }
                    }
                    else
                    {
                        verticalValue -= Time.fixedDeltaTime * moveSpeed;
                        headCamera.transform.localPosition = basePos + Vector3.up * verticalValue;
                        if (verticalValue < -vertical)
                        {
                            cameraUP = true;
                        }
                    }
                }
                else
                {
                    if (Mathf.Abs(verticalValue) < Time.fixedDeltaTime * moveSpeed)
                    {
                        verticalValue = 0;
                        cameraUP = true;
                    }
                    else if (verticalValue > 0 && Mathf.Abs(verticalValue) > Time.fixedDeltaTime * moveSpeed)
                    {
                        verticalValue -= Time.fixedDeltaTime * moveSpeed;
                        headCamera.transform.localPosition = basePos + Vector3.up * verticalValue;
                    }
                    else if (verticalValue < 0 && Mathf.Abs(verticalValue) > Time.fixedDeltaTime * moveSpeed)
                    {
                        verticalValue += Time.fixedDeltaTime * moveSpeed;
                        headCamera.transform.localPosition = basePos + Vector3.up * verticalValue;
                    }
                }
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator CorCameraFireShake()
        {
            float time = 0.1f;
            while (time > 0)
            {
                _CameraFireAngle.x = time / ShakingPlayTime * -1;
                time -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            _CameraEventAngle = Vector3.zero;
            yield return null;
        }

    }
}