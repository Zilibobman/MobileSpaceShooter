using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Borders
{
    public float minX_Offset = 1.1f;
    public float maxX_Offset = 1.1f;
    public float minY_Offset = 1.1f;
    public float maxY_Offset = 1.1f;

    [HideInInspector]
    public float minX, maxX, minY, maxY;

}
public class MainPlayerPilot : AbstructPilot<Vector2>
{
    public Borders borders;
    protected override void Start()
    {
        base.Start();
        ResizeBorders();
    }
    protected override void GoTo()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 dislocate = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dislocate.y += 1.5f;
            Driver.GoTo(dislocate);
        }
        gameObject.transform.position = new Vector2(Mathf.Clamp(gameObject.transform.position.x, borders.minX, borders.maxX),
                        Mathf.Clamp(gameObject.transform.position.y, borders.minY, borders.maxY));
    }

    private void ResizeBorders()
    {
        borders.minX = Camera.main.ViewportToWorldPoint(Vector2.zero).x + borders.minX_Offset;
        borders.minY = Camera.main.ViewportToWorldPoint(Vector2.zero).y + borders.minY_Offset;
        borders.maxX = Camera.main.ViewportToWorldPoint(Vector2.right).x - borders.maxX_Offset;
        borders.maxY = Camera.main.ViewportToWorldPoint(Vector2.up).y - borders.maxY_Offset;
    }
}

