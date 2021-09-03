using System;
using System.Collections.Generic;
using UnityEngine;

class ShipSpeedChanger : MonoBehaviour, IShipSettingsChanger
{
    public float Speed;

    public void ChangeSettinge(AbstructShip Ship)
    {
        Ship.driver.Speed = Speed;
    }
}
