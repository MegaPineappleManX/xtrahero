
public enum DamagableObjectType
{
    Player,
    Enemy,
    Neutral
}

public interface IDamageable
{
    public abstract int GetCurrentHealth();
    public abstract DamagableObjectType GetDamagableObjectType();
    public abstract bool Hit(int damageValue);
    public abstract void Kill();
}
