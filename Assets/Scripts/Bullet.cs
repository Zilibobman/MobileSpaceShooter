using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : AbstructBullet
{

    public ConflictSides conflictSide;
    public override ConflictSides ConflictSide => conflictSide;
}
