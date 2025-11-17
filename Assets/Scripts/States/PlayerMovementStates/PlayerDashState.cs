using UnityEngine;

public class PlayerDashState : PlayerMovementState
{
    public override PlayerMovementStateType Type { get; } = PlayerMovementStateType.Dashing;
    public override CurrentSpeedType SpeedType { get; } = CurrentSpeedType.Dash;
    private float _timeInState = 0.0f;

    public PlayerDashState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
    {
    }

    public override void Update(float dt)
    {
        _rightPressed = _player.FacingRight;
        _leftPressed = !_rightPressed;
        base.Update(dt);

        _timeInState += dt;
        if (_timeInState >= _player.DashTime)
        {
            _player.SetMovementState(new PlayerIdleState(_commandSystem, _player));
            return;
        }

        var groundPoint = GetClosestGroundPoint().point;
        if (groundPoint.y < _player.transform.position.y - _player.ColliderHeight / 2)
        {
            // going down a slope
            var slopeBuffer = .25f;
            if ((groundPoint.y + slopeBuffer > _player.transform.position.y - _player.ColliderHeight / 2) && Vector3.Angle(Vector3.up, GetClosestGroundPoint().normal) < 30.01)
            {
                // TODO: Replace this with actual slope stuff lmao
                var tempPos = _player.transform.position;
                tempPos.y = groundPoint.y + (_player.ColliderHeight / 2);
                _player.transform.position = tempPos;
                return;
            }

            _player.SetMovementState(new PlayerDashCoyoteTimeState(_commandSystem, _player));
            return;
        }
        else
        {
            // Going up a slope
            if (groundPoint.y > _player.transform.position.y - _player.ColliderHeight / 2)
            {
                // TODO: Replace this with actual slope stuff lmao
                var tempPos = _player.transform.position;
                tempPos.y = groundPoint.y + (_player.ColliderHeight / 2);
                _player.transform.position = tempPos;
            }
        }
    }

    protected override void OnRightStart(Command command)
    {
        if (!_player.FacingRight)
        {
            _player.SetMovementState(new PlayerWalkingState(_commandSystem, _player));
        }
        base.OnRightStart(command);
    }

    protected override void OnRightEnd(Command command)
    {
        if (_player.FacingRight)
        {
            _player.SetMovementState(new PlayerWalkingState(_commandSystem, _player));
        }
        base.OnRightEnd(command);
    }

    protected override void OnLeftStart(Command command)
    {
        if (_player.FacingRight)
        {
            _player.SetMovementState(new PlayerWalkingState(_commandSystem, _player));
        }
        base.OnLeftStart(command);
    }

    protected override void OnLeftEnd(Command command)
    {
        if (!_player.FacingRight)
        {
            _player.SetMovementState(new PlayerWalkingState(_commandSystem, _player));
        }
        base.OnLeftEnd(command);
    }

    protected override void OnDashEnd(Command command)
    {
        base.OnDashEnd(command);
        _player.SetMovementState(new PlayerIdleState(_commandSystem, _player));
    }

    protected override void OnJumpStart(Command command)
    {
        base.OnJumpStart(command);
        // TODO DashJumping State
        _player.SetMovementState(new PlayerDashJumpingState(_commandSystem, _player));
    }
}
