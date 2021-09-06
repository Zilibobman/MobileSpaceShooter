using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveByTransform : WaveByTrjectory<Transform[], Vector2[]>
{
    protected override void ModyifyEnemysTrajectory()
    {
        enemyPilot.Trajectory = NewPositionByPath(Trajectory);
    }
    private void OnDrawGizmos()
    {
        Vector2[] SmoothTrajectory = NewPositionByPath(Trajectory);
        for (int i = 0; i < SmoothTrajectory.Length - 1; i++)
        {
            Gizmos.DrawLine(SmoothTrajectory[i], SmoothTrajectory[i + 1]);
        }
    }
    protected override GameObject CreateEnemy()
    {
        GameObject newEnemy = base.CreateEnemy();
        newEnemy.transform.position = Trajectory[0].position;
        return newEnemy;
    }
    Vector2[] NewPositionByPath(Transform[] path)
    {
        Vector2[] SmoothTrajectory = new Vector2[path.Length];
        for (int i = 0; i < path.Length; i++)
        {
            SmoothTrajectory[i] = path[i].position;
        }
        SmoothTrajectory = Smoothing(SmoothTrajectory);
        SmoothTrajectory = Smoothing(SmoothTrajectory);
        SmoothTrajectory = Smoothing(SmoothTrajectory);
        return SmoothTrajectory;
    }

    Vector2[] Smoothing(Vector2[] path_Positions)
    {
        if (path_Positions.Length < 2)
            return path_Positions;
        Vector2[] new_Path_Positions = new Vector2[(path_Positions.Length - 2) * 2 + 2];
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

