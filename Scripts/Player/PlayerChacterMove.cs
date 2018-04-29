using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCharacter
{
    partial class PlayerCharacterController
    {
        //플레이어 움직일 키입력값 전부 확인
        private int KeyInput()
        {
            int _keyBit = 0;

            if (Input.GetKey(KeyCode.W)) { _keyBit |= (int)INPUTKEY.KEY_W; }
            else if (Input.GetKey(KeyCode.S)) { _keyBit |= (int)INPUTKEY.KEY_S; }
            if (Input.GetKey(KeyCode.A)) { _keyBit |= (int)INPUTKEY.KEY_A; }
            else if (Input.GetKey(KeyCode.D)) { _keyBit |= (int)INPUTKEY.KEY_D; }

            if (Input.GetMouseButton(0)) { _keyBit |= (int)INPUTKEY.MOUSE_LEFT; }
            if (Input.GetKeyDown(KeyCode.Space)) { _keyBit |= (int)INPUTKEY.KEY_SPACE; }
            if (Input.GetMouseButtonDown(1)) { _keyBit |= (int)INPUTKEY.MOUSE_RIGHT; }

            return _keyBit;
        }

        //키값 확인후 캐릭터 회전값 이동값 설정
        private void CheckPlayerState()
        {
            int _keyBit = KeyInput();

            if ((_keyBit & (int)INPUTKEY.KEY_SPACE) == (int)INPUTKEY.KEY_SPACE)
            {
                _PlayerUpdateState |= (int)PLAYERUPDATESTATE.JUMP;
                _keyBit -= (int)INPUTKEY.KEY_SPACE;
                
            }

            if ((_keyBit & (int)INPUTKEY.MOUSE_RIGHT) == (int)INPUTKEY.MOUSE_RIGHT)
            {
                _PlayerUpdateState |= (int)PLAYERUPDATESTATE.FLASH;
                _keyBit -= (int)INPUTKEY.MOUSE_RIGHT;               
            }
            else if ((_keyBit & (int)INPUTKEY.MOUSE_LEFT) == (int)INPUTKEY.MOUSE_LEFT)
            {
                _PlayerUpdateState |= (int)PLAYERUPDATESTATE.ATTACK;
                _keyBit -= (int)INPUTKEY.MOUSE_LEFT;
            }

            switch (_keyBit)
            {
                case (int)INPUTKEY.KEY_W: _PlayerRotAngle = 0; _PlayerState = PLAYERSTATE.MOVE; break;
                case (int)INPUTKEY.KEY_A: _PlayerRotAngle = -90; _PlayerState = PLAYERSTATE.MOVE; break;
                case (int)INPUTKEY.KEY_S: _PlayerRotAngle = 180; _PlayerState = PLAYERSTATE.MOVE; break;
                case (int)INPUTKEY.KEY_D: _PlayerRotAngle = 90; _PlayerState = PLAYERSTATE.MOVE; break;

                case (int)INPUTKEY.KEY_W | (int)INPUTKEY.KEY_A: _PlayerRotAngle = -45; _PlayerState = PLAYERSTATE.MOVE; break;
                case (int)INPUTKEY.KEY_W | (int)INPUTKEY.KEY_D: _PlayerRotAngle = 45; _PlayerState = PLAYERSTATE.MOVE; break;
                case (int)INPUTKEY.KEY_S | (int)INPUTKEY.KEY_A: _PlayerRotAngle = -135; _PlayerState = PLAYERSTATE.MOVE; break;
                case (int)INPUTKEY.KEY_S | (int)INPUTKEY.KEY_D: _PlayerRotAngle = 135; _PlayerState = PLAYERSTATE.MOVE; break;

                default: _PlayerState = PLAYERSTATE.STAND; break;
            }
        }

        private void PlayerStateMoveLU()
        {
            if ((_PlayerUpdateState & (int)PLAYERUPDATESTATE.FLASH) == (int)PLAYERUPDATESTATE.FLASH && corFlash == false && _FlashCoolTime <= 0)
            {
                StartCoroutine("CorFlash");
            }
            if ((_PlayerUpdateState & (int)PLAYERUPDATESTATE.JUMP) == (int)PLAYERUPDATESTATE.JUMP && _JumpCoolTime <= 0.0f && !_IsFalling)
            {
                rigid.AddForce(Vector3.up * _JumpPower, ForceMode.VelocityChange);
                _JumpCoolTime = _JumpDelay;
            }
            if ((_PlayerUpdateState & (int)PLAYERUPDATESTATE.ATTACK) == (int)PLAYERUPDATESTATE.ATTACK)
            {
                bool isFire = gunManager.Fire();
                if(isFire)
                StartCoroutine("CorCameraFireShake");
            }
            //if ((_PlayerUpdateState == PLAYERUPDATESTATE.FLASH) && corFlash == false &&  _FlashCoolTime <= 0)
            //{
            //    StartCoroutine("CorFlash");
            //    //_PlayerUpdateState = PLAYERUPDATESTATE.NONE;
            //}

            // if (_PlayerUpdateState == PLAYERUPDATESTATE.JUMP && _JumpCoolTime <= 0.0f && !_IsFalling)
            //{
            //    Debug.Log("JUMP");
            //    rigid.AddForce(Vector3.up * _JumpPower, ForceMode.VelocityChange);
            //    //_PlayerUpdateState = PLAYERUPDATESTATE.NONE;
            //    _JumpCoolTime = _JumpDelay;
            //}

            //if (_PlayerUpdateState == PLAYERUPDATESTATE.ATTACK)
            //{
            //    gunManager.Fire();
            //}

            _PlayerUpdateState = (int)PLAYERUPDATESTATE.NONE;
        }

        private void PlayerStateMoveFU()
        {
            if (IsFalling())
            {
                rigid.velocity += Physics.gravity * Time.fixedDeltaTime * 2;
                _IsFalling = true;
            }
            else
            {
                _IsFalling = false;
            }

            if (_PlayerState == PLAYERSTATE.MOVE)
            {
                Vector3 v = Quaternion.AngleAxis(_PlayerRotAngle, Vector3.up) * this.transform.forward * Time.fixedDeltaTime * _AccelSpeed;
                rigid.velocity += v;
                Vector3 velocity = rigid.velocity;
                float yValue = velocity.y;
                velocity.y = 0;
                if (velocity.sqrMagnitude > _MaxSpeed * _MaxSpeed)
                {
                    rigid.velocity = velocity.normalized * _MaxSpeed;
                    rigid.velocity += Vector3.up * yValue;
                }
            }
            else if(_PlayerState == PLAYERSTATE.STAND)
            {
                if (rigid.velocity.sqrMagnitude > _BreakSped * _BreakSped * Time.fixedDeltaTime * Time.fixedDeltaTime && !_IsFalling)
                {
                    rigid.velocity -= rigid.velocity.normalized * _BreakSped * Time.fixedDeltaTime;
                }
            }
        }

        private bool IsFalling()
        {
            CapsuleCollider capCol = GetComponent<CapsuleCollider>();
            Ray ray = new Ray(this.transform.position +capCol.center, Vector3.down);
            if(Physics.SphereCast(ray,capCol.radius * 0.95f,capCol.height * 0.5f ))
            {
                return false;
            }
            else
            {
                return true;                
            }
        }

        IEnumerator CorFlash()
        {
            corFlash = true;
            _FlashCoolTime = _FlashDelay;

            CapsuleCollider capCol = GetComponent<CapsuleCollider>();

            Vector3 dir = Quaternion.AngleAxis(_PlayerRotAngle, Vector3.up) * this.transform.forward;
            float distance = 0;
            Vector3 savePosition = this.transform.position;
            float saveView = headCamera.GetComponent<Camera>().fieldOfView;

            while (distance < _FlashDistance)
            {
                headCamera.GetComponent<Camera>().fieldOfView += 1;
                distance += _FlashDistance / _FlashTime * Time.fixedDeltaTime;
                Vector3 p1 = transform.position - (dir * 0.5f) + capCol.center + Vector3.up * -capCol.height * 0.4f;
                Vector3 p2 = p1 + Vector3.up * capCol.height * 0.9f;

                RaycastHit hit;
                if (Physics.CapsuleCast(p1, p2, capCol.radius, dir, out hit, distance))
                {
                    this.transform.position = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);
                    Debug.Log("Hit Object");
                    break;
                }
                else
                {
                    this.transform.position = savePosition + dir * distance;
                }
                yield return new WaitForEndOfFrame();
            }
            headCamera.GetComponent<Camera>().fieldOfView = saveView;
            corFlash = false;
            yield return null;
        }
    }
}