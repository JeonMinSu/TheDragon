using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveManagers
{
    TakeOff = 0,
    FlyingCircle
}


public class BlackBoard : Singleton<BlackBoard>
{

    private DragonManager _manager;
    public DragonManager Manager { get { return _manager; } }

    public MoveManager MoveManager;

    [SerializeField]
    private BulletManager _bulletManager;
    public BulletManager BulletManager { get { return _bulletManager; } }

    [SerializeField]
    private Transform _dragonMouth;
    public Transform DragonMouth { get { return _dragonMouth; } }

    private Clock _clocks;
    public Clock Clocks { get { return _clocks; } }

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

    private Vector3 _fixTargetPos;
    public Vector3 FixTargetPos { set { _fixTargetPos = value; } get { return _fixTargetPos; } }

    /* fly 관련 변수 */
    [SerializeField]
    private float _takeOffLimitDir;
    public float TakeOffLimitDir { get { return _takeOffLimitDir; } }

    [SerializeField]
    private float _dragonFlyingRadius;
    public float DragonFlyingRadius { get { return _dragonFlyingRadius; } }

    /* 보스몹 행동 관련 변수 */
    private bool _isStage;      //땅에 있는지
    public bool IsStage { set { _isStage = value; } get { return _isStage; } }

    private bool _isLanding;    //착륙 중인지
    public bool IsLanding { set { _isLanding = value; } get { return _isLanding; } }

    private bool _isIdle;       //아이들 중인지
    public bool IsIdle { set { _isIdle = value; } get { return _isIdle; } }

    private bool _isWalk;       //걷는 중인지
    public bool IsWalk { set { _isWalk = value; }  get { return _isIdle; } }

    private bool _isTakeOff;    //이륙 중인지
    public bool IsTakeOff { set { _isTakeOff = value; } get { return _isTakeOff; } }

    private bool _isHovering;   //호버링 중인지
    public bool IsHovering{ set { _isHovering = value; } get { return _isHovering; } }

    private bool _isFlying;     //날고 있는지
    public bool IsFlying { set { _isFlying = value; } get { return _isFlying; } }

    /* 보스몹 행동 체크 */
    private bool _isStageAct;   //땅에서 패턴을 사용하고 있는지
    public bool IsStageAct { set { _isStageAct = value; } get { return _isStageAct; } }

    private bool _isLandingAct; //착륙 액션을 하고 있는지
    public bool IsLandingAct { set { _isLandingAct = value; } get { return _isLandingAct; } }

    private bool _isTakeOffAct; //이륙 액션을 하고 있는지
    public bool IsTakeOffAct { set { _isTakeOffAct = value; } get { return _isTakeOffAct; } }

    private bool _hoveringAct;  //호버링 패턴을 사용하고 있는지
    public bool HoveringAct { set { _hoveringAct = value; } get { return _hoveringAct; } }

    private bool _flyingAct;    //플라잉 패턴을 사용하고 있는지
    public bool FlyingAct { set { _flyingAct = value; } get { return _flyingAct; } }


    /* 현재 얼음결정 개수 */
    [SerializeField]
    private int _curIceCrystalNum;
    public int CurIceCrystalNum { set { _curIceCrystalNum = value; } get { return _curIceCrystalNum; } }

    /* IceBullet(얼음탄환) 얼음 결정 개수 */
    [SerializeField]
    private int _maxIceBulletCrystalNum;
    public int MaxIceBulletCrystalNum { set { _maxIceBulletCrystalNum = value; } get { return _maxIceBulletCrystalNum; } }

    [SerializeField]
    private int _minIceBulletCrystalNum;
    public int MinIceBulletCrtystalNum { set { _minIceBulletCrystalNum = value; } get { return _minIceBulletCrystalNum; } }

    /* Missile(유도탄) 얼음 결정 개수 */
    [SerializeField]
    private int _missileCrystalNum;
    public int MissileCrystalNum { set { _missileCrystalNum = value; } get{ return _missileCrystalNum; } }

    [SerializeField]
    private int _breathCrystalNum;
    public int BreathCrystalNum { set { _breathCrystalNum = value; }  get { return _breathCrystalNum; } }

    /* 나중에 지워야 됨!!! */
    public float PlayerMaxHP;
    public float CurPlayerHP;
    
    public void InitMamber()
    {
        _manager = GameObject.FindWithTag("Dragon").GetComponent<DragonManager>();
        _isStage = true;;

        /* 시간 클래스 초기화 */
        _clocks = GetComponentInChildren<Clock>();

        /* 스텟 초기화 */
        _stat = _manager.Stat;

        _hpTakeOff = _stat.MaxHP * _maxHpTakeOffPercent;
        _hpLand = _stat.MaxHP * _maxHpLandPercent;

        SetPhase(DragonPhases.FirstPhase);
    }

    public DragonStat GetDragonStat()
    {
        return _manager.Stat;
    }

    public Clock.LandTimes GetLandTime()
    {
        return _clocks.LandTime;
    }

    public Clock.FlyingTimes GetFlyingTime()
    {
        return _clocks.Flyingtime;
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

    public NodeManager GetNodeManager(int Index)
    {
        return MoveManager.Manager[Index];
    }

    public bool IsMoveReady(int Index)
    {
        return GetNodeManager(Index).IsMoveReady;
    }

    public void FlyingMoveReady(int Index)
    {
        MoveManager.MoveMentReady(Index);
    }

    public void FlyingMovement(int Index)
    {
        MoveManager.NodeMovement(Index);
    }

    public void HoveringPatternChk()
    {
        if (CurPlayerHP >= PlayerMaxHP * 0.5f &&
            CurIceCrystalNum < MissileCrystalNum)
        {
            IsFlying = false;
            FlyingAct = false;
            IsHovering = true;
            return;
        }

        if (CurPlayerHP < PlayerMaxHP * 0.5 &&
            CurIceCrystalNum < BreathCrystalNum)
        {
            IsFlying = false;
            FlyingAct = false;
            IsHovering = true;
            return;
        }

    }

}
