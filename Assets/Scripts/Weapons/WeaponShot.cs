using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponShot : MonoBehaviour
{
    protected Equipable _weaponData;
    protected Vector3 _origin;
    protected Vector3 _direction;
    protected List<GameObject> _targetObjects;
    protected List<Vector3> _targetPositions;

    protected bool _triggered = false;

    public virtual void Init(Equipable weaponData, Vector3 origin, Vector3 direction, List<GameObject> targetObjects = null, List<Vector3> targetPostions = null)
    {
        _weaponData = weaponData;
        _origin = origin;
        _direction = direction;
        _targetObjects = targetObjects;
        _targetPositions = targetPostions;
    }
}
