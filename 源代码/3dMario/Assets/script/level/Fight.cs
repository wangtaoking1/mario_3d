using UnityEngine;
using System.Collections;

public class Fight : MonoBehaviour
{
	
	private CharacterMotor motor = null;
	private FPSInputController fps = null;
		
	public GameObject pre;
	
	// Use this for initialization
	void Start () 
	{
		motor = GetComponent<CharacterMotor>();
		fps = GetComponent<FPSInputController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( Input.GetKey(KeyCode.K) )
		{
			Vector3 littleJump = new Vector3(0, 2, 2);
			motor.SetVelocity(littleJump);
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		GameObject obj = hit.collider.gameObject;
		
		if( obj.name == "sanjiaoguai")
		{
			if ( !isUper(obj) )
			{
				GetComponent<Mario>().die();
			}
			else
			{
				double_jump();
				Destroy(obj);
				Constants.AddScore(10);
			}
		}
		else if (obj.name == "wugui")
		{
			if ( !isUper(obj) )
				GetComponent<Mario>().die();
			else
			{
				double_jump();
				Vector3 pos1 = obj.transform.position;
				pos1.y -= 0.2f;
				pos1.z -= 0.2f;
				Destroy(obj);
				GameObject inst = (GameObject) Instantiate(pre, pos1, pre.transform.rotation);
				Constants.AddScore(10);
			}
		}
		else if (obj.name == "suotouwugui" || obj.name == "suotouwugui(Clone)")
		{
			if ( !obj.GetComponent<Suotouwugui>().is_move )
			{
				if (transform.position.z < obj.transform.position.z)
				{
					obj.rigidbody.velocity = new Vector3(0, 0, 10);
					obj.GetComponent<Suotouwugui>().direction = 3;
				}
				else
				{
					obj.rigidbody.velocity = new Vector3(0, 0, -10);
					obj.GetComponent<Suotouwugui>().direction = -3;				
				}
				obj.GetComponent<Suotouwugui>().is_move = true;
				obj.GetComponent<Suotouwugui>().flag = true;
				Constants.AddScore(10);
				return ;
			}
			else if ( isUper(obj) )
			{
				double_jump();
				obj.GetComponent<Suotouwugui>().is_move = true;
				Constants.AddScore(10);
			}
			else
			{
				GetComponent<Mario>().die();
			}
		}
		else
		{
			motor.jumping.baseHeight = 3f;
			fps.ennable = true;
		}
	
	}
	
	public void double_jump()
	{
		motor.jumping.baseHeight = 0.3f;
		fps.ennable = false;
		motor.inputMoveDirection = new Vector3(0, 0, 0.2f);
		motor.inputJump = true;	
	}
	
	bool isUper(GameObject obj)
	{
		return transform.position.y - obj.transform.position.y > 0.5f;
	}
}
