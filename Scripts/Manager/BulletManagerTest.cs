using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManagerTest : MonoBehaviour {

    enum BULLETTYPE
    {
        BASE,
        HOMING,
        BREATH
    };

    public BulletManager manager;
    public float bulletFireTime = 5.0f;
    [SerializeField]
    private BULLETTYPE bulletType;
    float fireDelay = 0.0f;
    bool isFire = false;
   

    
	// Update is called once per frame
	void Update ()
    {
	    if(fireDelay < 0)
        {
            switch (bulletType)
            {
                case BULLETTYPE.BASE:
                    manager.DragonBaseBulletFire(this.transform);
                    break;
                case BULLETTYPE.HOMING:
                        manager.DragonHomingBulletFire(this.transform.position,this.transform.forward);
                    break;
                case BULLETTYPE.BREATH:
                    manager.DragonBreathOn(this.transform.position + this.transform.forward,this.transform.forward );
                    break;
                
            }
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
