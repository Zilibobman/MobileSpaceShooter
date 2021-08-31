using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour, IPlayerShip<Vector2,Vector2>
{
    private Slider slider;
    public int currentHP = 0;
    public int CurrentHP { get => currentHP; set => currentHP = value; }
    public int maxHP = 0;
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public GameObject obj_Driver;
    public IDriver<Vector2> driver;

    public event EventHandler ShipWasDestroy;

    public IDriver<Vector2> Driver => driver;

    public GameObject CentralGun;
    public List<GameObject> SideGuns;
    public TypesOfGun rageOfFire = TypesOfGun.Central;
    public TypesOfGun RageOfFire => rageOfFire;

    public ConflictSides conflictSide;
    public ConflictSides ConflictSide => conflictSide;

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
    public void Awake()
    {
        driver = obj_Driver.GetComponent<IDriver<Vector2>>();
    }

    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.FindGameObjectWithTag("sl_HP").GetComponent<Slider>();
        slider.maxValue = Constants.GetMaxSpecificationValue(Specifications.HP);
        changeSliderValue();
        SetRageOfFire(rageOfFire);
    }
    private void changeSliderValue()
    {
        slider.value = currentHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 dislocate = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dislocate.y += 1.5f;
            GoTo(dislocate);
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

    public void UpRageOfFire()
    {
        switch (rageOfFire)
        {
            case TypesOfGun.Central:
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
            case TypesOfGun.Central:
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
