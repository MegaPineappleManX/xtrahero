using UnityEngine;

public class PlayerDashJumpingFallingState : PlayerFallingState
{
    public override PlayerMovementStateType Type { get; } = PlayerMovementStateType.Falling;
    public override CurrentSpeedType SpeedType { get; } = CurrentSpeedType.Dash;

    public PlayerDashJumpingFallingState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
    {
    }
}
