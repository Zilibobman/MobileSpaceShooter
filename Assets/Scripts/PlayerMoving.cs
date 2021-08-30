using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public static PlayerMoving instance;
    public Borders borders;

    public int speed_Player = 5;

    private Camera _camera;

    private Vector2 _mouse_Position;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _camera = Camera.main;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            _mouse_Position = _camera.ScreenToWorldPoint(Input.mousePosition);
            _mouse_Position.y += 1.5f;

            transform.position = Vector2.MoveTowards(transform.position, _mouse_Position, speed_Player * Time.deltaTime);
        }
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, borders.minX, borders.maxX),
                                        Mathf.Clamp(transform.position.y, borders.minY, borders.maxY));

    }
}
