using UnityEngine;

public class EntityMovingPlatformState : EntityPlatformState
{

	public override EntityPlatformStateType Type { get; } = EntityPlatformStateType.MovingPlatform;

	public EntityMovingPlatformState(CommandSystem commandSystem, Entity entity) : base(commandSystem, entity)
	{
	}

	public override void Update(float dt)
	{
		base.Update(dt);
	}

	protected override void OnJumpStart(Command command) { }
	protected override void OnJumpEnd(Command command) { }
	protected override void OnDashStart(Command command) { }
	protected override void OnDashHeld(Command command) { }
	protected override void OnDashEnd(Command command) { }
	protected override void OnRightStart(Command command) { }
	protected override void OnRightEnd(Command command) { }
	protected override void OnLeftStart(Command command) { }
	protected override void OnLeftEnd(Command command) { }
	protected override void OnInvalid(Command command) { } 
}