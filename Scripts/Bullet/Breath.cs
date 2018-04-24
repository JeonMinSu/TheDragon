using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
    public GameObject BreathOnEffect;
    public GameObject BreathEffect;

    public void OnBreath()
    {
        StartCoroutine("corBreath");
    }

    IEnumerator corBreath()
    {
        //GameObject BreathOn =   Instantiate(BreathOnEffect, this.transform.position, Quaternion.identity);

        BreathOnEffect.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        //GameObject Breath = Instantiate(BreathEffect, this.transform.position, Quaternion.identity);
        BreathEffect.SetActive(true);
        yield return new WaitForSeconds(4.0f);

        //GameObject.Destroy(BreathOn);
        //GameObject.Destroy(Breath);
        GameObject.Destroy(this.gameObject);
        yield return null;
    }
}