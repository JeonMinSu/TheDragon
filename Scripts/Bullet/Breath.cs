using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour {

    private Transform _firePos;
    public GameObject _breathParticle;
    private bool isFire = false;
    private ParticleSystem[] _breatParticles;
    public void OnBreath(Transform pos)
    {
        _firePos = pos;
        _breatParticles = _breathParticle.GetComponentsInChildren<ParticleSystem>();
        for(int i = 0; i<_breatParticles.Length; i++)
        {
            _breatParticles[i].Play();
        }
        _breathParticle.GetComponent<Animator>().SetBool("IsFire", true);
        //_breathParticle.SetActive(true);       
        isFire = true;
    }

    private void Update()
    {
        if(isFire)
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
        _breathParticle.GetComponent<Animator>().SetBool("IsFire", false);
        // _breathParticle.SetActive(false);
        isFire = false;
    }


          

}
