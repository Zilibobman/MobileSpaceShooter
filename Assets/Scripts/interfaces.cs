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
}
public interface IHaveMaxHP
{
    public int MaxHP { get; }
}
public interface IControllebleHP : IHaveHP
{
    public new int CurrentHP { get; set; }
}
public interface IControllebleMaxHP : IHaveMaxHP
{
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
    public float Speed { get; }
}
public interface IHaveMaxSpeed
{
    public int MaxSpeed { get; }
}
public interface IControllebleSpeed : IHaveSpeed
{
    public new float Speed { get; set; }
}
public interface IControllebleMaxSpeed : IHaveMaxSpeed
{
    public new int MaxSpeed { get; }
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
public interface IHaveConflictSideAndDamageble: IHaveConflictSide, IDamagable
{

}
#endregion

#region Shield
public interface IShield : IDamagable, IHaveHP, IHaveMaxHP, IHeallable, IHaveConflictSideAndDamageble
{

}
#endregion

#region Guns and Bullets
public interface IHaveDamage
{
    public int Damage { get; }
}
public interface IHaveControllableDamage : IHaveDamage
{
    public new int Damage { get; set; }
}
public interface IBullet : IHaveSpeed, IDamager, IHaveConflictSide, IHaveDamage
{

}
public interface IGun
{
    public TypesOfGun TypeOfGun { get; }
    public IMakeAShot Shoter { get; set; }
    public List<IBulletModifyer> Modifiers { get; set; }
    public ParticleSystem Particle{get; set;}
}
public interface IAutoGun : IGun
{
    public float Time_Bullet_Spawn { get;}
}
public interface IMayBeShotGun : IGun
{
    public int Shot_Chance { get; set; }
}
public interface IShotInTimeGun : IGun
{
    public float Shot_Time_Start { get; set; }
    public float Shot_Time_End { get; set; }
}
public interface IMayBeShotInTimeGun: IMayBeShotGun, IShotInTimeGun {}
public interface IMayBeShotAutoGun : IMayBeShotGun, IAutoGun {}
public interface IBulletModifyer
{
    public void Modify(AbstructBullet bullet);
}
public interface IMakeAShot
{
    public void MakeAShot(GameObject Bullet);
}
#endregion

#region Pilots
public interface IPilot
{
    public IDriver Driver { get; }
}
public interface ICanChangeDriver<DriverInput>
{
    public void ChangeDriver(IDriver<DriverInput> newDriver);
}
public interface IPilot<DriverInput> : IPilot, ICanChangeDriver<DriverInput>
{

}
public interface IPilotByTrajectory : IPilot
{
    public delegate void EventEndingTrajectory(GameObject sender, bool is_return);
    public bool Is_return { get; set; }
    public event EventEndingTrajectory IEndTrajectory;
}
public interface IPilotByTrajectory<TrajectoryType> : IPilotByTrajectory
{
    public TrajectoryType Trajectory { get; set; }
}
#endregion

#region Ships
public interface IShip : IHaveHP, IHaveMaxHP, IHaveConflictSideAndDamageble
{
    public IShield Shield {get;}
    public IDriver Driver { get; }
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

#region Participant
public interface IHaveShip
{
    public IShip Ship {get;}
}
public interface IParticipant : IHaveShip
{

}
#endregion

public interface IEnemyWave
{
    public IEnumerator CreateEnemyWave();
}
public interface IReward
{
    public int Reward { get; }
}
public interface IShipSettingsChanger
{
    public void ChangeSettinge(AbstructShip Ship);
}
