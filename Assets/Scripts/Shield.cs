using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IShield
{
    public int currentHP = 0;
    public int CurrentHP => currentHP;

    public int maxHP = 0;
    public int MaxHP => maxHP;
    public ConflictSides conflictSide;
    public ConflictSides ConflictSide => conflictSide;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(int damage)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            currentHP = 0;
            gameObject.SetActive(false);
        }
    }

    public void Heal(int HP)
    {
        currentHP += HP;
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        if(!gameObject.activeSelf && currentHP > 0)
        {
            gameObject.SetActive(true);
        }
    }
}
