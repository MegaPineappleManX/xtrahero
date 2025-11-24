using UnityEngine;

public class ReflectableRocket : MonoBehaviour, IReflectable
{
	public Vector3 _direction;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
	{
		_direction.y -= Time.deltaTime / 4;
    	
	    transform.position += _direction * Time.deltaTime;
    }

	public void Reflect(Vector3 reflectorPosition)
    {
        var dir = transform.position - reflectorPosition;

        Debug.Log(dir);
        
        _direction = dir;//new Vector3(_direction.x * dir.x, _direction.y * dir.y, _direction.z * dir.z);
    }
}
