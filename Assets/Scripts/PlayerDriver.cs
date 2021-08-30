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
public class PlayerDriver : MonoBehaviour, IDriver<Vector2>
{
    public Transform ship;
    public Transform Ship => ship;

    public Borders borders;
    private Camera _camera;

    public int speed = 0;
    public int Speed => speed;
    public int maxSpeed = int.MaxValue;
    public int MaxSpeed => maxSpeed;

    public void GoTo(Vector2 destination)
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, borders.minX, borders.maxX),
                                Mathf.Clamp(transform.position.y, borders.minY, borders.maxY));
    }

    private void Awake()
    {
        _camera = Camera.main;
    }
    // Start is called before the first frame update
    private void Start()
    {
        ResizeBorders();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResizeBorders()
    {
        borders.minX = _camera.ViewportToWorldPoint(Vector2.zero).x + borders.minX_Offset;
        borders.minY = _camera.ViewportToWorldPoint(Vector2.zero).y + borders.minY_Offset;
        borders.maxX = _camera.ViewportToWorldPoint(Vector2.right).x - borders.maxX_Offset;
        borders.maxY = _camera.ViewportToWorldPoint(Vector2.up).y - borders.maxY_Offset;
    }
}
