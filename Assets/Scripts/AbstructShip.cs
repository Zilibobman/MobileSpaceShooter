using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstructShip<DriverType, DriverInput, ShieldType> : MonoBehaviour, IShip<IDriver<DriverInput>, IShield> 
    where DriverType : IDriver<DriverInput> 
    where ShieldType : IShield
{
    public GameObject obj_Driver;
    [HideInInspector]
    public DriverType driver;
    public IDriver<DriverInput> Driver => driver;
    public GameObject obj_Shield;
    [HideInInspector]
    public ShieldType shield;
    public IShield Shield { get => shield; }
    public int currentHP = 0;
    public int CurrentHP { get => currentHP; }

    public int maxHP = 0;
    public int MaxHP { get => maxHP; }

    public ConflictSides conflictSide;
    public ConflictSides ConflictSide => conflictSide;

    public event EventHandler ShipWasDestroy;

    public virtual void GetDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Destruction();
        }
    }
    public virtual void Awake()
    {
        driver = obj_Driver.GetComponent<DriverType>();
        obj_Shield?.TryGetComponent(out shield);
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void Destruction()
    {
        ShipWasDestroy.Invoke(this, new EventArgs());
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.tag.Contains("Player") || coll.tag.Contains("Enemy")) && coll.gameObject.TryGetComponent(out IHaveConflictSideAndDamageble collConflictSide))
        {
            if (collConflictSide.ConflictSide != conflictSide)
            {
                collConflictSide.GetDamage(1);
                GetDamage(1);
            }
        }
    }
}
