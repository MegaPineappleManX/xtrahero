using UnityEngine;
using System.Collections.Generic;

public enum PartType
{
    Invalid,
    Charge,
    ShotCount,
    DamageAmount,
    DamageType,
    WeaponType,
    Prefab,
}

public enum DamageType
{
    Default,
    Fire,
}

public abstract class Part
{
    [HideInInspector]
    public virtual PartType Type => PartType.Invalid;
}

public class ChargeLevel
{
    public float Damage; 
    public float MinChargeTime;
    public float Speed;
    public GameObject Prefab;
}

public class ChargePart : Part
{
    [HideInInspector]
    public override PartType Type => PartType.Charge;
    public List<ChargeLevel> ChargeLevels;
}

public class ShotCountPart : Part
{
    [HideInInspector]
    public override PartType Type => PartType.ShotCount;
    public int MaxActiveShots;
}

public class WeaponTypePart : Part
{
    [HideInInspector]
    public override PartType Type => PartType.WeaponType;
    public WeaponType WeaponType;

    public PlayerCombatState GetInitialCombatState(CommandSystem commandSystem, Player player) {
        return WeaponType switch
        {
	        WeaponType.BaseWeapon => new PlayerBaseWeaponIdleState(commandSystem, player),
	        WeaponType.MeleeWeapon => new PlayerMeleeWeaponIdleState(commandSystem, player),
            _ => throw new System.NotImplementedException(),
        };
    }
}

public class DamageTypePart : Part
{
    [HideInInspector]
    public override PartType Type => PartType.DamageType;
    public DamageType DamageType;
}

public class PrefabPart : Part
{
    [HideInInspector]
    public override PartType Type => PartType.Prefab;
    public GameObject Prefab;
}

public class DamageAmountPart : Part
{
    [HideInInspector]
    public override PartType Type => PartType.DamageAmount;
    public int DamageAmount;
}