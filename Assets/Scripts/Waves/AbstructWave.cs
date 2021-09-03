using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstructWave : MonoBehaviour, IEnemyWave
{
    public GameObject obj_Enemy;
    protected AbstructShip enemyShip;
    public int count_in_Wave;
    public float time_Spawn;
    
    public virtual IEnumerator CreateEnemyWave()
    {
        for (int i = 0; i < count_in_Wave; i++)
        {
            CreateEnemy();

            yield return new WaitForSeconds(time_Spawn);
        }
    }
    protected virtual GameObject CreateEnemy()
    {
        GameObject new_enemy = Instantiate(obj_Enemy, obj_Enemy.transform.position, Quaternion.identity);

        new_enemy.SetActive(true);
        return new_enemy;
    }

    // Start is called before the first frame update
    protected void Start()
    {
        GetEnemysComponents();
        ModyifyEnemys();
        StartCoroutine(CreateEnemyWave());
    }

    protected virtual void GetEnemysComponents()
    {
        enemyShip = obj_Enemy.transform.Find("Ship").GetComponent<AbstructShip>();
    }
    protected virtual void ModyifyEnemys()
    {
        foreach(IShipSettingsChanger changer in gameObject.GetComponentsInChildren<IShipSettingsChanger>())
        {
            changer.ChangeSettinge(enemyShip);
        }
    }
}
