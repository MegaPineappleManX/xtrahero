using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponShot : WeaponShot
{
	private int _chargeLevel;
	
	private float _timeActive = 0.25f;

	public void Init(Weapon weaponData, Vector3 origin, Vector3 direction, List<GameObject> targetObjects = null, List<Vector3> targetPostions = null, int chargeLevel = 0)
	{
		base.Init(weaponData, origin, direction, targetObjects, targetPostions);
		_chargeLevel = chargeLevel;
	}

	private void Update()
	{
		_timeActive -= Time.deltaTime;
		
		if (_timeActive <= 0) 
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (_triggered)
		{
			return;
		}

		var damageComponent = other.GetComponent<IDamageable>();
		if (damageComponent != null && damageComponent.GetDamagableObjectType() != DamagableObjectType.Player)
		{
			_triggered = true;
			if (damageComponent.Hit(_weaponData.DamageAmounts[_chargeLevel]))
			{
				OnHit();
			}
		}
	}

	private void OnHit()
	{
		//Destroy(gameObject);
	}
}
