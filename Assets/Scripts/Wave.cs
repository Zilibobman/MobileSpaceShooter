using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShootingSettings
{
    [Range(0, 100)]
    public int shot_Chance;
    public float shot_Time_Min, shot_Time_Max;
}
public class Wave : MonoBehaviour
{
    public ShootingSettings shooting_Settings;
    [Space]
    public GameObject obj_Enemy;
    public int count_in_Wave;
    public float speed_Enemy;
    public float time_Spawn;
    public Transform[] path_Points;
    public bool is_return;

    [Header("Test Wave!")]
    public bool is_Test_Wave;

    private FollowThePath follow_Component;
    public Enemy enemy_Component_Script;

    private void Start()
    {
        StartCoroutine(CreateEnemyWave());
    }

    IEnumerator CreateEnemyWave()
    {
        for (int i = 0; i < count_in_Wave; i++)
        {
            GameObject new_enemy = Instantiate(obj_Enemy, obj_Enemy.transform.position, Quaternion.identity);

            follow_Component = new_enemy.GetComponent<FollowThePath>();
            follow_Component.path_Points = path_Points;
            follow_Component.speed_Enemy = speed_Enemy;
            follow_Component.is_return = is_return;

            enemy_Component_Script = new_enemy.GetComponent<Enemy>();
            enemy_Component_Script.shot_Chance = shooting_Settings.shot_Chance;
            enemy_Component_Script.shot_Time_Min = shooting_Settings.shot_Time_Min;
            enemy_Component_Script.shot_Time_Max = shooting_Settings.shot_Time_Max;

            new_enemy.SetActive(true);

            yield return new WaitForSeconds(time_Spawn);
        }
        if (is_Test_Wave)
        {
            yield return new WaitForSeconds(5f);
            StartCoroutine(CreateEnemyWave());
        }
        if (!is_return)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        NewPositionByPath(path_Points);
    }

    void NewPositionByPath( Transform[] path)
    {
        Vector3[] path_Positions = new Vector3[path.Length];
        for(int i = 0; i < path.Length; i++)
        {
            path_Positions[i] = path[i].position;
        }
        path_Positions = Smoothing(path_Positions);
        path_Positions = Smoothing(path_Positions);
        path_Positions = Smoothing(path_Positions);
        for (int i = 0; i < path_Positions.Length - 1; i++)
        {
            Gizmos.DrawLine(path_Positions[i], path_Positions[i + 1]);
        }
    }

    Vector3[] Smoothing(Vector3[] path_Positions)
    {
        Vector3[] new_Path_Positions = new Vector3[(path_Positions.Length - 2) * 2 + 2];
        new_Path_Positions[0] = path_Positions[0];
        new_Path_Positions[new_Path_Positions.Length - 1] = path_Positions[path_Positions.Length - 1];

        int j = 1;
        for (int i = 0; i < path_Positions.Length - 2; i++)
        {
            new_Path_Positions[j] = path_Positions[i] + (path_Positions[i + 1] - path_Positions[i]) * 0.75f;
            new_Path_Positions[j + 1] = path_Positions[i + 1] + (path_Positions[i + 2] - path_Positions[i + 1]) * 0.25f;
            j += 2;
        }
        return new_Path_Positions;
    }
}
