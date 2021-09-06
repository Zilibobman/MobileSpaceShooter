using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstructPilot : MonoBehaviour, IPilot, ICanUpdateDriver
{
    public abstract IDriver Driver { get; }
    protected virtual void Update()
    {
        GoTo();
    }
    protected virtual void Start()
    {
        UpdateDriver();
    }
    protected abstract void GoTo();

    public abstract void UpdateDriver();
}

public abstract class AbstructPilotByTrajectory : AbstructPilot, IPilotByTrajectory
{
    public abstract bool Is_return { get; set; }

    public abstract event IPilotByTrajectory.EventEndingTrajectory IEndTrajectory;
}

public abstract class AbstructPilotByTrajectory<TrajectoryType> : AbstructPilotByTrajectory, IPilotByTrajectory<TrajectoryType>
{
    public abstract TrajectoryType Trajectory { get; set; }
}
