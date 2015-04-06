using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
	
	void OnCollisionEnter(Collision collision)
	{
		transform.position = new Vector3(0, -2, 2 );
	}
	
	public void jumpOnce(Vector3 objpos)
	{
		transform.position = new Vector3(objpos.x, objpos.y+0.8f, objpos.z);
		rigidbody.velocity = new Vector3(0f, 5f, 0f);
	}
}
