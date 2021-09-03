using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstructPilot<DriverInput> : AbstructPilot, IPilot<DriverInput>
{
    protected IDriver<DriverInput> driver;
    public override IDriver Driver { get => driver;}

    protected virtual void Start()
    {
        driver = gameObject.GetComponent<IShip>().Driver as IDriver<DriverInput>;
    }
    public virtual void ChangeDriver(IDriver<DriverInput> newDriver)
    {
        driver = newDriver;
    }
}
public abstract class AbstructPilot : MonoBehaviour, IPilot
{
    public abstract IDriver Driver { get; }
    protected virtual void Update()
    {
        GoTo();
    }
    protected abstract void GoTo();
}
