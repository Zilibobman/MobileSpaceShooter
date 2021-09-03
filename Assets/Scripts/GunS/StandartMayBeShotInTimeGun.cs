using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartMayBeShotInTimeGun : AbstractGun, IMayBeShotInTimeGun
{
    public int shot_Chance;
    public int Shot_Chance { get => shot_Chance; set => shot_Chance = value; }
    public float shot_Time_Min, shot_Time_Max;
    public float Shot_Time_Start { get => shot_Time_Min; set => shot_Time_Min = value; }
    public float Shot_Time_End { get => shot_Time_Max; set => shot_Time_Max = value; }


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
