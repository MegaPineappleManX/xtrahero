using UnityEngine;

public class PlayerWallJumpingState : PlayerMovementState
{
    public override PlayerMovementStateType Type { get; } = PlayerMovementStateType.WallJumping;
    private float _currentJumpForce;
    private bool _rightSideWall;

    public PlayerWallJumpingState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _currentJumpForce = _player.JumpForce;
        _rightSideWall = _player.FacingRight;
    }

    public override void Update(float dt)
    {
        if (_currentJumpForce > _player.JumpForce / 4)
        {
            _rightPressed = !_rightSideWall;
            _leftPressed = !_rightPressed;
        }
        base.Update(dt);
        if (_currentJumpForce <= 1)
        {
            SetFallingState();
            return;
        }
        _currentJumpForce -= dt * _currentJumpForce * 10;

        var hitPoint = GetClosestCeilingPoint().point;
        var tempPos = _player.transform.position + _currentJumpForce * dt * Vector3.up;
        var halfColliderHeight = _player.ColliderHeight / 2;
        if (hitPoint.y <= tempPos.y + halfColliderHeight) {
            tempPos.y = hitPoint.y - halfColliderHeight;
            _player.transform.position = tempPos;
            SetFallingState();
            return;
        }

        _player.transform.position = tempPos;
    }

    protected virtual void SetFallingState()
    {
        _player.SetMovementState(new PlayerFallingState(_commandSystem, _player));
    }

    protected override void OnLeftStart(Command command)
    {
        if (_currentJumpForce <= _player.JumpForce / 4)
        {
            base.OnLeftStart(command);
        }
    }

    protected override void OnRightStart(Command command)
    {
        if (_currentJumpForce <= _player.JumpForce / 4)
        {
            base.OnRightStart(command);
        }
    }
    protected override void OnRightEnd(Command command)
    {
        if (_currentJumpForce <= _player.JumpForce / 4)
        {
            base.OnRightEnd(command);
        }
    }
    protected override void OnLeftEnd(Command command)
    {
        if (_currentJumpForce <= _player.JumpForce / 4)
        {
            base.OnLeftEnd(command);
        }
    }

    protected override void OnJumpEnd(Command command)
    {
        base.OnJumpEnd(command);
        SetFallingState();
    }
}