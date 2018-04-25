using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
    public GameObject BreathOnEffect;
    public GameObject BreathEffect;
    public float BreatSizeUpValue = 0.25f;
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
        for(float i = 0; i<1; i += 0.025f)
        {
            Debug.Log(i);
            BreathEffect.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(4.0f);

        //GameObject.Destroy(BreathOn);
        //GameObject.Destroy(Breath);
        GameObject.Destroy(this.gameObject);
        yield return null;
    }
}