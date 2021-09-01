using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstructPilot<DriverInput> : MonoBehaviour, IPilot<DriverInput>
{
    protected IDriver<DriverInput> driver;
    public IDriver<DriverInput> Driver { get => driver; set => driver = value; }

    protected virtual void Start()
    {
        driver = gameObject.GetComponent<IShip<IDriver<DriverInput>, IShield>>().Driver;
    }
    protected virtual void Update()
    {
        GoTo();
    }
    protected abstract void GoTo();
}
