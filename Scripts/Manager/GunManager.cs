using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour {
    enum GunFireHand
    {
        RIGHT_POS,
        LEFT_POS
    };

    public GunBase GunLeft;
    public GunBase GunRight;
    public float shotDelay = 0.3f;
    float delayTime = 0.0f;
    GunFireHand FireHand;

	// Use this for initialization
	void Start ()
    {
        FireHand = GunFireHand.RIGHT_POS;
	}

    private void Update()
    {
        if (delayTime > 0)
            delayTime -= Time.deltaTime;
    }

    public void Fire()
    {
        if(delayTime <= 0)
        {
            delayTime = shotDelay;
            if (FireHand == GunFireHand.RIGHT_POS)
            {
                GunRight.Fire();
                FireHand = GunFireHand.LEFT_POS;
            }
            else
            {
                GunLeft.Fire();
                FireHand = GunFireHand.RIGHT_POS;
            }
        }
    }
}
