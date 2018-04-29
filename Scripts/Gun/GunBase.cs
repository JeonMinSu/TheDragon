using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour {

    [SerializeField] private GameObject FirePos;
    [SerializeField] private GameObject FireEffect;
    [SerializeField] private BulletManager bulletManager;

    //최대 총알 갯수
    [SerializeField] private int maxBulletCount;
    //현재 총알 갯수
    int currentBulletCount;
    //장전 개수
    [SerializeField] private int maxChargeCount;
    //현재 장탄 개수
    int currentChargeCount;

    //sound
    [SerializeField] private List<AudioClip> FireSounds;
    private AudioSource audioSource;
    int playCount;


	void Start ()
    {
        currentBulletCount = maxBulletCount;
        currentBulletCount -= maxChargeCount;
        currentChargeCount = maxChargeCount;
        audioSource = GetComponent<AudioSource>();

    }
    //발사
    public bool Fire()
    {
        if(currentChargeCount > 0)
        {
            FireEffect.SetActive(false);
            FireEffect.SetActive(true);
            bulletManager.PlayerBaseBulletFire(FirePos.transform);
            playCount = Random.Range(0, FireSounds.Count);
            audioSource.clip = FireSounds[playCount];
            audioSource.Play();
            //FireSounds[playCount].Play();
            currentChargeCount -= 1;
            return true;
        }
        else
        {
            Reload();
            return false;
        }
    }

    //재장전
    public void Reload()
    {
        int chargeCount = maxChargeCount - currentChargeCount;
        if(currentBulletCount >= chargeCount)
        {
            currentBulletCount -= chargeCount;
            currentChargeCount += chargeCount;
        }
    }
}