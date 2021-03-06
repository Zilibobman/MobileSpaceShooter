using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class EnemyWaves
{
    public float timeToStart;
    public GameObject wave;
    public bool is_Last_Wave;
}
public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    public GameObject[] playerShip;
    public EnemyWaves[] enemyWaves;
    private bool is_Final = false;

    public GameObject panel;
    private bool _isPause;
    public GameObject[] btnPause;
    public Text text_Score;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1;
        LoadPlayer();
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave, enemyWaves[i].is_Last_Wave));
        }
    }
    private void Update()
    {
        if(is_Final && GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !_isPause)
        {
            Debug.Log("Win");
            GamePause();
            btnPause[1].SetActive(false);
        }
        if(Player.instanse == null && !_isPause)
        {
            Debug.Log("Lose");
            GamePause();
        }
    }
    public void ScoreInGame(int score)
    {
        DataBase.instance.Score_Game += score;
        text_Score.text = "Score: " + DataBase.instance.Score_Game.ToString();
    }
    public void LoadPlayer()
    {
        Instantiate(playerShip[DataBase.instance.indexOfcurrentShip()]);
        Player.instanse.ship.CurrentHP = Player.instanse.ship.MaxHP = DataBase.instance.playerShipInfo(Specifications.HP);
        Player.instanse.ship.driver.Speed = Player.instanse.ship.driver.MaxSpeed = DataBase.instance.playerShipInfo(Specifications.Speed);
        Player.instanse.ship.shield.CurrentHP = Player.instanse.ship.shield.MaxHP = DataBase.instance.playerShipInfo(Specifications.Shield);
    }
    public void GamePause()
    {
        if(!_isPause)
        {
            _isPause = true;
            Time.timeScale = 0;
            panel.SetActive(true);
            if(Player.instanse != null)
            {
                btnPause[0].SetActive(false);
                btnPause[1].SetActive(true);
            }
            else
            {
                btnPause[0].SetActive(true);
                btnPause[1].SetActive(false);
            }
        }
        else
        {
            _isPause = false;
            Time.timeScale = 1;
            panel.SetActive(false);
        }
    }

    public void BtnRestartGame()
    {
        DataBase.instance.Score_Game = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BtnExitGame()
    {
        DataBase.instance.SaveGame();
        DataBase.instance.GameLoadScene("Menu");
    }
    IEnumerator CreateEnemyWave(float delay, GameObject Wave, bool Final)
    {
        if(delay != 0)
        {
            yield return new WaitForSeconds(delay);
        }
        if(Player.instanse != null)
        {
            Instantiate(Wave);
        }    
        if(Final)
        {
            is_Final = true;
        }
    }
}
