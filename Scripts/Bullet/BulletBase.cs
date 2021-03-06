﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{

    //총알에 필요한 기본적인 값
    private float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    private float _moveTime;
    protected float MoveTime { get { return _moveTime; } }
    public void SetTime(float _time) { _moveTime = _time; }
    private Vector3 _basePosition;
    protected Vector3 BasePosition { get { return _basePosition; } }
    private Transform _baseTarget;
    public void SetbaseTarget(Transform target) { _baseTarget = target; }

    private float _delayTime = 0.0f;
    public void SetDelayTime(float delay) { _delayTime = delay; }


    //총알 방향
    private Vector3 _bulletForward;
    protected Vector3 BulletForward { get { return _bulletForward; } set { _bulletForward = value; } }
    private Vector3 _bulletRight;
    protected Vector3 BulletRight { get { return _bulletRight; } }
    private Vector3 _bulletUp;
    protected Vector3 BulletUp { get { return _bulletUp; } }

    ////시작시 보정값 (위치, 회전값)
    //private Quaternion _bulletRot;
    //private Vector3 _firePosCorrect;

    //rotate, curve를 반대로 시작할껀지
    protected bool _isRevers = false;
    public void Revers() { _isRevers = true; }

    //누가 만들어낸 총알인지
    protected string _tag = "Player";
    //public string Tag {  get { return _tag; } set { _tag = value; } }
    public void SetTag(string tag) { _tag = tag; }

    //이전 위치저장
    private Vector3 _prevPosition;
    protected Vector3 PrevPosition { get { return _prevPosition; } set { _prevPosition = value; } }

    public GameObject CollisionSound;


    protected GameObject Player;
    public void SetPlayer(GameObject _player)
    {
        Player = _player;
    }

    //총 방향 설정해주는 구간
    void SetDirection(Transform _firePos)
    {
        _bulletForward = _firePos.forward;
        _bulletRight = _firePos.right;
        _bulletUp = _firePos.up;
    }

    //각 총알 기본 값 넣어줌
    public virtual void SetBulletValue(Transform firePos, float moveSpeed)
    {
        SetBaseValue(firePos, moveSpeed);
    }
    public virtual void SetBulletValue(Vector3 firePos, Vector3 fireDir, float moveSpeed)
    {
        SetBaseValue(firePos, fireDir, moveSpeed);
    }

    //기본값 넣어주는 함수들
    protected void SetBaseValue(Transform firePos, float moveSpeed)
    {
        SetDirection(firePos);
        _moveSpeed = moveSpeed;
        _basePosition = this.transform.position;
        StartSettingBullet();
    }
    protected void SetBaseValue(Vector3 firePos, Vector3 fireDir, float moveSpeed)
    {
        _bulletForward = fireDir.normalized;
        _bulletRight = Vector3.right;
        _bulletUp = Vector3.up;
        _moveSpeed = moveSpeed;
        _basePosition = firePos;
        StartSettingBullet();
    }

    //초기 시작할때 추가로 값을 넣어주고 싶은것들은 여기서 작업한다.
    public virtual void StartSettingBullet()
    {
        this.transform.rotation = Quaternion.LookRotation(_bulletForward, Vector3.up);
    }

    public void SetBulletSpeed(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }


    //총알 기본 위치값, 이동거리 업데이트
    protected void UpdateBulletValue()
    {
        if(_baseTarget != null)
        {
            _basePosition = _baseTarget.position;
        }
        else
        {
            _basePosition += _bulletForward * _moveSpeed * Time.fixedDeltaTime;
        }

        _moveTime += Time.fixedDeltaTime;
        PrevPosition = transform.position;
    }

    //각 총에 특성에 맞게 마지막 포지션값 적용하는 구문
    protected virtual void UpdateBulletPos()
    {
        UpdateBulletValue();
        this.transform.position = _basePosition;
    }

    protected virtual void BulletDestroy()
    {
        GameObject.Destroy(this.gameObject);
    }

    //총이 맞는지 안맞는지 체크하는 구문
    protected virtual void BulletCollisionCheck()
    {
        Vector3 dir = (transform.position - PrevPosition);
        float distance = dir.magnitude;
        Ray ray = new Ray(PrevPosition,dir.normalized);
        RaycastHit[] rayhit = Physics.RaycastAll(ray, distance);
        if(rayhit.Length > 0)
        {
            for(int i = 0; i<rayhit.Length;i++)
            {
                if(rayhit[i].collider.tag != _tag && rayhit[i].collider.tag != "Bullet")
                {
                    if(rayhit[i].collider.tag == "IceBlock" && transform.tag != "Dragon")
                    {
                        rayhit[i].collider.gameObject.GetComponent<IceBlock>().GetDamage(10);
                    }
                    if(rayhit[i].collider.tag == "DragonTarget")
                    {
                        rayhit[i].collider.SendMessage("Hit");
                    }

                    if(CollisionSound != null)
                    {
                        Instantiate(CollisionSound, transform.position, Quaternion.identity);
                    }

                    BulletDestroy();
                }
            }
        }
    }

    //업데이트
    private void FixedUpdate()
    {
        if(_delayTime > 0.0f)
        {
            _delayTime -= Time.fixedDeltaTime;
        }
        else
        {
            //총알 이동
            UpdateBulletPos();
            //총알 충돌 체크
            BulletCollisionCheck();
        }
    }
}