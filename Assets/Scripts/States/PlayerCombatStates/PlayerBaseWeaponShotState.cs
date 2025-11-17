
public class PlayerBaseWeaponShotState : PlayerCombatState
{
    public override PlayerCombatStateType Type { get; } = PlayerCombatStateType.Shot;

    public PlayerBaseWeaponShotState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
    {
        _player = player;
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        _player.SetCombatState(new PlayerBaseWeaponIdleState(_commandSystem, _player));
    }
}