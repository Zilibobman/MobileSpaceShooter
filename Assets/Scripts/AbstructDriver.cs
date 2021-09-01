using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstructDriver<DriverInput> : MonoBehaviour, IDriver<DriverInput>
{
    public Transform ship;
    public Transform Ship => ship;

    public int speed = 0;
    public int Speed => speed;
    public int maxSpeed = int.MaxValue;
    public int MaxSpeed => maxSpeed;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    public abstract void GoTo(DriverInput destination);
}
