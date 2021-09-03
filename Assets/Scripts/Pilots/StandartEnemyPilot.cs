using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartEnemyPilot : AbstructPilot<Vector2>, IPilotByTrajectory<Vector3[]>
{
    private int cur_Pos;
    private Vector3[] trajectory;
    public Vector3[] Trajectory { get => trajectory; 
        set
        {
            cur_Pos = 0;
            trajectory = value;
        } }
    public bool is_return = true;

    public event IPilotByTrajectory.EventEndingTrajectory IEndTrajectory;

    public bool Is_return { get => is_return; set => is_return = value; }
    protected override void Start()
    {
        base.Start();
    }
    protected override void GoTo()
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
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}

