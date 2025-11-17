using UnityEngine;

public class PlayerDashJumpingState : PlayerMovementState
{
    public override PlayerMovementStateType Type { get; } = PlayerMovementStateType.DashJumping;
    public override CurrentSpeedType SpeedType { get; } = CurrentSpeedType.Dash;
    private float _currentJumpForce;

    public PlayerDashJumpingState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _currentJumpForce = _player.JumpForce;
    }

    public override void Update(float dt)
    {
        base.Update(dt);

        if (_currentJumpForce <= 1)
        {
            _player.SetMovementState(new PlayerDashJumpingFallingState(_commandSystem, _player));
            return;
        }
        _currentJumpForce -= dt * _currentJumpForce * 10;

        var ceilingHitPoint = GetClosestCeilingPoint().point;
        var ceilingTempPos = _player.transform.position + _currentJumpForce * dt * Vector3.up;
        var halfColliderHeight = _player.ColliderHeight / 2;
        if (ceilingHitPoint.y <= ceilingTempPos.y + halfColliderHeight) {
            ceilingTempPos.y = ceilingHitPoint.y - halfColliderHeight;
            _player.transform.position = ceilingTempPos;
            _player.SetMovementState(new PlayerDashJumpingFallingState(_commandSystem, _player));
            return;
        }

        _player.transform.position = ceilingTempPos;
    }

    protected override void OnJumpEnd(Command command)
    {
        base.OnJumpEnd(command);
        _player.SetMovementState(new PlayerDashJumpingFallingState(_commandSystem, _player));
    }
}