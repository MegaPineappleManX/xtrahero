using UnityEngine;

public enum PlayerCombatStateType
{
    Idle,
    Charging,
    Shot,
    ChargedShot,
    Damaged,
    Invalid
}


public abstract class PlayerCombatState : EntityState
{
    protected Player _player;

    public virtual PlayerCombatStateType Type { get; } = PlayerCombatStateType.Invalid;

    public PlayerCombatState(CommandSystem commandSystem, Player player) : base(commandSystem)
    {
        _player = player;
    }

    public override void Update(float dt)
    {
        base.Update(dt);
    }

    protected override void ProcessCommand(Command command)
    {
        switch (command.Type)
        {
            case CommandType.ShootStart:
                OnShootStart(command);
                break;
            case CommandType.ShootEnd:
                OnShootEnd(command);
	            break;
            case CommandType.WeaponChangeLeft:
            	OnWeaponChangeLeft(command);
            	break;
            case CommandType.WeaponChangeRight:
            	OnWeaponChangeRight(command);
            	break;
            default:
                OnInvalid(command);
                break;
        }
    }
    
    protected virtual void OnShootStart(Command command) { }
	protected virtual void OnShootEnd(Command command) { }
	
	protected virtual void OnWeaponChangeLeft(Command command) 
	{ 
		_player.ChangeWeapon(false);
	}
	
	protected virtual void OnWeaponChangeRight(Command command) 
	{
		_player.ChangeWeapon(true);
	}

    protected virtual void OnInvalid(Command command)
    {
    }
}