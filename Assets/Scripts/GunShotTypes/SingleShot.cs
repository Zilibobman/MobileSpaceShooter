using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : MonoBehaviour, IMakeAShot
{
    void Start()
    {

    }
    void Update()
    {

    }
    public void MakeAShot(GameObject Bullet)
    {
        Instantiate(Bullet, transform.position, Quaternion.identity /*Quaternion.Euler(Vector3.zero)*/).SetActive(true);
    }
}
