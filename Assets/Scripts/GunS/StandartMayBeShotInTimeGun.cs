using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartMayBeShotInTimeGun : AbstractGun, IMayBeShotGun
{
    public int shot_Chance;
    public int Shot_Chance { get => shot_Chance; set => shot_Chance = value; }
    public float shot_Time_Min, shot_Time_Max;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Invoke("MakeAShot", Random.Range(shot_Time_Min, shot_Time_Max));
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void MakeAShot()
    {
        if (Random.value < (float)Shot_Chance / 100)
        {
            Shoter.MakeAShot(obj_ModifiBullet);
            particle?.Play();
        }
    }
}
