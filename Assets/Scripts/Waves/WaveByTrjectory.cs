using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WaveByTrjectory<TrajectoryTypeInInspector, TrajectoryTypeInPilot> : AbstructWave
{
    public TrajectoryTypeInInspector Trajectory;
    protected IPilotByTrajectory<TrajectoryTypeInPilot> enemyPilot;
    public bool Is_return;
    protected override void GetEnemysComponents()
    {
        base.GetEnemysComponents();
        enemyPilot = enemyShip.gameObject.GetComponent<IPilotByTrajectory<TrajectoryTypeInPilot>>();
    }
    protected override void ModyifyEnemys()
    {
        base.ModyifyEnemys();
        enemyPilot.Is_return = Is_return;
        ModyifyEnemysTrajectory();
    }
    protected abstract void ModyifyEnemysTrajectory();
    protected virtual void IfEnemyEndTrajectory(GameObject enemyPilotObj, bool is_return)
    {
        if (!is_return)
        {
            Destroy(enemyPilotObj.transform.parent);
        }
    }
    protected override GameObject CreateEnemy()
    {
        GameObject newEnemy = base.CreateEnemy();
        newEnemy.transform.Find("Ship").GetComponent<IPilotByTrajectory>().IEndTrajectory += IfEnemyEndTrajectory;
        return newEnemy;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
