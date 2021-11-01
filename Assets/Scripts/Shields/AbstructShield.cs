using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstructShield : MonoBehaviour, IShield, IControllebleHP, IControllebleMaxHP, IHaveConflictSideAndDamageble
{
    protected int currentHP;
    public int CurrentHP { get => currentHP; set => currentHP = value; }
    protected int maxHP;
    public int MaxHP { get => maxHP; set => maxHP = value; }

    public abstract ConflictSides ConflictSide { get; }

    public virtual void GetDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            gameObject.SetActive(false);
        }
        Activate();
    }

    public virtual void Heal(int HP)
    {
        currentHP += HP;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        Activate();
    }
    protected virtual void Activate()
    {
        if (currentHP > 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Activate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
