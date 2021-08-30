using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour,IPlayerShip<Vector2,Vector2>
{
    public int currentHP = 0;
    public int CurrentHP => currentHP;
    public int maxHP = 0;
    public int MaxHP => maxHP;

    public IDriver<Vector2> driver;

    public event EventHandler ShipWasDestroy;

    public IDriver<Vector2> Driver => driver;

    public GameObject CentralGun;
    public List<GameObject> SideGuns;
    public TypesOfGun rageOfFire = TypesOfGun.Cental;
    public TypesOfGun RageOfFire => rageOfFire;

    public TypesOfGun RateOfFire { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void GetDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Destruction();
        }
    }

    public void Heal(int HP)
    {
        currentHP += HP;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        if (!gameObject.activeSelf && currentHP > 0)
        {
            gameObject.SetActive(true);
        }
    }

    public void GoTo(Vector2 Trjectory)
    {
        Driver.GoTo(Trjectory);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GoTo(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
    private void Destruction()
    {
        ShipWasDestroy.Invoke(this, new EventArgs());
        Destroy(gameObject);
    }

    public void ChangeRageOfFire(TypesOfGun RageOfFire)
    {
        rageOfFire = RageOfFire;
        SetRageOfFire(rageOfFire);
    }

    public void UpRateOfFire()
    {
        switch (rageOfFire)
        {
            case TypesOfGun.Cental:
                rageOfFire = TypesOfGun.Side;
                break;
            case TypesOfGun.Side:
                rageOfFire = TypesOfGun.All;
                break;
        }
        SetRageOfFire(rageOfFire);
    }
    private void SetRageOfFire(TypesOfGun typesOfGun)
    {
        switch(typesOfGun)
        {
            case TypesOfGun.All:
                CentralGun.SetActive(true);
                foreach (var gun in SideGuns)
                    gun.SetActive(true);
                break;
            case TypesOfGun.Cental:
                CentralGun.SetActive(true);
                foreach (var gun in SideGuns)
                    gun.SetActive(false);
                break;
            case TypesOfGun.Side:
                CentralGun.SetActive(false);
                foreach (var gun in SideGuns)
                    gun.SetActive(true);
                break;
        }
    }
}
