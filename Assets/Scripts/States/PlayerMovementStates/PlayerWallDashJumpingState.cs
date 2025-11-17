using UnityEngine;

public class PlayerWallDashJumpingState : PlayerWallJumpingState
{
    public override PlayerMovementStateType Type { get; } = PlayerMovementStateType.WallJumping;
    public override CurrentSpeedType SpeedType { get; } = CurrentSpeedType.Dash;

    public PlayerWallDashJumpingState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
    {
    }

    protected override void SetFallingState()
    {
        _player.SetMovementState(new PlayerDashJumpingFallingState(_commandSystem, _player));
    }
}