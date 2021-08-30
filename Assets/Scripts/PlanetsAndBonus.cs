using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsAndBonus : MonoBehaviour
{
    public GameObject obj_Bonus;
    public float time_Bonus_Spawn;
    public GameObject[] obj_Planets;

    public float time_Planet_Spawn;
    public float speed_Planets;
    List<GameObject> planetList = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(BonusCreation());
        StartCoroutine(PlanetsCreation());
    }
    private void Update()
    {
        
    }
    IEnumerator BonusCreation()
    {
        while(true)
        {
            yield return new WaitForSeconds(time_Bonus_Spawn);
            Instantiate(
                obj_Bonus,
                new Vector2(Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX),
                            PlayerMoving.instance.borders.maxY * 1.5f),
                Quaternion.identity);

        }
    }
    IEnumerator PlanetsCreation()
    {
        for(int i = 0; i<obj_Planets.Length; i++)
        {
            planetList.Add(obj_Planets[i]);
        }
        yield return new WaitForSeconds(7);
        while(true)
        {
            int randomIndex = Random.Range(0, planetList.Count);
            GameObject newPlanet = Instantiate(planetList[randomIndex],
                new Vector2(Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX),
                PlayerMoving.instance.borders.maxY * 2f),
                Quaternion.Euler(0, 0, Random.Range(-25, 25))
                );
            planetList.RemoveAt(randomIndex);
            if(planetList.Count == 0)
            {
                for (int i = 0; i < obj_Planets.Length; i++)
                {
                    planetList.Add(obj_Planets[i]);
                }
            }
            newPlanet.GetComponent<ObjMoving>().speed = speed_Planets;
            yield return new WaitForSeconds(time_Planet_Spawn);
        }
    }
}
