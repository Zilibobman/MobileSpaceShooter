using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WaveByTrjectory<TrajectoryTypeInInspector, TrajectoryTypeInPilot> : AbstructWave
{
    public TrajectoryTypeInInspector Trajectory;
    public AbstructPilotByTrajectory<TrajectoryTypeInPilot> enemyPilotScript;
    public IPilotByTrajectory<TrajectoryTypeInPilot> enemyPilot;
    public bool Is_return;
    protected override void GetEnemyComponents(GameObject Enemy)
    {
        base.GetEnemyComponents(Enemy);
    }
    protected override void ModyifyEnemy()
    {
        base.ModyifyEnemy();
        enemyPilot = enemyShip.gameObject.GetComponent<IPilotByTrajectory<TrajectoryTypeInPilot>>();
        if (enemyPilot == null)
        {
            enemyPilot = enemyShip.gameObject.AddComponent(enemyPilotScript.GetType()) as IPilotByTrajectory<TrajectoryTypeInPilot>;
            enemyPilot.UpdateDriver();
        }
        enemyPilot.Is_return = Is_return;
        ModyifyEnemysTrajectory();
    }
    protected abstract void ModyifyEnemysTrajectory();
    protected virtual void IfEnemyEndTrajectory(GameObject enemyPilotObj, bool is_return)
    {
        if (!is_return)
        {
            DestroyRec(enemyPilotObj.transform.parent.gameObject);
        }
    }
    protected void DestroyRec(GameObject obj)
    {
        if (obj.transform.parent != null)
        {
            DestroyRec(obj.transform.parent.gameObject);
        }
        else
        {
            Destroy(obj);
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
