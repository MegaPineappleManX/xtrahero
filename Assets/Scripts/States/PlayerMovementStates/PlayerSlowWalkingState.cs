using UnityEngine;

public class PlayerSlowWalkingState : PlayerWalkingState
{
	public override PlayerMovementStateType Type { get; } = PlayerMovementStateType.WallJumping;
	public override CurrentSpeedType SpeedType { get; } = CurrentSpeedType.Dash;
	//public override CurrentPlatformType PlatformType  { get; } = CurrentPlatformType.Default;

	public PlayerSlowWalkingState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
	{
	}

	protected void SetFallingState()
	{
		_player.SetMovementState(new PlayerDashJumpingFallingState(_commandSystem, _player));
	}
}