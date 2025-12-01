using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponShot : WeaponShot
{
	private int _chargeLevel;
	private float _timeActive = 0.25f;
	private Transform _playerTransform;

	public void Init(Equipable weaponData, Transform playerTransform, Vector3 direction)
	{
		base.Init(weaponData, playerTransform.position, direction, null, null);
		_chargeLevel = 0;
		_playerTransform = playerTransform;
		transform.localRotation = direction.x == 1 ? transform.localRotation : Quaternion.Euler(0, 180, 0);
	}

	private void Update()
	{
		_timeActive -= Time.deltaTime;
		transform.position = _playerTransform.position;
		
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
			var damagePart = _weaponData.GetPart<DamageAmountPart>();
			if (damageComponent.Hit(damagePart == null ? 0 : damagePart.DamageAmount))
			{
				OnHit(other);
			}
		}
		
		var reflectComponent = other.GetComponent<IReflectable>();
		if (reflectComponent != null)
        {
            reflectComponent.Reflect(_playerTransform.position);
        }
	}

	private void OnHit(Collider other)
	{
	}
}
