﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoming : BulletBase
{
    float _homingPower;
    Vector3 _direction;
    private Transform _target;
    private Transform Target { get { return _target; } }

    private BulletManager manager;
  
    protected override void UpdateBulletPos()
    {
        UpdateBulletValue();
        this.transform.position = BasePosition;
        _direction = ((_target.position + (Vector3.up * 1.0f)) - transform.position).normalized;
        BulletForward = Vector3.Lerp(BulletForward, _direction, _homingPower * Time.fixedDeltaTime);
        this.transform.rotation = Quaternion.LookRotation(-_direction, Vector3.up);
    }

    protected override void BulletDestroy()
    {
        Player.GetComponent<PlayerCharacter.PlayerCharacterController>().StartCoroutine("CorCameraShake");
        if(manager != null)
        {
            RaycastHit rayhit;
            if (Physics.Raycast(this.transform.position + Vector3.up * 10.0f, Vector3.down, out rayhit, 100.0f))
            {
                manager.IceBlockSpawn(rayhit.point);
            }
        }
        base.BulletDestroy();
    }

    public virtual void SetBulletValue(Transform firePos, float moveSpeed, float homingPower, Transform target)
    {
        SetBaseValue(firePos, moveSpeed);
        _direction = firePos.forward;
        _homingPower = homingPower; 
        _target = target;
    }
    public virtual void SetBulletValue(Vector3 firePos, Vector3 fireDir, float moveSpeed, float homingPower, Transform target)
    {
        SetBaseValue(firePos, fireDir, moveSpeed);
        _direction = fireDir;
        _homingPower = homingPower;
        _target = target;

    }
    public void SetTarget(Transform _t)
    {
        _target = _t;
    }
    public void SetBulletManager(BulletManager _manager)
    {
        manager = _manager;
    }
}
