using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPlayerShield : AbstructShield
{
    private Slider slider;

    public override ConflictSides ConflictSide => ConflictSides.Player;

    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
        changeSliderValue();
    }

    public override void Heal(int HP)
    {
        base.Heal(HP);
        changeSliderValue();
    }
    private void changeSliderValue()
    {
        slider.value = currentHP;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        slider = GameObject.FindGameObjectWithTag("sl_Shield").GetComponent<Slider>();
        slider.maxValue = Constants.GetMaxSpecificationValue(Specifications.Shield);
        changeSliderValue();
    }
}
