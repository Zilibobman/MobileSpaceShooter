using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstructParticipant : MonoBehaviour, IHaveShip
{
    public GameObject obj_Ship;
    [HideInInspector]
    public AbstructShip ship;
    public IShip Ship { get => ship; }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        ship = obj_Ship.GetComponent<AbstructShip>();
        ship.ShipWasDestroy += ifShipDestroy;
    }

    // Update is called once per frame
    protected virtual void Update()
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
