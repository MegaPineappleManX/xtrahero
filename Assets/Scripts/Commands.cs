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