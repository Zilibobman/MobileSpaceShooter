using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBG : MonoBehaviour
{
    public float vertical_Size;
    private Vector2 _offset_Up;

    private void Update()
    {
        if(transform.position.y < -vertical_Size)
        {
            RepeatBackground();

        }
    }
    void RepeatBackground()
    {
        _offset_Up = new Vector2(0, vertical_Size * 2f);

        transform.position = (Vector2)transform.position + _offset_Up;
    }
}
