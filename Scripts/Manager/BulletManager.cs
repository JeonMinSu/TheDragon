using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private GameObject BasePlayerBullet;
    [SerializeField]
    private GameObject WaveBulletPrefab;
    [SerializeField]
    private GameObject BaseDragonBullet;
    [SerializeField]
    private GameObject GaOBaEffect;
    [SerializeField]
    private GameObject RotateDragonBullet;
    [SerializeField]
    private GameObject HomingDragonBullet;
    [SerializeField]
    private GameObject BreathPrefab;
    [SerializeField]
    private GameObject IceBlockPrefab;
    [SerializeField]
    private GameObject Dragon;
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject BaseDragonReadySound;

    [SerializeField]
    private GameObject BaseDragonFireSound;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            IceBlockSpawn(this.transform.position);
        }
    }

    public void PlayerBaseBulletFire(Transform _firepos)
    {
        GameObject bullet = Instantiate(BasePlayerBullet, _firepos.position, Quaternion.identity);
        bullet.GetComponent<BulletBase>().SetBulletValue(_firepos, 150.0f);
        bullet.GetComponent<BulletBase>().SetTag("Player");
    }

    //public void WaveBulletFire(Transform _firePos)
    //{

    //    GameObject bullet = Instantiate(WaveBulletPrefab, _firePos.position, Quaternion.identity);
    //    bullet.GetComponent<BulletWave>().SetBulletValue(_firePos, 30.0f, 1, 1);

    //    bullet = Instantiate(WaveBulletPrefab, _firePos.position, Quaternion.identity);
    //    bullet.GetComponent<BulletWave>().SetBulletValue(_firePos, 30.0f, 1, 1);
    //    bullet.GetComponent<BulletWave>().Revers();

    //    bullet = Instantiate(WaveBulletPrefab, _firePos.position, Quaternion.identity);
    //    bullet.GetComponent<BulletWave>().SetBulletValue(_firePos, 30.0f, 0, 0, 1, 1);

    //    bullet = Instantiate(WaveBulletPrefab, _firePos.position, Quaternion.identity);
    //    bullet.GetComponent<BulletWave>().SetBulletValue(_firePos, 30.0f, 0, 0, 1, 1);
    //    bullet.GetComponent<BulletWave>().Revers();
    //}

    //public void RotateBulletFire(Transform _firePos)
    //{

    //    BulletRotate bullet = Instantiate(RotateDragonBullet, _firePos.position, Quaternion.identity).GetComponent<BulletRotate>();
    //    bullet.SetBulletValue(_firePos, 30, 1, 360, RotAxis.ZRot);

    //    bullet = Instantiate(RotateDragonBullet, _firePos.position, Quaternion.identity).GetComponent<BulletRotate>();
    //    bullet.SetBulletValue(_firePos, 30, 1, 360, RotAxis.ZRot);
    //    bullet.SetTime((float)1 / 3);

    //    bullet = Instantiate(RotateDragonBullet, _firePos.position, Quaternion.identity).GetComponent<BulletRotate>();
    //    bullet.SetBulletValue(_firePos, 30, 1, 360, RotAxis.ZRot);
    //    bullet.SetTime((float)2 / 3);

    //    bullet = Instantiate(RotateDragonBullet, _firePos.position, Quaternion.identity).GetComponent<BulletRotate>();
    //    bullet.SetBulletValue(_firePos, 30, 1, 360, RotAxis.XRot);

    //    bullet = Instantiate(RotateDragonBullet, _firePos.position, Quaternion.identity).GetComponent<BulletRotate>();
    //    bullet.SetBulletValue(_firePos, 30, 1, 360, RotAxis.XRot);
    //    bullet.SetTime((float)1 / 3);

    //    bullet = Instantiate(RotateDragonBullet, _firePos.position, Quaternion.identity).GetComponent<BulletRotate>();
    //    bullet.SetBulletValue(_firePos, 30, 1, 360, RotAxis.XRot);
    //    bullet.SetTime((float)2 / 3);
    //}

    /*Dragon Bullet*/

    public void DragonBaseBulletFire(Transform _firePos)
    {
        GameObject bullet = Instantiate(BaseDragonBullet, _firePos.position, Quaternion.identity);
        bullet.GetComponent<BulletBase>().SetBulletValue(_firePos, 150.0f);
        bullet.GetComponent<BulletBase>().SetTag("Dragon");
        bullet.GetComponent<BulletBase>().SetDelayTime(4.0f);
        GameObject effect = Instantiate(GaOBaEffect, _firePos.position + _firePos.forward, Quaternion.LookRotation(_firePos.forward,Vector3.up));
        Instantiate(BaseDragonReadySound, _firePos.position, Quaternion.identity);
        Instantiate(BaseDragonFireSound, _firePos.position, Quaternion.identity);
    }

    public void DragonBaseBulletFire(Vector3 _firePos, Vector3 _fireDir)
    {
        GameObject bullet = Instantiate(BaseDragonBullet, _firePos, Quaternion.identity);
        bullet.GetComponent<BulletBase>().SetBulletValue(_firePos,_fireDir, 150.0f);
        bullet.GetComponent<BulletBase>().SetTag("Dragon");
        bullet.GetComponent<BulletBase>().SetDelayTime(4.0f);
        GameObject effect = Instantiate(GaOBaEffect, _firePos + _fireDir.normalized, Quaternion.LookRotation(_fireDir,Vector3.up));
        Instantiate(BaseDragonReadySound, _firePos, Quaternion.identity);
        Instantiate(BaseDragonFireSound, _firePos, Quaternion.identity);
    }

    public void DragonHomingBulletFire(Transform _firePos)
    {
        BulletHoming bullet = Instantiate(HomingDragonBullet, _firePos.position, Quaternion.identity).GetComponent<BulletHoming>();
        bullet.SetBulletValue(_firePos, 100.0f, 1.5f,Player.transform);
        bullet.SetTag("Dragon");
        bullet.SetPlayer(Player);
        bullet.SetBulletManager(this);

        foreach (RotAxis suit in RotAxis.GetValues(typeof(RotAxis)))
        {
            BulletRotate rotBullet = Instantiate(RotateDragonBullet, _firePos.position, Quaternion.identity).GetComponent<BulletRotate>();
            rotBullet.SetBulletValue(_firePos, 0, 10, 360, suit);
            rotBullet.SetTime(0.0f);
            rotBullet.SetbaseTarget(bullet.gameObject.transform);
            rotBullet.SetTag("Dragon");

            rotBullet = Instantiate(RotateDragonBullet, _firePos.position, Quaternion.identity).GetComponent<BulletRotate>();
            rotBullet.SetBulletValue(_firePos, 0, 10, 360,suit);
            rotBullet.SetTime(0.5f);
            rotBullet.SetbaseTarget(bullet.gameObject.transform);
            rotBullet.SetTag("Dragon");
        }
    }

    public void DragonHomingBulletFire(Vector3 _firePos, Vector3 _fireDir)
    {
        BulletHoming bullet = Instantiate(HomingDragonBullet, _firePos, Quaternion.identity).GetComponent<BulletHoming>();
        bullet.SetBulletValue(_firePos, _fireDir, 100.0f, 1.5f, Player.transform);
        bullet.SetTag("Dragon");
        bullet.SetPlayer(Player);
        bullet.SetBulletManager(this);

        foreach (RotAxis suit in RotAxis.GetValues(typeof(RotAxis)))
        {
            BulletRotate rotBullet = Instantiate(RotateDragonBullet, _firePos, Quaternion.identity).GetComponent<BulletRotate>();
            rotBullet.SetBulletValue(_firePos, _fireDir, 0, 10, 360, suit);
            rotBullet.SetTime(0.0f);
            rotBullet.SetbaseTarget(bullet.gameObject.transform);
            rotBullet.SetTag("Dragon");

            rotBullet = Instantiate(RotateDragonBullet, _firePos, Quaternion.identity).GetComponent<BulletRotate>();
            rotBullet.SetBulletValue(_firePos, _fireDir, 0, 10, 360, suit);
            rotBullet.SetTime(0.5f);
            rotBullet.SetbaseTarget(bullet.gameObject.transform);
            rotBullet.SetTag("Dragon");
        }
    }

    public void DragonBreathOn(Vector3 _firePos, Vector3 _fireDir)
    {
        Breath breath = Instantiate(BreathPrefab, _firePos, Quaternion.LookRotation(_fireDir, Vector3.up)).GetComponent<Breath>();
        breath.OnBreath();
    }

    //public void DragonBreathOff()
    //{
    //    BreathPrefab.GetComponent<Breath>().OffBreath();
    //}

    public void IceBlockSpawn(Vector3 SpawnPos)
    {
        GameObject ice = Instantiate(IceBlockPrefab, SpawnPos, IceBlockPrefab.transform.rotation);
        ice.GetComponent<IceBlock>().Spawn();
        //StartCoroutine("CorIceBreath");
    }

    public void CameraShake()
    {
        Player.GetComponent<PlayerCharacter.PlayerCharacterController>().SendMessage("CorCameraShake");
    }

    IEnumerator CorIceBreath()
    {
        for(int x=-10; x<10; x++)
        {
            GameObject ice = Instantiate(IceBlockPrefab, new Vector3(x * 5,0,x * 5), IceBlockPrefab.transform.rotation);
            ice.GetComponent<IceBlock>().Spawn();
            yield return new WaitForSeconds(0.1f);              
        }
    }

    IEnumerator CorBaseBulletFire(Transform _firePos)
    {
        List<GameObject> bulletList = new List<GameObject>();

        Vector3 dir = (Player.transform.position - _firePos.position).normalized;

        for (int i = 0; i < 10; i++)
        {
            Vector3 correct = new Vector3(0, 0, 0);//; = new Vector3(Mathf.Sin(i * Mathf.Deg2Rad * 360 / 10) * 10, Mathf.Cos(i * Mathf.Deg2Rad * 360 / 10) * 10, 0.0f);
            correct += _firePos.up * Mathf.Sin(i * Mathf.Deg2Rad * 360 / 10) * 10;
            correct += _firePos.right * Mathf.Cos(i * Mathf.Deg2Rad * 360 / 10) * 10;
            dir = (Player.transform.position - _firePos.position + correct).normalized;
            GameObject bullet = Instantiate(BaseDragonBullet, _firePos.position, Quaternion.identity);
            // bullet.GetComponent<BulletBase>().SetBulletValue(_firePos, 0.0f,rot, correct);
            bullet.GetComponent<BulletBase>().SetBulletValue(_firePos.position + correct , dir, 0.0f);
            bullet.GetComponent<BulletBase>().SetTag("Dragon");
            bulletList.Add(bullet);

            yield return new WaitForSeconds(0.1f);
        }

        for(int i = 0; i<bulletList.Count; i++)
        {
            bulletList[i].GetComponent<BulletBase>().SetBulletSpeed(150.0f);
            yield return new WaitForSeconds(0.3f);
        }
        yield return null;
    }

    bool ErrorFind()
    {

        if(Player == null)
        {
            Debug.LogWarning("Not Find Player");
            return true;
        }
        if (Dragon == null)
        {
            Debug.LogWarning("Not Dragon Player");
            return true;
        }
        if (BasePlayerBullet == null)
        {
            Debug.LogWarning("No BaseBullet Prefab");
            return true;
        }
        if (WaveBulletPrefab == null)
        {
            Debug.LogWarning("No WaveBulletPrefab Prefab");
            return true;
        }

        if (RotateDragonBullet == null)
        {
            Debug.LogWarning("No RotateDragonBullet Prefab");
            return true;
        }
        if (HomingDragonBullet == null)
        {
            Debug.LogWarning("No HomingDragonBullet Prefab");
            return true;

        }
        if(IceBlockPrefab == null)
        {
            Debug.LogWarning("No IceBlockPrefab Prefab");
            return true;
        }
        return false;
    }
}
