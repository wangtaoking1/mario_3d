using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour {
	
	public bool settleDown = false;
	//public float speed = 0.0002f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( settleDown )
		{
			//transform.Translate(new Vector3(0, -speed , 0)* Time.deltaTime); 
			Vector3 curpos = transform.position;
			transform.position = new Vector3(curpos.x, curpos.y*98/100, curpos.z);
		}
	}
	
	void OnGUI()
	{
	}
}
