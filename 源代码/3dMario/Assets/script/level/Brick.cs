using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour 
{
	public int count = 1;
	public Texture aft_tex;
	
	private Vector3 pos;
	
	// Use this for initialization
	void Start ()
	{
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 curpos = rigidbody.position;
		if( curpos.y < pos.y )
		{
			rigidbody.position = pos;
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}
		
		if( count == 0 )
		{
			renderer.material.mainTexture = aft_tex;
		}
	}
	
	public void JumpupOnce()
	{
		rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
		rigidbody.velocity = new Vector3(0f, 1.5f, 0f);
	}
}
