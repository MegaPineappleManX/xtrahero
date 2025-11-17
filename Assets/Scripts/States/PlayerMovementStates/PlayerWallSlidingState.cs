using UnityEngine;

public class PlayerWallSlidingState : PlayerMovementState
{
    public override PlayerMovementStateType Type { get; } = PlayerMovementStateType.WallSliding;
    private float _gravityForce = 0;
    private bool _rightSideWall;

    public PlayerWallSlidingState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
    {
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();
        _rightSideWall = _player.FacingRight;
    }

    public override void Update(float dt)
    {
        //base.Update(dt);
        
        if (_rightSideWall)
        {
            var hitPoint = GetClosestRightPoint().point;
            var tempPos = _player.transform.position;
            var halfColliderWidth = _player.ColliderWidth / 2;
            Debug.DrawLine(hitPoint, tempPos);

            _walled = tempPos.x >= hitPoint.x - halfColliderWidth ? true : false;
        }
        else
        {
            var hitPoint = GetClosestLeftPoint().point;
            var tempPos = _player.transform.position;
            var halfColliderWidth = _player.ColliderWidth / 2;
            Debug.DrawLine(hitPoint, tempPos);

            _walled = tempPos.x <= hitPoint.x + halfColliderWidth ? true : false;
        }

        if (!_walled)
        {
            _player.SetMovementState(new PlayerFallingState(_commandSystem, _player));
        }

        var groundPoint = GetClosestGroundPoint().point;
        if (groundPoint.y < _player.transform.position.y - _player.ColliderHeight / 2)
        {

            _gravityForce += dt;

            if (_gravityForce >= _player.MaxWallSlideGravity)
            {
                _gravityForce = _player.MaxWallSlideGravity;
            }

            _player.transform.position += _gravityForce * dt * Vector3.down;
        }
        else
        {

            _player.SetMovementState(new PlayerIdleState(_commandSystem, _player));
            return;
        }

    }

    protected override void OnRightStart(Command command)
    {
        if (!_rightSideWall)
        {
            _player.SetMovementState(new PlayerFallingState(_commandSystem, _player));
        }
    }
    protected override void OnRightEnd(Command command)
    {
        if (_rightSideWall)
        {
            _player.SetMovementState(new PlayerFallingState(_commandSystem, _player));
        }
    }

    protected override void OnLeftStart(Command command)
    {
        if (_rightSideWall)
        {
            _player.SetMovementState(new PlayerFallingState(_commandSystem, _player));
        }
    }
    protected override void OnLeftEnd(Command command)
    {
        if (!_rightSideWall)
        {
            _player.SetMovementState(new PlayerFallingState(_commandSystem, _player));
        }
    }
    
    protected override void OnJumpStart(Command command)
    {
        base.OnJumpStart(command);
        if (_dashPressed)
        {
            _player.SetMovementState(new PlayerWallDashJumpingState(_commandSystem, _player));
            return;
        }
        _player.SetMovementState(new PlayerWallJumpingState(_commandSystem, _player));
    }
}
