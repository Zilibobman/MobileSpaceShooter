using System;
using System.Collections.Generic;
using UnityEngine;

class ShipMayBeShotInTimeGunSettingsChanger : MonoBehaviour, IShipSettingsChanger
{
    [Range(0, 100)]
    public int shot_Chance;
    public float shot_Time_Min, shot_Time_Max;

    public void ChangeSettinge(AbstructShip Ship)
    {
        IMayBeShotInTimeGun[] guns = Ship.gameObject.transform.GetComponentsInChildren<IMayBeShotInTimeGun>();
        foreach (var gun in guns)
        {
            gun.Shot_Chance = shot_Chance;
            gun.Shot_Time_Start = shot_Time_Min;
            gun.Shot_Time_End = shot_Time_Max;
        }
    }
}
