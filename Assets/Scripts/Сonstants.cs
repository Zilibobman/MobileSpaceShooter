using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Specifications
{
    HP,
    Speed,
    Shield
}
public class Constants// : MonoBehaviour
{
    public const int IndexOfSpecificationsInDataBase = 2;
    public static List<Specifications> AllSpecifications
    {
        get
        {
            return new List<Specifications> { Specifications.HP, Specifications.Speed, Specifications.Shield };
        }
    }
    public static int GetMaxSpecificationValue(Specifications specification)
    {
        switch(specification)
        {
            case Specifications.HP:
                return 15;
            case Specifications.Speed:
                return 20;
            case Specifications.Shield:
                return 6;
            default:
                return 0;
        }
    }
    public static int GetSpecificationCost(Specifications specification)
    {
        switch (specification)
        {
            case Specifications.HP:
                return 250;
            case Specifications.Speed:
                return 500;
            case Specifications.Shield:
                return 2500;
            default:
                return 0;
        }
    }
}
