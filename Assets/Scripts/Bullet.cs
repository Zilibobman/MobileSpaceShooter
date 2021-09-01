using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    public int damage;

    public int Damage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int Speed { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public ConflictSides conflictSide;
    public ConflictSides ConflictSide => conflictSide;

    public void Hurt(IDamagable victim)
    {
        victim.GetDamage(damage);
    }

    private void Destruction()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if((coll.tag.Contains("Player") || coll.tag.Contains("Enemy")) && coll.gameObject.TryGetComponent(out IHaveConflictSideAndDamageble collConflictSide))
        {
            if(collConflictSide.ConflictSide != conflictSide)
            {
                Hurt(collConflictSide);
                Destruction();
            }
        }

    }
}
