﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard : Singleton<BlackBoard>
{
    private DragonManager _manager;
    public DragonManager Manager { get { return _manager; } }

    private MoveManager _patternManager;
    public MoveManager PatternManager { get { return _patternManager; } }

    private Clock _clocks;
    public Clock Clocks { get { return _clocks; } }

    private Clock.LandIdleTimes _idleTimes;
    public Clock.LandIdleTimes IdleTimes { get { return _idleTimes; } }

    private Clock.LandPatternTimes _landPatternTime;
    public Clock.LandPatternTimes LandPatternTime { get { return _landPatternTime; } }

    private DragonStat _stat;
    public DragonStat Stat { get { return _stat; } }

    public DragonPhases CurrentPhase;

    /* 보스몹 상태 관련 변수 */
    [SerializeField]
    private float _maxHpTakeOffPercent;
    public float MaxHpTakeOffPercent { get { return _maxHpTakeOffPercent; } }

    [SerializeField]
    private float _hpTakeOff;
    public float HpTakeOff { set { _hpTakeOff = value; } get { return _hpTakeOff; } }

    [SerializeField]
    private float _maxHpLandPercent;
    public float MaxHpLandPercent { get { return _maxHpLandPercent; } }

    [SerializeField]
    private float _hpLand;
    public float HpLand { set { _hpLand = value; } get { return _hpLand; } }

    [SerializeField]
    private bool _isStage;
    public bool IsStage { set { _isStage = value; } get { return _isStage; } }

    [SerializeField]
    private bool _isFlying;
    public bool IsFlying { set { _isFlying = value; } get { return _isFlying; } }

    /* 보스몹 페이즈 관련 변수 */
    [SerializeField]
    private float _hpPhaseSecond;
    public float HpPhaseSecond { set { _hpPhaseSecond = value; } get { return _hpPhaseSecond; } }

    [SerializeField]
    private float _hpPhaseThird;
    public float HpPhaseThird { set { _hpPhaseThird = value; } get { return _hpPhaseThird; } }


    /* 보스몹 이동 관련 변수*/
    private float _radius;
    public float Radius { set { _radius = value; } get { return _radius; } }

    private float _theta;
    public float Theta { set { _theta = value; } get { return _theta; } }

    private bool _isRadiusChk;
    public bool IsRadiusChk { set { _isRadiusChk = value; } get { return _isRadiusChk; } }

    /* 보스몹 패턴 관련 변수 */
    [SerializeField]
    private float _rushLimitDir;
    public float RushLimitDir { set { _rushLimitDir = value; } get { return _rushLimitDir; } }

    private bool _isPatternChk;
    public bool IsPatternChk { set { _isPatternChk = value; } get { return _isPatternChk; } }

    private Vector3 _fixTargetPos;
    public Vector3 FixTargetPos { set { _fixTargetPos = value; } get { return _fixTargetPos; } }

    /* 날기 관련 변수 */
    private float _takeOffLimitDir;
    public float TakeOffLimitDir { get { return _takeOffLimitDir; } }
    
    public void InitMamber()
    {
        _manager = GameObject.FindWithTag("Dragon").GetComponent<DragonManager>();
        _patternManager = GameObject.FindWithTag("Dragon").GetComponent<MoveManager>();

        /* 시간 클래스 초기화 */
        _clocks = GetComponentInChildren<Clock>();
        _idleTimes = _clocks.IdleTimes;
        _landPatternTime = _clocks.PatternTimes;
        
        /* 스텟 초기화 */
        _stat = _manager.Stat;


        _hpTakeOff = _stat.MaxHP * _maxHpTakeOffPercent;
        _hpLand = _stat.MaxHP * _maxHpLandPercent;


        SetPhase(DragonPhases.FirstPhase);
    }

    public void SetPhase(DragonPhases _newPhase)
    {
        CurrentPhase = _newPhase;
    }

    public void Move(Transform Target, float MoveSpeed, float TurnSpeed)
    {
        Transform Dragon = Manager.transform;

        if (Dragon.position != Target.position)
        {
            Vector3 forward = Target.position - Dragon.position;

            if (forward != Vector3.zero)
            {
                Dragon.rotation =
                    Quaternion.RotateTowards(
                        Dragon.rotation,
                        Quaternion.LookRotation(forward),
                        TurnSpeed * Time.deltaTime);

                Vector3 nextPos =
                    Vector3.MoveTowards(
                        Dragon.position,
                        Target.position,
                        MoveSpeed * Time.deltaTime);

                Dragon.position = nextPos;

            }
        }


    }

    public void CircleMove(Vector3 Target, float Radian)
    {
        Transform dragon = Manager.transform;

        Vector3 circlePos = GetCirclePos(Target, Radian);
        Vector3 forward = (Target - dragon.position).normalized;

        dragon.position = circlePos;

        dragon.rotation = Quaternion.LookRotation(forward);

    }

    public float Acceleration(float fCurSpeed, float fMaxSpeed, float fAccSpeed)
    {
        if (fCurSpeed == fMaxSpeed)
            return fMaxSpeed;
        else
        {
            float dir = Mathf.Sign(fMaxSpeed - fCurSpeed);
            fCurSpeed += fAccSpeed * dir * Time.deltaTime;
            return (dir == Mathf.Sign(fMaxSpeed - fCurSpeed)) ? fCurSpeed : fMaxSpeed;
        }

    }

    public Vector3 GetCirclePos(Vector3 Target, float Radian)
    {
        Vector3 retPos;

        Transform dragon = Manager.transform;

        float x = Mathf.Cos(Radian) * Radius + Target.x;
        float z = Mathf.Sin(Radian) * Radius + Target.z;

        retPos = new Vector3(x, dragon.position.y, z);
        return retPos;

    }

    public bool DistanceCalc(Transform _this, Transform _target, float Range)
    {
        if (Vector3.Distance(_this.position, _target.position) <= Range)
            return true;

        return false;
    }

    public void startPattern(IEnumerator cor) {
        CoroutineManager.DoCoroutine(cor);
    }

}
