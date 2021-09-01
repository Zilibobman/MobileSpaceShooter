using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StandartAutoGun : AbstractGun, IAutoGun
{
    protected float timer_Shot;
    public float time_Bullet_Spawn = 0.3f;
    public float Time_Bullet_Spawn { get => time_Bullet_Spawn; set => time_Bullet_Spawn = value; }

    protected override void MakeAShot()
    {
        if (Time.time > timer_Shot)
        {
            timer_Shot = Time.time + time_Bullet_Spawn;
            Shoter.MakeAShot(obj_ModifiBullet);
            particle?.Play();
        }
    }
}
