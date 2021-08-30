using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    public static Player instanse = null;

    public int player_Health = 10;

    public GameObject obj_Shield;
    public int shield_Health;
    private UnityEngine.UI.Slider _slider_hp_Player;
    private UnityEngine.UI.Slider _slider_hp_Shield;

    private void Awake()
    {
        if (instanse == null)
            instanse = this;
        else
            Destroy(gameObject);


        _slider_hp_Player = GameObject.FindGameObjectWithTag("sl_HP").GetComponent<Slider>();
        _slider_hp_Shield = GameObject.FindGameObjectWithTag("sl_Shield").GetComponent<Slider>();
    }
    private void Start()
    {

        _slider_hp_Player.value = (float)player_Health / Constants.GetMaxSpecificationValue(Specifications.HP);
        if (shield_Health > 0)
        {
            obj_Shield.SetActive(true);
            _slider_hp_Shield.value = (float)shield_Health / Constants.GetMaxSpecificationValue(Specifications.Shield);
        }
        else
        {
            obj_Shield.SetActive(false);
            _slider_hp_Shield.value = 0;
        }
    }

    public void GetDamageShield(int damage)
    {
        shield_Health -= damage;
        _slider_hp_Shield.value = (float)shield_Health / Constants.GetMaxSpecificationValue(Specifications.Shield);
        if(shield_Health <= 0)
        {
            obj_Shield.SetActive(false);
        }
    }

    public void GetDamage(int damage)
    {
        player_Health -= damage;
        _slider_hp_Player.value = (float)player_Health / Constants.GetMaxSpecificationValue(Specifications.HP);
        if (player_Health <= 0)
        {
            Destruction();
        }
    }
    private void Destruction()
    {
        Destroy(gameObject);
    }
}
