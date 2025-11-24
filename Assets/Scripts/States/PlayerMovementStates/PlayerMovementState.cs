using UnityEditor.ShaderGraph;
using UnityEngine;

public enum PlayerMovementStateType
{
    Idle,
    Walking,
    Dashing,
    Sliding,
    Jumping,
    DoubleJumping,
    Falling,
    CoyoteTime,
    DashCoyoteTime,
    DashJumping,
    AirDashing,
    WallSliding,
    WallJumping,
    Damaged,
    Spawning,
    Death,
    Invalid
}

public enum CurrentSpeedType
{
    Normal,
    Dash,
    Slow,
    Stunned,
    Rooted,
}

public enum CurrentPlatformType
{
	Normal,
	Moving,
	Ice,
}

public abstract class PlayerMovementState : EntityState
{
    protected Player _player;
    protected bool _rightPressed;
    protected bool _leftPressed;
    protected bool _walled;
    protected bool _dashPressed;

    public virtual PlayerMovementStateType Type { get; } = PlayerMovementStateType.Invalid;
	public virtual CurrentSpeedType SpeedType { get; } = CurrentSpeedType.Normal;
	public virtual CurrentPlatformType PlatformType { get; } = CurrentPlatformType.Normal;

    public PlayerMovementState(CommandSystem commandSystem, Player player) : base(commandSystem)
    {
        _player = player;
    }

    public override void Update(float dt)
    {
        base.Update(dt);

        var currentSpeed = SpeedType switch
        {
            CurrentSpeedType.Normal => _player.WalkSpeed,
            CurrentSpeedType.Dash => _player.DashSpeed,
            CurrentSpeedType.Slow => throw new System.NotImplementedException(),
            CurrentSpeedType.Stunned => throw new System.NotImplementedException(),
            CurrentSpeedType.Rooted => throw new System.NotImplementedException(),
            _ => throw new System.NotImplementedException()
        };

        if (_rightPressed)
        {
            var hitPoint = GetClosestRightPoint().point;
            var tempPos = _player.transform.position + currentSpeed * dt * Vector3.right;
            var halfColliderWidth = _player.ColliderWidth / 2;
            Debug.DrawLine(hitPoint, tempPos);

            if (tempPos.x <= hitPoint.x - halfColliderWidth)
            {
                _player.transform.position = tempPos;
                _walled = false;
                return;
            }
            _player.transform.position = new Vector3(hitPoint.x - halfColliderWidth, _player.transform.position.y, _player.transform.position.z);
            _walled = true;
        }

        if (_leftPressed)
        {
            var hitPoint = GetClosestLeftPoint().point;
            var tempPos = _player.transform.position + currentSpeed * dt * Vector3.left;
            var halfColliderWidth = _player.ColliderWidth / 2;
            Debug.DrawLine(hitPoint, tempPos);

            if (tempPos.x >= hitPoint.x + halfColliderWidth)
            {
                _player.transform.position = tempPos;
                _walled = false;
                return;
            }
            _player.transform.position = new Vector3(hitPoint.x + halfColliderWidth, _player.transform.position.y, _player.transform.position.z);
            _walled = true;
        }
    }

    // A lot of this could be streamlined but I want it as easy to read as possible for now.
    // I'm not 100% sure how I want my character controller to work and I work on this project so intermitently that I want simple here.
    protected virtual RaycastHit GetClosestGroundPoint()
    {
        var rayOrigin = _player.transform.position;

        var hit = new RaycastHit();
        hit.point = Vector3.down * int.MaxValue;

        // check center first
        if (Physics.Raycast(rayOrigin, Vector3.down, out var hitInfo, _player.ColliderHeight, _player.GroundLayerMask))
        {
            hit = hitInfo;

            // If we're on a slope only return the middle point
            if (Vector3.Angle(Vector3.up, hitInfo.normal) > 0 && hit.point.y + _player.ColliderHeight > _player.transform.position.y)
            {
                Debug.DrawRay(rayOrigin, Vector3.down, Color.green);
                return hit;
            }
            else
            {
                Debug.DrawRay(rayOrigin, Vector3.down, Color.red);
            }
        }

        var leftPoint = Vector3.left * _player.ColliderWidth / 2;
        var leftHits = Physics.RaycastAll(rayOrigin + leftPoint, Vector3.down, _player.ColliderHeight, _player.GroundLayerMask);
    
        foreach (var leftHit in leftHits)
        {
            if (hit.point != Vector3.down * int.MaxValue || Vector3.Angle(Vector3.up, leftHit.normal) == 0)
            {
                //Debug.DrawRay(rayOrigin + leftPoint, Vector3.down, Color.green);
                //hit = hit.point.y > leftHit.point.y ? hit : leftHit;
            }
            else
            {
                //Debug.DrawRay(rayOrigin, Vector3.down, Color.red);
            }
        }
        if (Physics.Raycast(rayOrigin + leftPoint, Vector3.down, out hitInfo, _player.ColliderHeight, _player.GroundLayerMask))
        {
            // ignore slopes
            if (Vector3.Angle(Vector3.up, hitInfo.normal) == 90 && Vector3.Angle(Vector3.up, hit.normal) == 0)
            {
                Debug.DrawRay(rayOrigin + leftPoint, Vector3.down, Color.green);
                hit = hit.point.y > hitInfo.point.y ? hit : hitInfo;
            }
            else
            {
                Debug.DrawRay(rayOrigin, Vector3.down, Color.red);
            }
        }

        var rightPoint = Vector3.right * _player.ColliderWidth / 2;
        if (Physics.Raycast(rayOrigin + rightPoint, Vector3.down, out hitInfo, _player.ColliderHeight / 2, _player.GroundLayerMask))
        {
            // ignore slopes
            if (Vector3.Angle(Vector3.up, hitInfo.normal) == 90 && Vector3.Angle(Vector3.up, hit.normal) == 0)
            {
                Debug.DrawRay(rayOrigin + rightPoint, Vector3.down, Color.green);
                hit = hit.point.y > hitInfo.point.y ? hit : hitInfo;
            }
            else
            {
                Debug.DrawRay(rayOrigin, Vector3.down, Color.red);
            }
        }

        return hit;
    }

    protected virtual RaycastHit GetClosestCeilingPoint()
    {
        var rayOrigin = _player.transform.position;
        var hit = new RaycastHit();
        hit.point = Vector3.up * int.MaxValue;

        // check center first
        if (Physics.Raycast(rayOrigin, Vector3.up, out var hitInfo, Mathf.Infinity, _player.GroundLayerMask))
        {
            hit = hitInfo;
        }

        var leftPoint = Vector3.left * _player.ColliderWidth / 2;
        if (Physics.Raycast(rayOrigin + leftPoint, Vector3.up, out hitInfo, Mathf.Infinity, _player.GroundLayerMask))
        {
            if (Vector3.Angle(Vector3.down, hitInfo.normal) == 0)
            {

                hit = hitInfo;
            }
        }

        var rightPoint = Vector3.right * _player.ColliderWidth / 2;
        if (Physics.Raycast(rayOrigin + rightPoint, Vector3.up, out hitInfo, Mathf.Infinity, _player.GroundLayerMask))
        {
            if (Vector3.Angle(Vector3.down, hitInfo.normal) == 0)
            {
                hit = hitInfo;
            }
        }

        return hit;
    }

    protected virtual RaycastHit GetClosestLeftPoint()
    {
        var rayOrigin = _player.transform.position;
        var hit = new RaycastHit();
        hit.point = Vector3.left * int.MaxValue;

        // check center first
        if (Physics.Raycast(rayOrigin, Vector3.left, out var hitInfo, Mathf.Infinity, _player.GroundLayerMask))
        {
            hit = hitInfo;
        }

        var upPoint = Vector3.up * _player.ColliderHeight / 2;
        if (Physics.Raycast(rayOrigin + upPoint, Vector3.left, out hitInfo, Mathf.Infinity, _player.GroundLayerMask))
        {
            if (Vector3.Angle(Vector3.right, hitInfo.normal) == 0 && hit.point.x < hitInfo.point.x)
            {
                hit = hitInfo;
            }
        }

        var downPoint = Vector3.down * _player.ColliderHeight / 3;
        if (Physics.Raycast(rayOrigin + downPoint, Vector3.left, out hitInfo, Mathf.Infinity, _player.GroundLayerMask))
        {
            if (Vector3.Angle(Vector3.right, hitInfo.normal) == 0 && hit.point.x < hitInfo.point.x)
            {
                hit = hitInfo;
            }
        }

        return hit;
    }

    protected virtual RaycastHit GetClosestRightPoint()
    {
        var rayOrigin = _player.transform.position;
        var hit = new RaycastHit();
        hit.point = Vector3.right * int.MaxValue;

        // check center first
        if (Physics.Raycast(rayOrigin, Vector3.right, out var hitInfo, Mathf.Infinity, _player.GroundLayerMask))
        {
            hit = hitInfo;
        }

        var upPoint = Vector3.up * _player.ColliderHeight / 2;
        if (Physics.Raycast(rayOrigin + upPoint, Vector3.right, out hitInfo, Mathf.Infinity, _player.GroundLayerMask))
        {
            if (Vector3.Angle(Vector3.left, hitInfo.normal) == 0 && hit.point.x > hitInfo.point.x)
            {
                hit = hitInfo;
            }
        }

        var downPoint = Vector3.down * _player.ColliderHeight / 3;
        if (Physics.Raycast(rayOrigin + downPoint, Vector3.right, out hitInfo, Mathf.Infinity, _player.GroundLayerMask))
        {
            if (Vector3.Angle(Vector3.left, hitInfo.normal) == 0 && hit.point.x > hitInfo.point.x)
            {
                hit = hitInfo;
            }
        }

        return hit;
    }

    protected override void ProcessCommand(Command command)
    {
        switch (command.Type)
        {
            case CommandType.JumpStart:
                OnJumpStart(command);
                break;
            case CommandType.JumpEnd:
                OnJumpEnd(command);
                break;
            case CommandType.RightStart:
                OnRightStart(command);
                break;
            case CommandType.RightEnd:
                OnRightEnd(command);
                break;
            case CommandType.LeftStart:
                OnLeftStart(command);
                break;
            case CommandType.LeftEnd:
                OnLeftEnd(command);
                break;
            case CommandType.DashStart:
                OnDashStart(command);
                break;
            case CommandType.DashHeld:
                OnDashHeld(command);
                break;
            case CommandType.DashEnd:
                OnDashEnd(command);
                break;
            case CommandType.Invalid:
            default:
                OnInvalid(command);
                break;
        }
    }


    protected virtual void OnJumpStart(Command command) { }
    protected virtual void OnJumpEnd(Command command) { }
    protected virtual void OnDashStart(Command command) { }
    
    protected virtual void OnDashHeld(Command command)
    {
        _dashPressed = true;
    }
    
    protected virtual void OnDashEnd(Command command)
    {
        _dashPressed = false;
    }

    protected virtual void OnRightStart(Command command)
    {
        _leftPressed = false;
        _rightPressed = true;
        if (SpeedType != CurrentSpeedType.Stunned)
        {
            _player.SetFacing(true);
        }
    }

    protected virtual void OnRightEnd(Command command)
    {
        _rightPressed = false;
    }

    protected virtual void OnLeftStart(Command command)
    {
        _rightPressed = false;
        _leftPressed = true;
        if (SpeedType != CurrentSpeedType.Stunned)
        {
            _player.SetFacing(false);
        }
    }

    protected virtual void OnLeftEnd(Command command)
    {
        _leftPressed = false;
    }

    protected virtual void OnInvalid(Command command)
    {
    }
}