using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StandartGun : MonoBehaviour, IGun
{
    private float timer_Shot;
    public GameObject obj_Bullet;
    private GameObject obj_ModifiBullet;
    public TypesOfGun typeOfGun;
    public List<IBulletModifyer> Modifiers { get; set; }
    public IMakeAShot Shoter { get; set; }
    public TypesOfGun TypeOfGun => typeOfGun;

    public float time_Bullet_Spawn = 0.3f;
    public float Time_Bullet_Spawn { get => time_Bullet_Spawn; set => time_Bullet_Spawn = value; }
    public ParticleSystem particle;
    public ParticleSystem Particle { get => particle; set => particle = value; }

    // Start is called before the first frame update
    void Start()
    {
        Shoter = gameObject.GetComponent<IMakeAShot>();
        SetStandartModifiers();
        ModifyBullets();
        particle = gameObject.GetComponent<ParticleSystem>();
    }
    public void ModifyBullets()
    {
        if(obj_ModifiBullet != null)
        {
            Destroy(obj_ModifiBullet);
        }
        obj_ModifiBullet = Instantiate(obj_Bullet);
        obj_ModifiBullet.SetActive(false);

        IBullet bullet = obj_ModifiBullet.GetComponent<IBullet>();
        foreach (var modificator in Modifiers)
        {
            modificator.Modify(bullet);
        }
    }
    public void SetStandartModifiers()
    {
        Modifiers = gameObject.GetComponents<IBulletModifyer>().ToList();
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
        Shoter.MakeAShot(obj_ModifiBullet);
        particle.Play();
        //Instantiate(obj_Bullet, gameObject.transform.position, Quaternion.Euler(Vector3.zero));
    }
}
