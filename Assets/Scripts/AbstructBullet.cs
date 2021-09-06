using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstructBullet : MonoBehaviour, IBullet, IControllebleSpeed, IHaveControllableDamage
{
    [SerializeField]
    protected int damage;
    public int Damage { get => damage; set => damage = value; }
    [SerializeField]
    protected float speed;
    public float Speed { get => speed; set => speed = value; }
    public abstract ConflictSides ConflictSide { get; }

    public virtual void Hurt(IDamagable victim)
    {
        victim.GetDamage(damage);
        Destruction();
    }
    protected virtual void Destruction()
    {
        Destroy(gameObject);
    }
    protected virtual void mooving()
    {
        transform.Translate(transform.up * speed * Time.deltaTime);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        mooving();
    }
    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.tag.Contains("Player") || coll.tag.Contains("Enemy")) && coll.gameObject.TryGetComponent(out IHaveConflictSideAndDamageble collConflictSide))
        {
            if (collConflictSide.ConflictSide != ConflictSide)
            {
                Hurt(collConflictSide);
                Destruction();
            }
        }
    }
}
