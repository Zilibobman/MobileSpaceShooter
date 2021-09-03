using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AbstructParticipant
{
    
    public int score_Value;
    /*public int enemy_Health;
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

    public void GetDamage(int damage)
    {
        enemy_Health -= damage;
        if(enemy_Health <= 0)
        {
            Destruction();
        }
    }*/

    protected override void Destruction()
    {
        LevelController.instance.ScoreInGame(score_Value);
        Destroy(gameObject);
    }
}
