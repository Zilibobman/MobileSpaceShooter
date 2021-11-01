using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpShot : MonoBehaviour, IMakeAShot
{
    [SerializeField] int BulletCount;
    [SerializeField] float MinAngle;
    [SerializeField] float MaxAngle;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {

    }
    public void MakeAShot(GameObject Bullet)
    {
        for (float zZz = MinAngle; zZz < MaxAngle; zZz += (MaxAngle - MinAngle) / BulletCount)
        {
            // Create an instance of the prefab obj_Bullet_Boss in the boss position and 
            // rotates zZz degrees around the z axis
            Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, zZz)).SetActive(true);
        }
    }
}
