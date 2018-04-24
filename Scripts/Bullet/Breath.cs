using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
    private Transform _firePos;
    public GameObject _breathParticle;
    private bool isFire = false;
    private ParticleSystem[] _breatParticles;

    private void Start()
    {
        _breatParticles = _breathParticle.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < _breatParticles.Length; i++)
        {
            _breatParticles[i].Stop();
        }
    }
    public void OnBreath(Transform pos)
    {
        _firePos = pos;
        for (int i = 0; i < _breatParticles.Length; i++)
        {
            _breatParticles[i].Play();
        }
        isFire = true;
    }
    private void Update()
    {
        if (isFire)
        {
            _breathParticle.transform.position = _firePos.transform.position;
            _breathParticle.transform.rotation = _firePos.transform.rotation;
        }
    }
    public void OffBreath()
    {
        for (int i = 0; i < _breatParticles.Length; i++)
        {
            _breatParticles[i].Stop();
        }
        isFire = false;
    }
}