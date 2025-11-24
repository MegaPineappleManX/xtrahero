using UnityEngine;

public class PlayerMeleeWeaponIdleState : PlayerCombatState
{
    public override PlayerCombatStateType Type { get; } = PlayerCombatStateType.Idle;

    public PlayerMeleeWeaponIdleState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
    {
        _player = player;
    }

    public override void Update(float dt)
    {
        base.Update(dt);
    }

    protected override void OnShootStart(Command command)
    {
	    base.OnShootStart(command);
        
	    var weapon = _player.ActiveWeapon;
	    
	    GameObject prefabToSpawn = weapon.Prefabs[0];
	    
	    var shot = GameObject.Instantiate(prefabToSpawn, _player.transform.position, Quaternion.identity);
	    shot.GetComponent<MeleeWeaponShot>().Init(_player.ActiveWeapon, _player.transform, _player.ShootingRight ? Vector3.right : Vector3.left);

	    _player.SetCombatState(new PlayerMeleeWeaponShotState(_commandSystem, _player));
    }
}