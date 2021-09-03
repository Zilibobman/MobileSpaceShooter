using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DriverGoToVector2 : AbstructDriver<Vector2>
{
    public override void GoTo(Vector2 destination)
    {
        ship.transform.position = Vector2.MoveTowards(ship.transform.position, destination, speed * Time.deltaTime);
    }
}
