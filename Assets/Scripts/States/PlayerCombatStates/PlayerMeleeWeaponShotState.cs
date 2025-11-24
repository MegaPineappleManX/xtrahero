
public class PlayerMeleeWeaponShotState : PlayerCombatState
{
	public override PlayerCombatStateType Type { get; } = PlayerCombatStateType.Shot;
    
	private float _timeInState = 0;

    public PlayerMeleeWeaponShotState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
    {
        _player = player;
    }

    public override void Update(float dt)
    {
	    base.Update(dt);
	    _timeInState += dt;
	    
	    if (_timeInState >= 0.25) 
	    {
		    _player.SetCombatState(new PlayerMeleeWeaponIdleState(_commandSystem, _player));
	    }
	    
    }
}