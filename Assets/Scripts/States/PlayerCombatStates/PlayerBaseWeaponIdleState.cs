
public class PlayerBaseWeaponIdleState : PlayerCombatState
{
    public override PlayerCombatStateType Type { get; } = PlayerCombatStateType.Idle;

    public PlayerBaseWeaponIdleState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
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
        _player.SetCombatState(new PlayerBaseWeaponChargingState(_commandSystem, _player));
    }
}