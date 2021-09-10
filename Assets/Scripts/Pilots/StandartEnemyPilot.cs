using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartEnemyPilot : AbstructPilotByTrajectory<Vector2[]>
{
    private int cur_Pos;
    protected IDriver<Vector2> driver;
    public override IDriver Driver => driver;
    public Vector2[] trajectory;
    public override Vector2[] Trajectory { get => trajectory; 
        set
        {
            cur_Pos = 0;
            trajectory = value;
        } }
    public bool is_return = true;

    public override event IPilotByTrajectory.EventEndingTrajectory IEndTrajectory;

    public override bool Is_return { get => is_return; set => is_return = value; }

    protected override void Start()
    {
        base.Start();
    }
    protected override void GoTo()
    {
        if (cur_Pos < trajectory.Length)
        {
            driver.GoTo(trajectory[cur_Pos]);
            if (Vector2.Distance(transform.position, trajectory[cur_Pos]) < 0.2f)
            {
                cur_Pos++;
                if (cur_Pos >= trajectory.Length)
                {
                    IEndTrajectory.Invoke(gameObject, is_return);
                    if (is_return)
                    {
                        cur_Pos = 0;
                    }
                }
            }
        }
    }

    public override void UpdateDriver()
    {
        driver = gameObject.GetComponent<IShip>().Driver as IDriver<Vector2>;
    }
}

