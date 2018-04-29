using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TargetManager : MonoBehaviour {


    [SerializeField] private List<Collider> targets;     //Dragon Target Colliders
    [SerializeField] private float targetDelayTime = 3.0f;
    [SerializeField] private GameObject targetActiveEffect;
    [SerializeField] private GameObject targetDestroyEffect;
    float targetActiveTime = 0.0f;
    private Camera targetCamera;                       //Dragon TargetUI Camera
    int activeTargetCount;
    int oldTargetCount = -1;

    bool isHit = false;
    bool corHit = false;
	// Use this for initialization
	void Start ()
    {
        targetCamera = GetComponent<Camera>();

	}
	
	// Update is called once per frame
	void Update ()
    {
		if(targetActiveTime <= 0.0f)
        {        
            activeTargetCount = Random.Range(0, targets.Count);
            targetActiveTime = targetDelayTime;    
            targetActiveEffect.SetActive(false);
            targetActiveEffect.SetActive(true);
        }

        Vector3 pos = Camera.main.WorldToScreenPoint(targets[activeTargetCount].transform.position);
        if (isHit && !corHit)
        {
            StartCoroutine("CorHitEffectOn", activeTargetCount);
        }
        else
        {
            targetActiveTime -= Time.deltaTime;
            targetActiveEffect.transform.position = targetCamera.ScreenToWorldPoint(pos);
        }
    }

    void Hit()
    {
        if(!corHit)
        isHit = true;
        //Debug.Log("HitOK");
    }

    IEnumerator CorHitEffectOn(int count)
    {
        corHit = true;
        Vector3 pos = new Vector3(0, 0, 0);
        GameObject DestroyEffect = Instantiate(targetDestroyEffect, pos, Quaternion.identity);

        targetActiveEffect.SetActive(false);
        float time = 0.0f;
        while(time < 3.0f)
        {
            pos = Camera.main.WorldToScreenPoint(targets[count].transform.position);
            pos = targetCamera.ScreenToWorldPoint(pos);
            DestroyEffect.transform.position = pos;
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }



        //yield return new WaitForSeconds(3.0f);

        GameObject.Destroy(DestroyEffect);
        isHit = false;
        targetActiveTime = 0;

        corHit = false;
    }
}
