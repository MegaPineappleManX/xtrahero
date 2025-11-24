using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapon", menuName = "Scriptable Objects/Weapons/Melee Weapon")]
public class MeleeWeapon : Weapon
{
	public int DamageAmounts;
	public GameObject Prefab;
}
