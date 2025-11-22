public enum CommandType
{
    JumpStart,
    JumpEnd,
    LeftStart,
    LeftEnd,
    RightStart,
    RightEnd,
    DashStart,
    DashHeld,
    DashEnd,
    ShootStart,
	ShootEnd,
	WeaponChangeLeft,
	WeaponChangeRight,
	WeaponChange_01,
	WeaponChange_02,
	WeaponChange_03,
	WeaponChange_04,
	WeaponChange_05,
	WeaponChange_06,
	WeaponChange_07,
	WeaponChange_08,
    Invalid
}

public abstract class Command
{
    public virtual CommandType Type { get; } = CommandType.Invalid;
}
public class JumpPressedCommand : Command
{
    public override CommandType Type { get; } = CommandType.JumpStart;
}
public class JumpReleasedCommand : Command
{
    public override CommandType Type { get; } = CommandType.JumpEnd;
}
public class LeftPressedCommand : Command
{
    public override CommandType Type { get; } = CommandType.LeftStart;
}
public class RightPressedCommand : Command
{
    public override CommandType Type { get; } = CommandType.RightStart;
}
public class LeftReleasedCommand : Command
{
    public override CommandType Type { get; } = CommandType.LeftEnd;
}
public class RightReleasedCommand : Command
{
    public override CommandType Type { get; } = CommandType.RightEnd;
}
public class DashPressedCommand : Command
{
    public override CommandType Type { get; } = CommandType.DashStart;
}
public class DashHeldCommand : Command
{
    public override CommandType Type { get; } = CommandType.DashHeld;
}
public class DashReleasedCommand : Command
{
    public override CommandType Type { get; } = CommandType.DashEnd;
}
public class ShootStartCommand : Command
{
    public override CommandType Type { get; } = CommandType.ShootStart;
}
public class ShootEndCommand : Command
{
	public override CommandType Type { get; } = CommandType.ShootEnd;
}
public class WeaponChangeLeftCommand : Command
{
	public override CommandType Type { get; } = CommandType.WeaponChangeLeft;
}
public class WeaponChangeRightCommand : Command
{
	public override CommandType Type { get; } = CommandType.WeaponChangeRight;
}
public class WeaponChange01Command : Command
{
	public override CommandType Type { get; } = CommandType.WeaponChange_01;
}
public class WeaponChange02Command : Command
{
	public override CommandType Type { get; } = CommandType.WeaponChange_02;
}
public class WeaponChange03Command : Command
{
	public override CommandType Type { get; } = CommandType.WeaponChange_03;
}
public class WeaponChange04Command : Command
{
	public override CommandType Type { get; } = CommandType.WeaponChange_04;
}
public class WeaponChange05Command : Command
{
	public override CommandType Type { get; } = CommandType.WeaponChange_05;
}
public class WeaponChange06Command : Command
{
	public override CommandType Type { get; } = CommandType.WeaponChange_06;
}
public class WeaponChange07Command : Command
{
	public override CommandType Type { get; } = CommandType.WeaponChange_07;
}
public class WeaponChange08Command : Command
{
	public override CommandType Type { get; } = CommandType.WeaponChange_08;
}