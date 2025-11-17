using UnityEngine;

public class PlayerFallingState : PlayerMovementState
{
    public override PlayerMovementStateType Type { get; } = PlayerMovementStateType.Falling;
    private float _maxGravityForce = 20;
    private float _gravityForce = 0;

    public PlayerFallingState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
    {
    }

    public override void Update(float dt)
    {
        base.Update(dt);

        var groundPoint = GetClosestGroundPoint().point;
        if (groundPoint.y < _player.transform.position.y - _player.ColliderHeight / 2)
        {

            _gravityForce += dt * _maxGravityForce * 5f;

            if (_gravityForce >= _maxGravityForce)
            {
                _gravityForce = _maxGravityForce;
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
}
