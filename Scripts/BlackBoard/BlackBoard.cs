using System.Collections;
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

    private Clock.FlyingTimes _flyingTime;
    public Clock.FlyingTimes FlyingTime { get { return _flyingTime; } }

    private Clock.FlyingPatternTimes _flyingPatternTime;
    public Clock.FlyingPatternTimes FlyingPatternTime { get { return _flyingPatternTime; } }

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

    private bool _isStage;
    public bool IsStage { set { _isStage = value; } get { return _isStage; } }

    private bool _isTakeOff;
    public bool IsTakeOff { set { _isTakeOff = value; } get { return _isTakeOff; } }

    /* 보스몹 이륙착륙 액션 확인 */
    private bool _isLandingAct;
    public bool IsLandingAct { set { _isLandingAct = value; } get { return _isLandingAct; } }

    private bool _isTakeOffAct;
    public bool IsTakeOffAct { set { _isTakeOffAct = value; } get { return _isTakeOffAct; } }

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

    /* fly 관련 변수 */
    [SerializeField]
    private float _takeOffLimitDir;
    public float TakeOffLimitDir { get { return _takeOffLimitDir; } }

    [SerializeField]
    private float _dragonFlyingRadius;
    public float DragonFlyingRadius { get { return _dragonFlyingRadius; } }

    /* fly 패턴 관련 변수 */
    private bool _isHovering;
    public bool IsHovering{ set { _isHovering = value; } get { return _isHovering; } }

    private bool _isFlying;
    public bool IsFlying { set { _isFlying = value; } get { return _isFlying; } }

    private bool _hoveringPatternChk;
    public bool HoveringPatternChk { set { _hoveringPatternChk = value; } get { return _hoveringPatternChk; } }

    private bool _flyingPatternChk;
    public bool FlyingPatternChk { set { _flyingPatternChk = value; } get { return _flyingPatternChk; } }

    public int BreathNum = 49;

    public void InitMamber()
    {
        _manager = GameObject.FindWithTag("Dragon").GetComponent<DragonManager>();
        _patternManager = GameObject.FindWithTag("Dragon").GetComponent<MoveManager>();

        /* 시간 클래스 초기화 */
        _clocks = GetComponentInChildren<Clock>();
        _idleTimes = _clocks.IdleTimes;
        _landPatternTime = _clocks.PatternTimes;
        _flyingTime = _clocks.Flyingtime;
        _flyingPatternTime = _clocks.FlyingPatternTime;

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
        if (fCurSpeed >= fMaxSpeed)
            return fMaxSpeed;
        else
        {
            float dir = Mathf.Sign(fMaxSpeed - fCurSpeed);
            fCurSpeed += fAccSpeed * dir * Time.fixedDeltaTime;
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

}
