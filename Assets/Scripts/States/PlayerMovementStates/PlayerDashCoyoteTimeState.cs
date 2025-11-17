using UnityEngine;

public class PlayerDashCoyoteTimeState : PlayerMovementState
{
    public override PlayerMovementStateType Type { get; } = PlayerMovementStateType.DashCoyoteTime;
    public override CurrentSpeedType SpeedType { get; } = CurrentSpeedType.Dash;
    private float _maxGravityForce = 20;
    private float _gravityForce = 0;
    private float _timeInState = 0;
    private float _coyoteTimeDuration = .1f;

    public PlayerDashCoyoteTimeState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
    {
    }

    public override void Update(float dt)
    {
        _timeInState += dt;

        if (_timeInState >= _coyoteTimeDuration)
        {
            _player.SetMovementState(new PlayerDashJumpingFallingState(_commandSystem, _player));
            return;
        }

        base.Update(dt);

        var groundPoint = GetClosestGroundPoint().point;
        if (groundPoint.y < _player.transform.position.y - _player.ColliderHeight / 2)
        {

            _gravityForce += dt * _maxGravityForce * 5f;

            if (_gravityForce >= _maxGravityForce)
            {
                _gravityForce = _maxGravityForce;
            }

            //_player.transform.position += _gravityForce * dt * Vector3.down;
        }
        else
        {

            _player.SetMovementState(new PlayerIdleState(_commandSystem, _player));
            return;
        }
    }

    protected override void OnRightStart(Command command)
    {
        if (_walled && _player.FacingRight)
        {
            _player.SetMovementState(new PlayerWallSlidingState(_commandSystem, _player));
        }
        base.OnRightStart(command);
    }
    
    protected override void OnLeftStart(Command command)
    {
        if (_walled && !_player.FacingRight)
        {
            _player.SetMovementState(new PlayerWallSlidingState(_commandSystem, _player));
        }
        base.OnLeftStart(command);
    }
    protected override void OnJumpStart(Command command)
    {
        base.OnJumpStart(command);
        _player.SetMovementState(new PlayerDashJumpingState(_commandSystem, _player));
    }
}
