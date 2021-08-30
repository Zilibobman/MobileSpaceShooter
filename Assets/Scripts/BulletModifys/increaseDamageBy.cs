using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class increaseDamageBy : IBulletModifyer
{
    public int BonusDamage = 0;

    public void Modify(IBullet bullet)
    {
        bullet.Damage += BonusDamage;
    }
}
