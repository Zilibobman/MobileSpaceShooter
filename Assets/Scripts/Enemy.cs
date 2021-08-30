using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemy_Health;
    public int score_Value;

    [Space]
    public GameObject obj_Bullet;
    public float shot_Time_Min, shot_Time_Max;
    public int shot_Chance;

    [Header("BOSS")]
    public bool is_Boss;
    public GameObject obj_Bullet_Boss;
    public float time_Bullet_Boss_Spawn;
    private float _timer_Shot_Boss;
    public int shot_Chance_Boss;

    private void Start()
    {
        if (!is_Boss)
        {
            Invoke("OpenFire", Random.Range(shot_Time_Min, shot_Time_Max));
        }
    }
    private void Update()
    {
        if(is_Boss)
        {
            if(Time.time > _timer_Shot_Boss)
            {
                _timer_Shot_Boss = Time.time + time_Bullet_Boss_Spawn;
                OpenFire();
                OpenFireBoss();
            }
        }
    }
    private void OpenFireBoss()
    {
        if(Random.value < (float)shot_Chance_Boss / 100)
        {
            for (int zZz = -40; zZz < 40; zZz += 10)
            {
                Instantiate(obj_Bullet_Boss, transform.position, Quaternion.Euler(0, 0, zZz));
            }
        }
    }
    void OpenFire()
    {
        if(Random.value < (float)shot_Chance /100)
        {
            Instantiate(obj_Bullet, transform.position, Quaternion.identity);
        }
    }
    public void GetDamage(int damage)
    {
        enemy_Health -= damage;
        if(enemy_Health <= 0)
        {
            Destuction();
        }
    }

    private void Destuction()
    {
        LevelController.instance.ScoreInGame(score_Value);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player_Ship")
        {
            Player.instanse.GetDamage(1);
            GetDamage(1);
        }
        if (coll.tag == "Shield")
        {
            Player.instanse.GetDamageShield(1);
            GetDamage(1);
        }
    }
}
