using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#region DataBase classes
public class DataBase
{
    public static DataBase instance = new DataBase();
    public int[][] playerShipsInfo =
    {
        new int[]{1, 000, 1, 15, 3},
        new int[]{0, 550, 2, 8, 0},
        new int[]{0, 950, 3, 6, 0},
    };

    public int Score = 0;
    public int Score_Game = 0;
    public void GameLoadScene(string Name)
    {
        SceneManager.LoadScene(Name);
    }
    private int indexOfcurrentShip()
    {
        for (int i = 0; i < DataBase.instance.playerShipsInfo.Length; i++)
        {
            if (DataBase.instance.playerShipsInfo[i][0] == 1)
            {
                return i;
            }
        }
        return -1;
    }
    public int playerShipInfo(Specifications specification)
    {
        return playerShipsInfo[indexOfcurrentShip()][Constants.IndexOfSpecificationsInDataBase + (int)specification];
    }
    public void SaveGame()
    {
        Score += Score_Game;
        Score_Game = 0;
        for (int i = 0; i < playerShipsInfo.Length; i++)
        {
            for (int j = 0; j < playerShipsInfo[i].Length; j++)
            {
                PlayerPrefs.SetInt("InfoSave" + i.ToString() + j.ToString(), playerShipsInfo[i][j]);
            }
        }
        PlayerPrefs.SetInt("InfoSaveScore", Score);
    }
    public void LoadGameSave()
    {
        for(int i = 0; i < playerShipsInfo.Length; i++)
        {
            for(int j = 0; j< playerShipsInfo[i].Length; j++)
            {
                playerShipsInfo[i][j] = PlayerPrefs.GetInt("InfoSave" + i.ToString() + j.ToString());
            }
        }
        Score = PlayerPrefs.GetInt("InfoSaveScore");
    }
}
#endregion
public class MainMenu : MonoBehaviour
{
    public GameObject[] game_Panels;

    public Text Score;

    [Header("Shop panel")]
    public GameObject[] shop_Ships;
    public Text[] shop_Ship_Text;
    public GameObject btn_Shop_Buy;
    public Text shop_Btn_Byu_Cost_Text;

    [Header("Upgrade panel")]
    public Sprite[] upgrade_Sprite_Ships;
    public GameObject upgrade_Sprite_Ship;
    public Slider[] upgrade_Sliders;
    public Text[] upgrade_Show_Cost;

    private int _index;
    private int _indexBuy;

    #region Buttons Save Load Debug Exit Choise...
    public void BtnSave()
    {
        DataBase.instance.SaveGame();
    }
    public void BtnLoadGameSave()
    {
        DataBase.instance.LoadGameSave();
    }
    public void BtnDeleteSaveGame_Debug()
    {
        PlayerPrefs.DeleteAll();
        UpdateScore();
    }
    public void BtnChoiceLevelGame(string name)
    {
        DataBase.instance.GameLoadScene(name);
    }
    public void BtnExitGame()
    {
        BtnSave();
        Application.Quit();
    }
    #endregion

    private void Start()
    {
        if(PlayerPrefs.HasKey("InfoSaveScore"))
        {
            Debug.Log("found save game!!!");
            BtnLoadGameSave();
        }
        else
        {
            BtnSave();
        }
        UpdateScore();
        ShopShipHighlighting();
    }
    public void UpdateScore()
    {
        Score.text = DataBase.instance.Score.ToString();
    }
    #region Shop
    public void ShopShipHighlighting()
    {
        for(int i = 0; i < DataBase.instance.playerShipsInfo.Length; i++)
        {
            shop_Ships[i].GetComponent<Image>().color = Color.gray;
            if (DataBase.instance.playerShipsInfo[i][0] == 1)
            {
                //shop_Ships[i].GetComponent<Image>().color = Color.white;
                shop_Ship_Text[i].color = Color.green;
                _index = i;
            }
            else
            {
                //shop_Ships[i].GetComponent<Image>().color = Color.gray;
                shop_Ship_Text[i].color = Color.red;
            }
            if(DataBase.instance.playerShipsInfo[i][1] == 0)
            {
                shop_Ship_Text[i].text = "Open";
            }
            else
            {
                shop_Ship_Text[i].text = "Cost: " + DataBase.instance.playerShipsInfo[i][1].ToString();
            }
        }
        shop_Ships[_indexBuy].GetComponent<Image>().color = Color.white;
    }
    public void ShopCheckPlayerShip(int num)
    {
        _indexBuy = num;
        if (DataBase.instance.playerShipsInfo[num][1] == 0)
        {
            for(int i = 0; i < DataBase.instance.playerShipsInfo.Length; i++)
            {
                DataBase.instance.playerShipsInfo[i][0] = 0;
            }
            DataBase.instance.playerShipsInfo[num][0] = 1;
            _index = num;
            btn_Shop_Buy.SetActive(false);
        }
        if(DataBase.instance.playerShipsInfo[num][1] != 0 
            && DataBase.instance.playerShipsInfo[num][1] <= DataBase.instance.Score)
        {
            btn_Shop_Buy.SetActive(true);
            shop_Btn_Byu_Cost_Text.text = "Buy: " + DataBase.instance.playerShipsInfo[num][1].ToString();
        }
        if(DataBase.instance.playerShipsInfo[num][1] != 0
            && DataBase.instance.playerShipsInfo[num][1] > DataBase.instance.Score)
        {
            btn_Shop_Buy.SetActive(false);
        }
        ShopShipHighlighting();
    }
    public void BtnShopBuyShip()
    {
        _index = _indexBuy;
        DataBase.instance.Score = DataBase.instance.Score - DataBase.instance.playerShipsInfo[_index][1];
        DataBase.instance.playerShipsInfo[_index][1] = 0;
        UpdateScore();
        ShopCheckPlayerShip(_index);
        for(int i = 0; i < DataBase.instance.playerShipsInfo.Length; i++)
        {
            DataBase.instance.playerShipsInfo[i][0] = 0;
        }
        DataBase.instance.playerShipsInfo[_index][0] = 1;
        ShopShipHighlighting();
    }
    #endregion
    #region Upgrade
    public void UpgradesGetInformation()
    {
        upgrade_Sprite_Ship.GetComponent<Image>().sprite = upgrade_Sprite_Ships[_index];
        foreach (var sp in Constants.AllSpecifications)
        {
            upgrade_Show_Cost[(int)sp].text = "Cost: " + Constants.GetSpecificationCost(sp).ToString();
            upgrade_Sliders[(int)sp].value = (float)DataBase.instance.playerShipsInfo[_index][(int)sp + Constants.IndexOfSpecificationsInDataBase] / Constants.GetMaxSpecificationValue(sp);
        }
    }
    public void Btn_Upgrade(int IndexOfSpec)
    {
        Specifications specification = (Specifications)IndexOfSpec;
        if (DataBase.instance.Score >= Constants.GetSpecificationCost(specification) 
            && DataBase.instance.playerShipsInfo[_index][(int)specification + Constants.IndexOfSpecificationsInDataBase] < Constants.GetMaxSpecificationValue(specification))
        {
            DataBase.instance.Score -= Constants.GetSpecificationCost(specification);
            DataBase.instance.playerShipsInfo[_index][(int)specification + Constants.IndexOfSpecificationsInDataBase] += 1;
            upgrade_Sliders[(int)specification].value += (float)1 / Constants.GetMaxSpecificationValue(specification);
        }
        UpdateScore();
    }
    #endregion
    public void Show_Change_Panel(int index)
    {
        ShopShipHighlighting();
        _indexBuy = _index;
        game_Panels[index].SetActive(true);
    }
    public void Hide_Change_Panel(int index)
    {
        BtnSave();
        game_Panels[index].SetActive(false);
    }
}
