using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Specifications
{
    HP,
    Speed,
    Shield
}
public enum TypesOfGun
{
    Central,
    Side,
    All
}
public enum ConflictSides
{
    Enemy,
    Player
}
public class Constants// : MonoBehaviour
{
    public const int IndexOfSpecificationsInDataBase = 2;
    public static IEnumerable<Specifications> AllSpecifications
    {
        get
        {
            return (IEnumerable<Specifications>)System.Enum.GetValues(typeof(Specifications));// new List<Specifications> { Specifications.HP, Specifications.Speed, Specifications.Shield };
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
