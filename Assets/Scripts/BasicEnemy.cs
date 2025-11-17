using UnityEngine;

public class BasicEnemy : MonoBehaviour, IDamageable
{

    // IDamageable

    private int _currentHealth = 10;

    public int GetCurrentHealth() => _currentHealth;

    public DamagableObjectType GetDamagableObjectType() => DamagableObjectType.Enemy;

    public bool Hit(int damageValue)
    {
        _currentHealth -= damageValue;

        if (_currentHealth <= 0)
        {
            Kill();
        }

        return true;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}