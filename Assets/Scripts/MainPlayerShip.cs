using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPlayerShip : AbstructShip, IHeallable, IHaveDiferentRagesOfFire, ICanChangeCentralGun, ICanChangeSideGun
{
    private Slider slider;

    public GameObject centralGunSlot;
    public GameObject CentralGunSlot { get => centralGunSlot; }
    public GameObject[] sideGunsSlots;
    public GameObject[] SideGunsSlots { get => sideGunsSlots; }

    public TypesOfGun rageOfFire = TypesOfGun.Central;
    public TypesOfGun RageOfFire => rageOfFire;

    public override ConflictSides ConflictSide => ConflictSides.Player;

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

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        slider = GameObject.FindGameObjectWithTag("sl_HP").GetComponent<Slider>();
        slider.maxValue = Constants.GetMaxSpecificationValue(Specifications.HP);
        changeSliderValue();
        SetRageOfFire(rageOfFire);
    }
    private void changeSliderValue()
    {
        slider.value = currentHP;
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
                centralGunSlot.transform.GetChild(0).gameObject.SetActive(true);
                foreach (var gunSlot in sideGunsSlots)
                    gunSlot.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case TypesOfGun.Central:
                centralGunSlot.transform.GetChild(0).gameObject.SetActive(true);
                foreach (var gunSlot in sideGunsSlots)
                    gunSlot.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case TypesOfGun.Side:
                centralGunSlot.transform.GetChild(0).gameObject.SetActive(false);
                foreach (var gunSlot in sideGunsSlots)
                    gunSlot.transform.GetChild(0).gameObject.SetActive(true);
                break;
        }
    }

    public void ChangeCentralGun(GameObject newGun)
    {
        DestroyImmediate(centralGunSlot.transform.GetChild(0).gameObject);
        Instantiate(newGun, centralGunSlot.transform);
        SetRageOfFire(rageOfFire);
    }

    public void ChangeSideGun(GameObject newGun)
    {
        foreach(var sideGunSlot in sideGunsSlots)
        {
            DestroyImmediate(sideGunSlot.transform.GetChild(0).gameObject);
            Instantiate(newGun, sideGunSlot.transform);
        }
        SetRageOfFire(rageOfFire);
    }
}
