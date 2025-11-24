using UnityEditor.ShaderGraph;
using UnityEngine;

public enum EntityPlatformStateType
{
	Default,
	MovingPlatform,
	Ice,
	Invalid
}

public abstract class EntityPlatformState : EntityState
{
	protected Entity _entity;

	public virtual EntityPlatformStateType Type { get; } = EntityPlatformStateType.Invalid;

	public EntityPlatformState(CommandSystem commandSystem, Entity entity) : base(commandSystem)
	{
		_entity = entity;
	}

	public override void Update(float dt)
	{
		base.Update(dt);
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
	protected virtual void OnDashHeld(Command command) { }
	protected virtual void OnDashEnd(Command command) { }
	protected virtual void OnRightStart(Command command) { }
	protected virtual void OnRightEnd(Command command) { }
	protected virtual void OnLeftStart(Command command) { }
	protected virtual void OnLeftEnd(Command command) { }
	protected virtual void OnInvalid(Command command) { } 
}