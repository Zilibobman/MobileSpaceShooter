using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class increaseDamageBy : MonoBehaviour, IBulletModifyer
{
    public int BonusDamage = 0;

    public void Modify(AbstructBullet bullet)
    {
        bullet.Damage += BonusDamage;
    }
}
