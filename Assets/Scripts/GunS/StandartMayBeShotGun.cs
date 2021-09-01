using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class StandartMayBeShotGun : AbstractGun, IMayBeShotGun, IAutoGun
{
    protected float timer_Shot;
    public int shot_Chance;
    public int Shot_Chance { get => shot_Chance; set => shot_Chance = value; }

    public float time_Bullet_Spawn = 0.3f;
    public float Time_Bullet_Spawn { get => time_Bullet_Spawn; set => time_Bullet_Spawn = value; }

    protected override void MakeAShot()
    {
        if (Time.time > timer_Shot)
        {
            timer_Shot = Time.time + time_Bullet_Spawn;
            if (Random.value < (float)Shot_Chance / 100)
            {
                Shoter.MakeAShot(obj_ModifiBullet);
                particle?.Play();
            }
        }
    }
}
