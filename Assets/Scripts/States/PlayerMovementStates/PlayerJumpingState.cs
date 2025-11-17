using UnityEngine;

public class PlayerJumpingState : PlayerMovementState
{
    public override PlayerMovementStateType Type { get; } = PlayerMovementStateType.Jumping;
    private float _currentJumpForce;

    public PlayerJumpingState(CommandSystem commandSystem, Player player) : base(commandSystem, player)
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
            _player.SetMovementState(new PlayerFallingState(_commandSystem, _player));
            return;
        }
        _currentJumpForce -= dt * _currentJumpForce * 10;

        var hitPoint = GetClosestCeilingPoint().point;
        var tempPos = _player.transform.position + _currentJumpForce * dt * Vector3.up;
        var halfColliderHeight = _player.ColliderHeight / 2;
        if (hitPoint.y <= tempPos.y + halfColliderHeight) {
            tempPos.y = hitPoint.y - halfColliderHeight;
            _player.transform.position = tempPos;
            _player.SetMovementState(new PlayerFallingState(_commandSystem, _player));
            return;
        }

        _player.transform.position = tempPos;
    }

    protected override void OnJumpEnd(Command command)
    {
        base.OnJumpEnd(command);
        _player.SetMovementState(new PlayerFallingState(_commandSystem, _player));
    }
}