using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCharacter
{
    partial class PlayerCharacterController : MonoBehaviour
    {
        enum INPUTKEY
        {
            KEY_W = 1,
            KEY_S = 2,
            KEY_A = 4,
            KEY_D = 8,
            KEY_LSHIFT = 16,
            KEY_SPACE = 32,
            MOUSE_LEFT = 64,
            MOUSE_RIGHT = 128
        }
        enum PLAYERSTATE
        {
            STAND = 1,
            MOVE = 2,
            ROLL = 4,
            FALLING = 8,
            HIT = 16,
            DEAD = 32
        }
        enum PLAYERUPDATESTATE
        {
            NONE =0,
            FLASH = 1,
            ATTACK = 2,
            JUMP = 4
        }

        //이동 관련
        public float m_TurnSpeed = 360;
        public float _AccelSpeed = 100;
        public float _BreakSped = 100;
        public float _MaxSpeed = 15;
        public float _JumpPower = 20.0f;
        public float _JumpDelay = 0.2f;
        private float  _JumpCoolTime= 0.0f;
        private bool _IsFalling = false;

        //카메라 회전
        private Vector3 CameraRot;
        private Vector3 PlayerRot;
        public float _CameraDownAngleX = 60;
        public float _CameraUpAngleX = -80;
        Vector3 _CameraEventAngle;
        Vector3 _CameraFireAngle;

        //순간이동 관련 값
        public float _FlashDistance = 10;
        public float _FlashTime = 0.1f;
        public float _FlashDelay = 3.0f;
        private float _FlashCoolTime = 0.0f;

        //키 입력값
        //int m_KeyBit = 0;
        float _PlayerRotAngle = 0;

        PLAYERSTATE _PlayerState = PLAYERSTATE.STAND;
        int _PlayerUpdateState = (int)PLAYERUPDATESTATE.NONE;
        //PLAYERUPDATESTATE _PlayerUpdateState = PLAYERUPDATESTATE.NONE;

        //머리 카메라
        public GameObject headCamera;

        //카메라 진동값
        //추후 설정 저장해서 3~4개정도 만들것
        public float ShakingPlayTime = 0.2f;
        public float ShakingWaitTime =  0.02f;
        public float ShakingRadius = 3.0f;

        //플레이어 물리
        Rigidbody rigid;

        //총매니저
        public GunManager gunManager;

        //코루틴 확인
        bool corFlash = false;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //m_BulletReroadCount = m_BulletCurrentCount;
            rigid = this.GetComponent<Rigidbody>();
            StartCoroutine("CorWalkShaking");
        }

        // Update is called once per frame
        void Update()
        {
            //캐릭터 회전값 및 키입력 상태체크
            CheckPlayerState();

            //캐릭터 머리 회전
            CameraSetRot();

            //if (Input.GetMouseButtonDown(0))
            //{
            //    StartCoroutine("CorCameraShakeVertical");
            //}
            //if (Input.GetMouseButtonDown(1))
            //{
            //    StartCoroutine("CorCameraShake");
            //}
        }

        private void LateUpdate()
        {
            PlayerStateMoveLU();   
        }
        private void FixedUpdate()
        {
            //쿨타임 체크
            CoolTimeCheck();

            PlayerStateMoveFU();
        }

        private void CoolTimeCheck()
        {
            if (_FlashCoolTime > 0) _FlashCoolTime -= Time.fixedDeltaTime;
            if (_JumpCoolTime > 0) _JumpCoolTime -= Time.fixedDeltaTime;
        }
    }
}