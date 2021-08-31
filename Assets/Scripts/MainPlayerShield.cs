using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPlayerShield : MonoBehaviour, IMainPlayerShield
{
    private Slider slider;

    public int currentHP = 0;
    public int CurrentHP { get => currentHP; set => currentHP = value; }

    public int maxHP = 0;
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public ConflictSides ConflictSide => ConflictSides.Player;

    public void GetDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            gameObject.SetActive(false);
        }
        changeSliderValue();
    }

    public void Heal(int HP)
    {
        currentHP += HP;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        if (!gameObject.activeSelf && currentHP > 0)
        {
            gameObject.SetActive(true);
        }
        changeSliderValue();
    }
    private void changeSliderValue()
    {
        slider.value = currentHP;
    }

    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.FindGameObjectWithTag("sl_Shield").GetComponent<Slider>();
        slider.maxValue = Constants.GetMaxSpecificationValue(Specifications.Shield);
        changeSliderValue();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
