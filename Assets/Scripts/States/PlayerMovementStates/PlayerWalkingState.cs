using UnityEngine;

public class PlayerWalkingState : PlayerMovementState
{
    public override PlayerMovementStateType Type { get; } = PlayerMovementStateType.Walking;

    public PlayerWalkingState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
    {
    }

    public override void Update(float dt)
    {
        base.Update(dt);

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

            _player.SetMovementState(new PlayerCoyoteTimeState(_commandSystem, _player));
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

    protected override void OnDashStart(Command command)
    {
        base.OnDashStart(command);
        _player.SetMovementState(new PlayerDashState(_commandSystem, _player));
    }
    
    protected override void OnRightEnd(Command command)
    {
        base.OnRightEnd(command);
        _player.SetMovementState(new PlayerIdleState(_commandSystem, _player));
    }

    protected override void OnLeftEnd(Command command)
    {
        base.OnLeftEnd(command);
        _player.SetMovementState(new PlayerIdleState(_commandSystem, _player));
    }


    protected override void OnJumpStart(Command command)
    {
        base.OnJumpStart(command);
        _player.SetMovementState(new PlayerJumpingState(_commandSystem, _player));
    }
}
