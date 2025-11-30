using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq; 

[CreateAssetMenu(fileName = "Equipable", menuName = "Scriptable Objects/Equipable")]
public class Equipable : SerializedScriptableObject
{
    public List<Part> Parts;

    public T GetPart<T>() where T : Part
    {
        return Parts.OfType<T>().FirstOrDefault();
    }
}
