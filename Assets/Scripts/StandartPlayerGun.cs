using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartPlayerGun : MonoBehaviour, IGun
{
    private float timer_Shot;
    public GameObject obj_Bullet;
    public TypesOfGun typeOfGun;
    public IBulletModifyer Modifyer;
    public IMakeAShot Shoter;
    public TypesOfGun TypeOfGun => typeOfGun;

    public float time_Bullet_Spawn = 0.3f;
    public float Time_Bullet_Spawn { get => time_Bullet_Spawn; set => time_Bullet_Spawn = value; }

    // Start is called before the first frame update
    void Start()
    {
        ModifyBullets();
    }
    protected virtual void ModifyBullets()
    {
        IBullet bullet = obj_Bullet.GetComponent<IBullet>();
        //Modifyer.Modify(bullet);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer_Shot)
        {
            timer_Shot = Time.time + time_Bullet_Spawn;
            MakeAShot();
        }
    }
    protected virtual void MakeAShot()
    {
        //Shoter.MakeAShot(obj_Bullet);
        //Instantiate(obj_Bullet, gameObject.transform.position, Quaternion.Euler(Vector3.zero));
    }
}
