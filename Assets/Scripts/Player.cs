using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : AbstructParticipant<MainPlayerShip, DriverGoToVector2, Vector2, MainPlayerShield>
{
    public static Player instanse = null;

    private void Awake()
    {
        if (instanse == null)
            instanse = this;
        else
            Destroy(gameObject);

        ship = obj_Ship.GetComponent<MainPlayerShip>();
        ship.ShipWasDestroy += ifShipDestroy;
    }
    private void Start()
    {

    }
}
