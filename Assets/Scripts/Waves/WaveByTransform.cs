using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveByTransform : WaveByTrjectory<Transform[], Vector3[]>
{
    protected override void ModyifyEnemysTrajectory()
    {
        enemyPilot.Trajectory = NewPositionByPath(Trajectory);
    }
    private void OnDrawGizmos()
    {
        Vector3[] SmoothTrajectory = NewPositionByPath(Trajectory);
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
        if (path_Positions.Length < 2)
            return path_Positions;
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

