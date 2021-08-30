using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : MonoBehaviour, IMakeAShot
{
    public void MakeAShot(GameObject Bullet)
    {
        Instantiate(Bullet, gameObject.transform.position, Quaternion.Euler(Vector3.zero));
    }
}
