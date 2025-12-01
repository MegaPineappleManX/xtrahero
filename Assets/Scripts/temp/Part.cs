using UnityEngine;
using System.Collections.Generic;

public enum DamageType
{
    Default,
    Fire,
}
public enum WeaponType
{
	BaseWeapon,
	MeleeWeapon,
    Invalid
}

public abstract class Part
{
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
    public List<ChargeLevel> ChargeLevels;
}

public class ShotCountPart : Part
{
    public int MaxActiveShots;
}

public class WeaponTypePart : Part
{
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
    public DamageType DamageType;
}

public class PrefabPart : Part
{
    public GameObject Prefab;
}

public class DamageAmountPart : Part
{
    public int DamageAmount;
}