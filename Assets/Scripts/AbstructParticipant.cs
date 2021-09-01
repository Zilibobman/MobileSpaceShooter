using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstructParticipant<ShipType, DriverType, DriverInput, ShieldType> : MonoBehaviour 
    where DriverType : IDriver<DriverInput>
    where ShieldType : IShield
    where ShipType : AbstructShip<DriverType, DriverInput, ShieldType>
{
    public GameObject obj_Ship;
    protected ShipType ship;
    public IShip<IDriver<DriverInput>, IShield> Ship => ship;
    // Start is called before the first frame update
    void Start()
    {
        ship = obj_Ship.GetComponent<ShipType>();
        ship.ShipWasDestroy += ifShipDestroy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected virtual void ifShipDestroy(object sender, System.EventArgs e)
    {
        Destruction();
    }
    protected virtual void Destruction()
    {
        Destroy(gameObject);
    }
}
