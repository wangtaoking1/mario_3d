using UnityEngine;
using System.Collections;

public class Mushroom : MonoBehaviour {
	
	public float moveSpeed = 2f;
	private int direction = 1;
	private float curZ = 0;
	
	// Use this for initialization
	void Start () {
		rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		if( Mathf.Abs(transform.position.z - curZ) < 0.001 )
			direction = -direction;
		
		if (rigidbody.velocity.y > 0)
			rigidbody.velocity = new Vector3(0, 0, moveSpeed * direction);
		else
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, moveSpeed * direction);
		//transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * direction);

		curZ = transform.position.z;
		
		disappear();
	}
	
	void OnCollisionEnter(Collision collision) 
	{
		GameObject obj = collision.gameObject;
		
		if (obj.name == "Begin_tower" || obj.name == "End_tower" || obj.name == "barrel")
		{
			direction = -direction;		
		}
		else if ( (obj.name == "wugui" || obj.name == "suotouwugui" || obj.name == "sanjiaoguai" || obj.name == "suotouwugui(Clone)")&& middle(obj.transform.position))
		{
			direction = -direction;
		}
		
        foreach (ContactPoint contact in collision.contacts) 
		{
            if( contact.otherCollider.name == "Mario" )
			{
				contact.otherCollider.gameObject.GetComponent<Mario>().eatAMushroom();
				beEaten();
				break;
			}
        }   
    }
	
	public void beEaten()
	{
		Destroy(gameObject);
	}
	
	public bool middle(Vector3 pos)
	{
		return Mathf.Abs(pos.y - transform.position.y) < 0.2;
	}
	
	public void disappear()
	{
		if (transform.position.y < 0 || transform.position.x > 1 ||transform.position.x < 0 )
		{
			Destroy(transform.gameObject);
		}
	}
}
