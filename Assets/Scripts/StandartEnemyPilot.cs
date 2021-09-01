using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartEnemyPilot : AbstructPilot<Vector2>, IPilotByTrajectory<Vector2, Transform[]>
{
    private Vector3[] SmoothTrajectory;
    private int cur_Pos;
    public Transform[] trajectory;
    public Transform[] Trajectory { get => trajectory; set => trajectory = value; }
    public bool is_return = true;
    protected override void Start()
    {
        base.Start();
        SmoothTrajectory = NewPositionByPath(trajectory);
    }
    protected override void GoTo()
    {
        Driver.GoTo(new Vector2(SmoothTrajectory[cur_Pos].x, SmoothTrajectory[cur_Pos].y));
        if (Vector3.Distance(transform.position, SmoothTrajectory[cur_Pos]) < 0.2f)
        {
            cur_Pos++;
            if (cur_Pos >= SmoothTrajectory.Length)
            {
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
    private void OnDrawGizmos()
    {
        Vector3[] SmoothTrajectory = NewPositionByPath(trajectory);
        for (int i = 0; i < SmoothTrajectory.Length - 1; i++)
        {
            Gizmos.DrawLine(SmoothTrajectory[i], SmoothTrajectory[i + 1]);
        }
    }

    Vector3[] NewPositionByPath(Transform[] path)
    {
        Vector3[] SmoothTrajectory = new Vector3[path.Length];
        for (int i = 0; i < path.Length; i++)
        {
            SmoothTrajectory[i] = path[i].position;
        }
        SmoothTrajectory = Smoothing(SmoothTrajectory);
        SmoothTrajectory = Smoothing(SmoothTrajectory);
        SmoothTrajectory = Smoothing(SmoothTrajectory);
        return SmoothTrajectory;
    }

    Vector3[] Smoothing(Vector3[] path_Positions)
    {
        Vector3[] new_Path_Positions = new Vector3[(path_Positions.Length - 2) * 2 + 2];
        new_Path_Positions[0] = path_Positions[0];
        new_Path_Positions[new_Path_Positions.Length - 1] = path_Positions[path_Positions.Length - 1];

        int j = 1;
        for (int i = 0; i < path_Positions.Length - 2; i++)
        {
            new_Path_Positions[j] = path_Positions[i] + (path_Positions[i + 1] - path_Positions[i]) * 0.75f;
            new_Path_Positions[j + 1] = path_Positions[i + 1] + (path_Positions[i + 2] - path_Positions[i + 1]) * 0.25f;
            j += 2;
        }
        return new_Path_Positions;
    }
}

