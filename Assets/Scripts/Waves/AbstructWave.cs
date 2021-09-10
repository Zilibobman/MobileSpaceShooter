using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

public abstract class AbstructWave : MonoBehaviour, IEnemyWave
{
    public GameObject obj_Enemy;
    protected GameObject obj_modifiEnemy;
    protected AbstructShip enemyShip;
    public int count_in_Wave;
    public float time_Spawn;
    protected List<GameObject> AliveEnemys = new List<GameObject>();
    protected IEnumerable<IShipSettingsChanger> shipSettings;

    public event EventHandler WaveComplit;

    protected IEnumerable<IShipSettingsChanger> ShipSettings
    { get
        {
            if (shipSettings == null)
            {
                shipSettings = gameObject.GetComponents<IShipSettingsChanger>();
            }
            return shipSettings;
        }
    }
    
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
        GameObject new_enemy = Instantiate(obj_modifiEnemy, obj_Enemy.transform.position, Quaternion.identity);
        AliveEnemys.Add(new_enemy);
        //new_enemy.transform.Find("Ship").GetComponent<AbstructShip>().ShipWasDestroy += CheckAliveEnemys;
        new_enemy.SetActive(true);
        return new_enemy;
    }

    protected void Awake()
    {
        obj_modifiEnemy = Instantiate(obj_Enemy);
        obj_modifiEnemy.SetActive(false);
        GetEnemyComponents(obj_modifiEnemy);
        ModyifyEnemy();
    }
    // Start is called before the first frame update
    protected void Start()
    {
        StartCoroutine(CreateEnemyWave());
    }

    protected virtual void GetEnemyComponents(GameObject Enemy)
    {
        enemyShip = Enemy.transform.Find("Ship").GetComponent<AbstructShip>();
    }
    protected virtual void ModyifyEnemy()
    {
        foreach(IShipSettingsChanger changer in ShipSettings)
        {
            changer.ChangeSettinge(enemyShip);
        }
    }
    protected virtual void CheckAliveEnemys()
    {
        foreach(var enemy in AliveEnemys)
        {
            if (enemy == null)
                AliveEnemys.Remove(enemy);
        }
        if (AliveEnemys.Count == 0)
            Destroy(gameObject);
    }
}
