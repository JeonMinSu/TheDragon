using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour {

    public GameObject FirePos;
    public GameObject FireEffect;
    public BulletManager bulletManager;

    //최대 총알 갯수
    public int maxBulletCount;
    //현재 총알 갯수
    int currentBulletCount;
    //장전 개수
    public int maxChargeCount;
    //현재 장탄 개수
    int currentChargeCount;


	void Start ()
    {
        currentBulletCount = maxBulletCount;
        currentBulletCount -= maxChargeCount;
        currentChargeCount = maxChargeCount;
	}
    //발사
    public void Fire()
    {
        if(currentChargeCount > 0)
        {
            FireEffect.SetActive(false);
            FireEffect.SetActive(true);
            bulletManager.PlayerBaseBulletFire(FirePos.transform);
            currentChargeCount -= 1;
        }
        else
        {
            Reload();
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