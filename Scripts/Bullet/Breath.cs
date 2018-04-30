using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
    public GameObject BreathOnEffect;
    public GameObject BreathEffect;

    public AudioClip BreathOnSound;
    public AudioClip BreathFireSound;

    private AudioSource audioSource;

    //public float BreatSizeUpValue = 0.25f;
    public void OnBreath()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("corBreath");

    }

    IEnumerator corBreath()
    {
        //GameObject BreathOn =   Instantiate(BreathOnEffect, this.transform.position, Quaternion.identity);

        BreathOnEffect.SetActive(true);
        audioSource.clip = BreathOnSound;
        audioSource.Play();
        yield return new WaitForSeconds(1.0f);

        //GameObject Breath = Instantiate(BreathEffect, this.transform.position, Quaternion.identity);

        audioSource.clip = BreathFireSound;
        audioSource.Play();
        BreathEffect.SetActive(true);
        //for(float i = 0; i<1; i += 0.025f)
        //{
        //    Debug.Log(i);
        //    BreathEffect.transform.localScale = new Vector3(i, i, i);
        //    yield return new WaitForEndOfFrame();
        //}

        yield return new WaitForSeconds(5.0f);

        //GameObject.Destroy(BreathOn);
        //GameObject.Destroy(Breath);
        GameObject.Destroy(this.gameObject);
        yield return null;
    }
}