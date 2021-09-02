using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : AbstructShip
{
    public override ConflictSides ConflictSide => ConflictSides.Enemy;
}
