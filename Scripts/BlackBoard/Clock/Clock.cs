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

    [SerializeField]
    private LandPatternTimes _patternTimes;
    public LandPatternTimes PatternTimes { get { return _patternTimes; } }

    [SerializeField]
    private LandIdleTimes _idleTimes;
    public LandIdleTimes IdleTimes { get { return _idleTimes; } }

    [SerializeField]
    private float _fallingTime;
    public float FallingTime { set { _fallingTime = value; } get { return _fallingTime; } }

    private void Awake()
    {
        IdleTimes.ChangeTime = Random.Range(IdleTimes.MinChangeTime, IdleTimes.MaxChangeTime);
    }

}
