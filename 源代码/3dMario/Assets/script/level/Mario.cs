using UnityEngine;
using System.Collections;

public class Mario : MonoBehaviour
{
	
	private int time =0;
	// Use this for initialization
	private int pretime = 0;
	private int numOfMushroom = 0;
	private int totalTime = 180;
	
	private GameObject coin;
	public GameObject Mushroom;
	public GameObject pre_cube;
	
	public Texture mushroom_tex;
	public Texture time_tex;
	public Texture cross_tex;
	public Texture coin_tex;
	private Object[] numbers;
	
	public bool hasEnded = false;
	private CharacterMotor motor;
	private int flagScore = 0;
	
	public AudioSource flagsound;
	public AudioSource jumpsound;
	public AudioSource coinsound;
	public AudioSource diesound;
	public AudioSource cracksound;
	
	void Start ()
	{
		coin = GameObject.Find("Coin");
		numbers = Resources.LoadAll("Numbers");
		motor = GetComponent<CharacterMotor>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( Input.GetKeyDown(KeyCode.E) )
		{
			transform.Rotate(new Vector3(0, 180, 0) );
		}
		
		
		if( hasEnded && motor.grounded )
		{
			Vector3 pos = transform.position;
			if( pos.z < 178.5f )
			{
				Vector3 curpos = transform.position;
				transform.position = new Vector3(curpos.x, curpos.y, curpos.z + 0.2f);
			}
			else
			{
				
				motor.inputMoveDirection = new Vector3(0, 0, 1f);
				if( flagsound.isPlaying)
					flagsound.Stop();
			}
		}
		
		checkDead();
	}
	
	void FixedUpdate()
	{
		int cur = Mathf.FloorToInt(Time.time);
		if( cur != pretime )
		{
			pretime = cur;
			time++;
		}
		
		Vector3 pos = transform.position;
		transform.position = new Vector3(0, pos.y, pos.z);
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(5, 5, 40, 40), mushroom_tex);
		GUI.DrawTexture(new Rect(50, 9, 30, 30), cross_tex);
		drawNumber(70, 5, 40, numOfMushroom);
		
		GUI.DrawTexture(new Rect(5, 80, 40, 40), coin_tex);
		GUI.DrawTexture(new Rect(50, 84, 30, 30), cross_tex);
		drawNumber(70, 80, 40, Constants.coin);
		
		GUI.DrawTexture(new Rect(5, 160, 40, 40), time_tex);
		GUI.DrawTexture(new Rect(50, 164, 30, 30), cross_tex);
		drawNumber(70, 160, 40, totalTime - time);
		
		
		if( hasEnded )
		{
			drawNumber(Screen.width/2-100, Screen.height/2, 50, flagScore);
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		GameObject obj = hit.collider.gameObject;
		if( obj.name == "brick" && obj.GetComponent<Brick>().count > 0 && isUnder(obj))
		{
			obj.GetComponent<Brick>().JumpupOnce();
			
			Vector3 objpos = obj.transform.position;
			coin.GetComponent<Coin>().jumpOnce(objpos);
			if( coinsound.isPlaying )
				coinsound.Stop();
			coinsound.Play();
			obj.GetComponent<Brick>().count --;
			
			Constants.AddCoin();
			Constants.AddScore(10);
		}
		if( obj.name == "cube_mushroom" && obj.GetComponent<Brick>().count==1 && isUnder(obj))
		{
			obj.GetComponent<Brick>().JumpupOnce();
			
			//GameObject mushroom = GameObject.Find("Mushroom");
			Vector3 objPos = obj.transform.position;
			GameObject mushroom = (GameObject) Instantiate(Mushroom, new Vector3(objPos.x, objPos.y+1f, objPos.z), Mushroom.transform.rotation);
			
			mushroom.rigidbody.velocity = new Vector3(0f, 1.5f, 0f);
			obj.GetComponent<Brick>().count--;
			
			Constants.AddScore(5);
		}
		if( obj.name =="cube" && obj.transform.position.y > transform.position.y)
		{
			if( cracksound.isPlaying )
				cracksound.Stop();
			cracksound.Play();
			GameObject instance = (GameObject) Instantiate(pre_cube, obj.transform.position, pre_cube.transform.rotation);
			Destroy(obj);
			
			Vector3 pos = instance.transform.position;
			Collider[] colliders = Physics.OverlapSphere(pos, 1f);
			
			foreach(Collider col in colliders)
			{	
				if (col.rigidbody && col.gameObject.name == "small")
				{
					col.rigidbody.AddExplosionForce(800f, pos, 1f, 3f);
					Destroy(col.gameObject, 3);
					//col.transform.gameObject.AddComponent(Application.dataPath + "/script/level/small");
				}
			}
			Constants.AddScore(10);
		}
		if (obj.name == "Mushroom1(Clone)")
		{
			eatAMushroom();
			obj.GetComponent<Mushroom>().beEaten();
		}
		if( obj.name == "Cylinder" && !hasEnded)
		{
			hasEnded = true;
			GameObject.Find("Plane").GetComponent<Flag>().settleDown = true;
			flagScore = Mathf.FloorToInt(transform.position.y) * 100;
			Constants.AddScore(flagScore);
			GameObject map = GameObject.Find("Camera");
			if( map.GetComponent<PrettyMap>().bgmusic.isPlaying )
			{
				map.GetComponent<PrettyMap>().bgmusic.Stop();
				flagsound.Play();
			}
		}
		if( obj.name == "End_tower" )
		{
			Constants.curLevel++;
			Application.LoadLevel(0);
		}
	}
	
	
	private void checkDead()
	{
		Vector3 pos = transform.position;
		if( pos.y < -1 && !diesound.isPlaying )
		{
			diesound.Play();
			GameObject.Find("Camera").GetComponent<PrettyMap>().bgmusic.Stop();
		}
		if( pos.y < -25 || time ==totalTime )
		{
			if( numOfMushroom > 0 )
			{
				numOfMushroom--;
				Vector3 prepos = transform.position;
				if( prepos.z < 63 )
					transform.position = new Vector3(0, 2, 0);
				else
					transform.position = new Vector3(0, 2, 63);
				time = 0;
				return;
			}
			//transform.position = new Vector3(0, 2, 1);
			if( Constants.life >0 )
			{
				Constants.life--;
				Application.LoadLevel( 1 );
			}
			else
				Application.LoadLevel(0);
		}
	}
	
	public void die()
	{
		if (!diesound.isPlaying)
		{
			diesound.Play();
			GameObject.Find("Camera").GetComponent<PrettyMap>().bgmusic.Stop();
		}
		Invoke("load", 2f);
	}
	
	public void load()
	{
		Debug.Log("1");
		if( Constants.life >0 )
		{
			Constants.life--;
			Application.LoadLevel(1);
		}
		else
			Application.LoadLevel(0);	
	}
	
	public void eatAMushroom()
	{
		numOfMushroom++;
		Constants.AddScore(50);
	}
	
	
	private bool isUnder( GameObject obj)
	{
		if( obj.transform.position.y >= (transform.position.y + 1.18 ) )
			return true;
		else
			return false;
	}
	
	private void drawNumber(int x, int y, int size, int number)
	{
		char[] chars = number.ToString().ToCharArray();
		foreach( char s in chars)
		{
			int i = int.Parse(s.ToString());
			GUI.DrawTexture(new Rect(x, y, size, size), (Texture)numbers[i]);
			x += size/2;
		}
	}
}
