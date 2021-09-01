using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

#region HP and Damage
public interface IDamagable
{
    public void GetDamage(int damage);
}
public interface IHaveHP
{
    public int CurrentHP { get; }
    public int MaxHP { get; }
}
public interface IControllebleHP : IHaveHP
{
    public new int CurrentHP { get; set; }
    public new int MaxHP { get; set; }
}
public interface IHeallable
{
    public void Heal(int HP);
}
public interface IDamager
{
    public void Hurt(IDamagable victim);
}
#endregion

#region Speed Mooving and Driver
public interface IHaveSpeed
{
    public int Speed { get; }
}
public interface IHaveMaxSpeed
{
    public int MaxSpeed { get; }
}
public interface IControllebleSpeed : IHaveSpeed, IHaveMaxSpeed
{
    public new int Speed { get; set; }
    public new int MaxSpeed { get; set; }
}
public interface IDriver : IHaveSpeed, IHaveMaxSpeed
{
    public Transform Ship { get; }
}
public interface IDriver<DriverInput> : IDriver
{
    public void GoTo(DriverInput destination);
}

public interface IMoovingByTrjectory<MoovingBy>
{
    public void GoTo(MoovingBy Trjectory);
}
#endregion

#region Conflict Side
public interface IHaveConflictSide
{
    public ConflictSides ConflictSide { get; }
}
public interface IHaveConflictSideAndDamageble: IHaveConflictSide,IDamagable
{

}
#endregion

#region Shield
public interface IShield : IDamagable, IHaveHP, IHeallable, IHaveConflictSideAndDamageble
{

}
#endregion

#region Guns and Bullets
public interface IBullet : IHaveSpeed, IDamager, IHaveConflictSide
{
    public int Damage{get; set;}
    public new int Speed { get; set; }
}
public interface IGun
{
    public TypesOfGun TypeOfGun { get; }
    public IMakeAShot Shoter { get; set; }
    public List<IBulletModifyer> Modifiers { get; set; }
    public ParticleSystem Particle{get;set;}
}
public interface IAutoGun : IGun
{
    public float Time_Bullet_Spawn { get; set; }
}
public interface IMayBeShotGun : IGun
{
    public int Shot_Chance { get; set; }
}
    public interface IBulletModifyer
{
    public void Modify(IBullet bullet);
}
public interface IMakeAShot
{
    public void MakeAShot(GameObject Bullet);
}
#endregion

#region Ships
public interface IPilot<DriverInput>
{
    public IDriver<DriverInput> Driver { get; }
}
public interface IPilotByTrajectory<DriverInput, TrajectoryType> : IPilot<DriverInput>
{
    public TrajectoryType Trajectory { get; set; }
}
public interface IShip<DriverType, ShieldType> : IHaveHP, IHaveConflictSideAndDamageble where DriverType : IDriver where ShieldType : IShield
{
    public ShieldType Shield {get;}
    public DriverType Driver { get; }
    public event EventHandler ShipWasDestroy;
}
public interface ICanChangeCentralGun
{
    public GameObject CentralGunSlot { get; }
    public void ChangeCentralGun(GameObject newGun);
}
public interface ICanChangeSideGun
{
    public GameObject[] SideGunsSlots { get; }
    public void ChangeSideGun(GameObject newGun);
}
public interface IHaveDiferentRagesOfFire
{
    public TypesOfGun RageOfFire { get; }
    public void ChangeRageOfFire(TypesOfGun RageOfFire);
    public void UpRageOfFire();
}
#endregion
