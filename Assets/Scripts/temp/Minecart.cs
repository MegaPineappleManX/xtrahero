using UnityEngine;

public interface IRideable 
{
	public void Attach(GameObject passenger) { }
	public void Detach(GameObject passenger) { }
}

public interface IPilotable 
{
	public void Attach(GameObject pilot) { }
	public void Detach(GameObject pilot) { }
}


public class Minecart : MonoBehaviour, IRideable
{
	GameObject passenger = null;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void Attach(GameObject passenger) 
	{ 
		
	}
	
	public void Detach(GameObject passenger) 
	{ 
		passenger = null;
	}
}
