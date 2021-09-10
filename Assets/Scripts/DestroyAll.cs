using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAll : MonoBehaviour
{
    private BoxCollider2D _boundare_Collider;
    private Vector2 _viewport_Size;

    private void Awake()
    {
        _boundare_Collider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        ResizeCollider();
    }
    void ResizeCollider()
    {
        _viewport_Size = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) * 2;
        _viewport_Size.x = 1.5f;
        _viewport_Size.y = 1.5f;
    }
    public void OnTriggerExit2D(Collider2D coll)
    {
        switch(coll.tag)
        {
            case "Planet":
                DestroyRec(coll.gameObject);
                break;
            case "Bullet":
                DestroyRec(coll.gameObject);
                break;
        }
    }
    private void DestroyRec(GameObject obj)
    {
        if(obj.transform.parent != null)
        {
            DestroyRec(obj.transform.parent.gameObject);
        }
        else
        {
            Destroy(obj);
        }
    }
}
