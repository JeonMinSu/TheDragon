using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManagerTest : MonoBehaviour {

    public BulletManager manager;

    public float bulletFireTime = 5.0f;
    float fireDelay = 0.0f;
    bool isFire = false;

	
	// Update is called once per frame
	void Update ()
    {
	    if(fireDelay < 0)
        {
            manager.HomingBulletFire(this.transform);
            fireDelay = bulletFireTime;
        }
        fireDelay -= Time.fixedDeltaTime;

        //if(Input.GetKeyDown(KeyCode.CapsLock))
        //{
        //    if (isFire)
        //    {
        //        manager.BreathOff();
        //        isFire = false;
        //    }
        //    else
        //    {
        //        manager.BreathOn(this.transform);
        //        isFire = true;
        //    }
        //}
	}
}
