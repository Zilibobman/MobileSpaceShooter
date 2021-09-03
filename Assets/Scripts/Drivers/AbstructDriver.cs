using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstructDriver<DriverInput> : AbstructDriver, IDriver<DriverInput>
{
    public abstract void GoTo(DriverInput destination);
}
public abstract class AbstructDriver : MonoBehaviour, IDriver, IControllebleSpeed, IControllebleMaxSpeed
{
    public Transform ship;
    public Transform Ship { get => ship; }

    protected float speed = 0;
    public float Speed { get => speed; set => speed = value; }
    protected int maxSpeed = int.MaxValue;
    public int MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
}
