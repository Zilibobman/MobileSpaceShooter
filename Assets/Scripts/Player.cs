using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : AbstructParticipant
{
    public static Player instanse = null;

    protected override void Awake()
    {
        if (instanse == null)
            instanse = this;
        else
            Destroy(gameObject);
        base.Awake();
    }
}
