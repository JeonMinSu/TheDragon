using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [System.Serializable]
    public class LandIdleTimes
    {
        private float _curIdleTime;
        public float CurIdleTime { set { _curIdleTime = value; } get { return _curIdleTime; } }

        [SerializeField]
        private float _maxIdleTime;
        public float MaxIdleTime { set { _maxIdleTime = value; } get { return _maxIdleTime; } }

        [SerializeField]
        private float _minChangeTime;
        public float MinChangeTime { get { return _minChangeTime; } }

        [SerializeField]
        private float _maxChangeTime;
        public float MaxChangeTime { get { return _maxChangeTime; } }

        private float _curLandWalkTime = 0.0f;
        public float CurLandWalkTime { set { _curLandWalkTime = value; } get { return _curLandWalkTime; } }

        private float _changeTime;
        public float ChangeTime { set { _changeTime = value; } get { return _changeTime; } }
    }

    [System.Serializable]
    public class LandPatternTimes
    {
        [SerializeField]
        private float _preRushDelay;
        public float PreRushDelay { get { return _preRushDelay; } }

        [SerializeField]
        private float _rushRunTime;
        public float RushRunTime { get { return _rushRunTime; } }

        [SerializeField]
        private float _afterRushDelay;
        public float AfterRushDelay { get { return _afterRushDelay; } }

        [SerializeField]
        private float _preOverLapDelay;
        public float PreOverLapDelay { get { return _preOverLapDelay; } }

        [SerializeField]
        private float _afterOverLapDelay;
        public float AfterOverLapDelay { get { return _afterOverLapDelay; } }

    }

    [System.Serializable]
    public class FlyingTimes
    {
        [SerializeField]
        private float _hoveringTime;
        public float HoveringTime { set { _hoveringTime = value; } get { return _hoveringTime; } }

        [SerializeField]
        private float _curHoveringTime;
        public float CurHoveringTime { set { _curHoveringTime = value; } get { return _curHoveringTime; } }

        [SerializeField]
        private float _flyTime;
        public float FlyTime { get { return _flyTime; } }

        [SerializeField]
        private float _curFlyTime;
        public float CurFlyTime { set { _curFlyTime = value; } get { return _curFlyTime; } }

    }

    [System.Serializable]
    public class FlyingPatternTimes
    {
        [SerializeField]
        private float _preBreathTime;
        public float PreBreathTime { set { _preBreathTime = value; } get { return _preBreathTime; } }

        [SerializeField]
        private float _afterBreathTime;
        public float AfterBreathTime { set { _afterBreathTime = value; } get { return _afterBreathTime; } }

        [SerializeField]
        private float _preMissileTime;
        public float PreMissileTime { set { _preMissileTime = value; } get { return _preMissileTime; } }

        [SerializeField]
        private float _afterMissileTime;
        public float AfterMissileTime { set { _afterMissileTime = value; }get { return _afterMissileTime; } }

    }

    [SerializeField]
    private LandPatternTimes _patternTimes;
    public LandPatternTimes PatternTimes { get { return _patternTimes; } }

    [SerializeField]
    private LandIdleTimes _idleTimes;
    public LandIdleTimes IdleTimes { get { return _idleTimes; } }

    [SerializeField]
    private FlyingTimes _flyingTime;
    public FlyingTimes Flyingtime { get { return _flyingTime; } }

    [SerializeField]
    private FlyingPatternTimes _flyingPatternTime;
    public FlyingPatternTimes FlyingPatternTime { get { return _flyingPatternTime; } }

    [SerializeField]
    private float _fallingTime;
    public float FallingTime { set { _fallingTime = value; } get { return _fallingTime; } }

    private void Awake()
    {
        IdleTimes.ChangeTime = Random.Range(IdleTimes.MinChangeTime, IdleTimes.MaxChangeTime);
    }

}
