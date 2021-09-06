using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractGun : MonoBehaviour, IGun
{
    public GameObject obj_Bullet;
    protected GameObject obj_ModifiBullet;
    public TypesOfGun typeOfGun;
    public List<IBulletModifyer> Modifiers { get; set; }
    public IMakeAShot Shoter { get; set; }
    public TypesOfGun TypeOfGun => typeOfGun;
    [HideInInspector]
    public ParticleSystem particle;
    public ParticleSystem Particle { get => particle; set => particle = value; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Shoter = gameObject.GetComponent<IMakeAShot>();
        SetStandartModifiers();
        ModifyBullets();
        gameObject.TryGetComponent<ParticleSystem>(out particle);
    }
    public virtual void ModifyBullets()
    {
        if (obj_ModifiBullet != null)
        {
            Destroy(obj_ModifiBullet);
        }
        obj_ModifiBullet = Instantiate(obj_Bullet);
        obj_ModifiBullet.SetActive(false);
        obj_ModifiBullet.transform.SetParent(gameObject.transform);

        AbstructBullet bullet = obj_ModifiBullet.GetComponent<AbstructBullet>();
        foreach (var modificator in Modifiers)
        {
            modificator.Modify(bullet);
        }
    }
    public virtual void SetStandartModifiers()
    {
        Modifiers = gameObject.GetComponents<IBulletModifyer>().ToList();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        MakeAShot();
    }
    protected abstract void MakeAShot();
}
