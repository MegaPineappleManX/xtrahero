using UnityEngine;

public class PlayerBaseWeaponChargingState : PlayerCombatState
{
    public override PlayerCombatStateType Type { get; } = PlayerCombatStateType.Charging;
    private float _timeInState;

    public PlayerBaseWeaponChargingState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
    {
        _player = player;
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        _timeInState += dt;
    }

    protected override void OnShootEnd(Command command)
    {
        base.OnShootEnd(command);
	    var weapon = _player.ActiveWeapon;
        int chargeLevel = 0;

        GameObject prefabToSpawn = weapon.GetPart<ChargePart>().ChargeLevels[0].Prefab;

        for (int i = 1; i < weapon.GetPart<ChargePart>().ChargeLevels.Count; ++i)
        {
            var chargeLevelData = weapon.GetPart<ChargePart>().ChargeLevels[i];
            if (_timeInState >= chargeLevelData.MinChargeTime)
            {
                prefabToSpawn = chargeLevelData.Prefab;
                chargeLevel = i;
            }
        }

        var shot = GameObject.Instantiate(prefabToSpawn, _player.transform.position, Quaternion.identity);
        shot.GetComponent<ChargableWeaponShot>().Init(_player.ActiveWeapon, _player.transform.position, _player.ShootingRight ? Vector3.right : Vector3.left, null, null, chargeLevel);

        if (chargeLevel > 0)
        {
            _player.SetCombatState(new PlayerBaseWeaponChargeShotState(_commandSystem, _player));
            return;
        }

        _player.SetCombatState(new PlayerBaseWeaponShotState(_commandSystem, _player));
    }
}