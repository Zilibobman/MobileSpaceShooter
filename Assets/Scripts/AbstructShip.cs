using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstructShip : MonoBehaviour, IShip, IControllebleHP, IControllebleMaxHP
{
    public GameObject obj_Driver;
    [HideInInspector]
    public AbstructDriver driver;
    public IDriver Driver => driver;
    public GameObject obj_Shield;
    [HideInInspector]
    public AbstructShield shield;
    public IShield Shield { get => shield; }
    protected int currentHP = 0;
    public int CurrentHP { get => currentHP; set => currentHP = value; }

    protected int maxHP = 0;
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public abstract ConflictSides ConflictSide { get; }

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
        driver = obj_Driver.GetComponent<AbstructDriver>();
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
            if (collConflictSide.ConflictSide != ConflictSide)
            {
                collConflictSide.GetDamage(Constants.CollisionDamage);
                GetDamage(Constants.CollisionDamage);
            }
        }
    }
}
