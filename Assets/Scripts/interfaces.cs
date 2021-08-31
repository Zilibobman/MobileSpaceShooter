using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public interface IDamagable
{
    public void GetDamage(int damage);
}
public interface IHaveHP
{
    public int CurrentHP { get; }
    public int MaxHP { get; }
}
public interface IHeallable
{
    public void Heal(int HP);
}
public interface IHaveSpeed
{
    public int Speed { get; }
}
public interface IHaveMaxSpeed
{
    public int MaxSpeed { get; }
}
public interface IHaveConflictSide
{
    public ConflictSides ConflictSide { get; }
}
public interface IHaveConflictSideAndDamageble: IHaveConflictSide,IDamagable
{

}
public interface IDriver<DriverInput> : IHaveSpeed, IHaveMaxSpeed
{
    public Transform Ship { get; }
    public void GoTo(DriverInput destination);
}
public interface IHaveDriver<DriverInput>
{
    IDriver<DriverInput> Driver { get; }
}
public interface IMoovingByTrjectory<MoovingBy>
{
    public void GoTo(MoovingBy Trjectory);
}
public interface IDamager
{
    public void Hurt(IDamagable victim);
}
public interface IShield : IDamagable, IHaveHP, IHeallable, IHaveConflictSideAndDamageble
{

}
public interface IMainPlayerShield : IShield 
{
    public new  int CurrentHP { get; set; }
    public new int MaxHP { get; set; }
}
public interface IBullet : IHaveSpeed, IDamager, IHaveConflictSide
{
    public int Damage{get; set;}
    public new int Speed { get; set; }
}
public interface IGun
{
    public TypesOfGun TypeOfGun { get; }
    public float Time_Bullet_Spawn { get; set; }
    public IMakeAShot Shoter { get; set; }
    public List<IBulletModifyer> Modifiers { get; set; }
    public ParticleSystem Particle{get;set;}
}
public interface IBulletModifyer
{
    public void Modify(IBullet bullet);
}
public interface IMakeAShot
{
    public void MakeAShot(GameObject Bullet);
}
public interface ICanChangeCentralGun
{
    public void ChangeCentralGun(GameObject CentralGun);
}
public interface ICanChangeSideGun
{
    public void ChangeSideGun(GameObject SideGun);
}
public interface IHaveDiferentRagesOfFire
{
    public TypesOfGun RageOfFire { get;}
    public void ChangeRageOfFire(TypesOfGun RageOfFire);
    public void UpRageOfFire();
}
public interface IShip<DriverInput, MoovingBy> : IDamagable, IHaveHP, IHaveConflictSideAndDamageble, IHaveDriver<DriverInput>, IMoovingByTrjectory<MoovingBy>
{
    public event EventHandler ShipWasDestroy;
}
public interface IPlayerShip<DriverInput, MoovingBy> : IShip<DriverInput, MoovingBy>, IHeallable, IHaveDiferentRagesOfFire
{
    public new int CurrentHP { get; set; }
    public new int MaxHP { get; set; }
}
public interface IMainPlayer
{
    public IPlayerShip<Vector2, Vector2> Ship { get; }
}
