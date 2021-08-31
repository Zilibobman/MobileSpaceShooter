using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour, IMainPlayer
{
    public static Player instanse = null;

    public GameObject obj_Shield;
    public IMainPlayerShield shield;
    public GameObject obj_Ship;
    private IPlayerShip<Vector2, Vector2> ship;
    public IPlayerShip<Vector2, Vector2> Ship => ship;

    private void Awake()
    {
        if (instanse == null)
            instanse = this;
        else
            Destroy(gameObject);

        shield = obj_Shield.GetComponent<IMainPlayerShield>();
        ship = obj_Ship.GetComponent<IPlayerShip<Vector2, Vector2>>();
        ship.ShipWasDestroy += ifShipDestroy;
    }
    private void Start()
    {

    }
    private void ifShipDestroy(object sender, System.EventArgs e) 
    {
        Destruction();
    }
    private void Destruction()
    {
        Destroy(gameObject);
    }
}
