
public class PlayerBaseWeaponChargeShotState : PlayerCombatState
{
    public override PlayerCombatStateType Type { get; } = PlayerCombatStateType.ChargedShot;

    public PlayerBaseWeaponChargeShotState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
    {
        _player = player;
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        _player.SetCombatState(new PlayerBaseWeaponIdleState(_commandSystem, _player));
    }
}