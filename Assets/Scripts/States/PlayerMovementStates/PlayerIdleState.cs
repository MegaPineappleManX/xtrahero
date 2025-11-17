using Unity.Mathematics;
using UnityEngine;

public class PlayerIdleState : PlayerMovementState
{
    public override PlayerMovementStateType Type { get; } = PlayerMovementStateType.Idle;

    public PlayerIdleState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
    {
    }

    public override void Update(float dt)
    {
        base.Update(dt);

        var groundPoint = GetClosestGroundPoint().point;
        if (groundPoint.y < _player.transform.position.y - _player.ColliderHeight / 2)
        {
            _player.SetMovementState(new PlayerCoyoteTimeState(_commandSystem, _player));
            return;
        }
    }

    protected override void OnDashStart(Command command)
    {
        base.OnDashStart(command);
        _player.SetMovementState(new PlayerDashState(_commandSystem, _player));
    }

    protected override void OnRightStart(Command command)
    {
        base.OnRightStart(command);
        _player.SetMovementState(new PlayerWalkingState(_commandSystem, _player));
    }

    protected override void OnLeftStart(Command command)
    {
        base.OnLeftStart(command);
        _player.SetMovementState(new PlayerWalkingState(_commandSystem, _player));
    }

    protected override void OnJumpStart(Command command)
    {
        base.OnJumpStart(command);
        _player.SetMovementState(new PlayerJumpingState(_commandSystem, _player));
    }
}
