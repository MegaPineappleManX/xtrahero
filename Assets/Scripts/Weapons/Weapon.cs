using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    BaseWeapon,
    Invalid
}

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    public int MaxActiveShots = 3;
    public List<int> ShotSpeeds;
    public List<int> DamageAmounts;
    public List<float> ChargeIntervals;
    public List<GameObject> Prefabs;
    public WeaponType WeaponType;
    public PlayerCombatState GetInitialCombatState(CommandSystem commandSystem, Player player) {
        return WeaponType switch
        {
            WeaponType.BaseWeapon => new PlayerBaseWeaponIdleState(commandSystem, player),
            _ => throw new System.NotImplementedException(),
        };
    }
}
