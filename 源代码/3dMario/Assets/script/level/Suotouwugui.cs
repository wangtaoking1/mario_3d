using UnityEngine;
using System.Collections;

public class Suotouwugui : MonoBehaviour {

	public float moveSpeed = 3f;
	public int direction = -1;
	public bool is_move = false;
	private float curZ = 0;
	
	public bool flag =false;
	
	// Use this for initialization
	void Start () {
		rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
		is_move = false;
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		if(is_move)
			rigidbody.velocity = new Vector3(0, 0, moveSpeed * direction);
		else
			rigidbody.velocity = Vector3.zero;
		/*
		if( is_move && Mathf.Abs(transform.position.z - curZ) < 0.001 )
			direction = -direction;
		
		if (is_move && rigidbody.velocity.y > 0)
			rigidbody.velocity = new Vector3(0, 0, moveSpeed * direction);
		else if (is_move)
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, moveSpeed * direction);
		else
			rigidbody.velocity = Vector3.zero;
		*/
		curZ = transform.position.z;
		disappear();
	}
	
	void OnCollisionEnter(Collision collision) 
	{
		GameObject obj = collision.gameObject;
		Debug.Log(obj.name);
		
		if ( obj.name == "barrel" || obj.name == "Begin_tower" || obj.name == "End_tower"
			|| obj.name == "suotouwugui(Clone)" || obj.name == "suotouwugui" || obj.name == "Mushroom1(Clone)" || obj.name == "step_cube" )
		{
			direction = -direction;		
		}
		else if( obj.rigidbody && middle(obj.transform.position) && is_move)
		{
			if (obj.name == "wugui" || obj.name == "sanjiaoguai" )
				Destroy(obj);
			else if (obj.name == "cube")
				direction = -direction;	
			
		}
		foreach (ContactPoint contact in collision.contacts) 
		{
            if( contact.otherCollider.name == "Mario" && is_move && !flag )
			{
				contact.otherCollider.gameObject.GetComponent<Mario>().die();
				break;
			}
        }
		flag = false;
    }
	
	public bool middle(Vector3 pos)
	{
		return Mathf.Abs(pos.y - transform.position.y) < 0.2;
	}
	
	public void disappear()
	{
		if (transform.position.y < 0.5 || transform.position.x >= 1 ||transform.position.x <= -1 )
		{
			Destroy(gameObject);
		}
	}
	public void die()
	{
		Destroy(gameObject);
	}
}
