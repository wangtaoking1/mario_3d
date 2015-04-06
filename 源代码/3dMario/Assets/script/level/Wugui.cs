using UnityEngine;
using System.Collections;

public class Wugui : MonoBehaviour {

	public float moveSpeed = 1.5f;
	public int direction = -1;
	private float curZ = 0;
	
	// Use this for initialization
	void Start () {
		rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{	
		rigidbody.velocity = new Vector3(0, 0, moveSpeed * direction);
		/*
		if( Mathf.Abs(transform.position.z - curZ) < 0.001 )
			direction = -direction;
		
		if (rigidbody.velocity.y > 0)
			rigidbody.velocity = new Vector3(0, 0, moveSpeed * direction);
		else
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, moveSpeed * direction);
		//transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * direction);
		 */
		curZ = transform.position.z;
		disappear();
	}
	
	void OnCollisionEnter(Collision collision) 
	{
		GameObject obj = collision.gameObject;
		
		if ( obj.name == "barrel" || obj.name == "Begin_tower" || obj.name == "End_tower" || obj.name == "Mushroom1(Clone)" 
			|| obj.name == "step_cube" )
		{
			transform.Rotate(new Vector3(0, 180, 0));
			direction = -direction;		
		}
		else if( obj.rigidbody )
		{
			
			if ( (obj.name == "wugui" || obj.name == "suotouwugui" || obj.name == "sanjiaoguai" || obj.name == "suotouwugui(Clone)" || obj.name == "cube" )&& middle(obj.transform.position))
			{
				transform.Rotate(new Vector3(0, 180, 0));
				direction = -direction;
			}
		}
		
		foreach (ContactPoint contact in collision.contacts) 
		{
            if( contact.otherCollider.name == "Mario")
			{
				contact.otherCollider.gameObject.GetComponent<Mario>().die();
			}
        } 
    }
	
	public bool middle(Vector3 pos)
	{
		return Mathf.Abs(pos.y - transform.position.y) < 0.2;
	}
	
	public void disappear()
	{
		if (transform.position.y < 0 || transform.position.x >= 1 ||transform.position.x <= -1 )
		{
			Debug.Log(transform.position.y + " " + transform.position.x);
			Destroy(transform.gameObject);
		}
	}
	public void die()
	{
		Destroy(gameObject);
	}
}
