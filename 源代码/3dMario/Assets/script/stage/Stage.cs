using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour
{
	public Texture background;
	private float startTime = 0;
	private Texture mario;
	private Texture score_tex;
	private Texture stage_tex;
	private Texture cross_tex;
	private Object[] numbers;
	// Use this for initialization
	void Start ()
	{
		startTime = Time.time;
		mario = (Texture)Resources.Load("Images/Mario");
		score_tex = (Texture)Resources.Load("Images/score");
		stage_tex = (Texture)Resources.Load("Images/stage");
		cross_tex = (Texture)Resources.Load("Images/cross");
		numbers  = Resources.LoadAll("Numbers");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( (Time.time - startTime) > 2.5f )
			Application.LoadLevel(Constants.curLevel+1);
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
		
		GUI.DrawTexture(new Rect(Screen.width/2-120, Screen.height/2 - 200, 100, 100), score_tex);
		GUI.DrawTexture(new Rect(Screen.width/2-30, Screen.height/2 - 175, 50, 50), cross_tex);
		drawNumber(Screen.width/2, Screen.height/2-185, 60, Constants.score);
		
		GUI.DrawTexture(new Rect(Screen.width/2-100, Screen.height/2-120, 100, 100), stage_tex);
		drawNumber(Screen.width/2, Screen.height/2-100, 60, Constants.curLevel);
		
		GUI.DrawTexture(new Rect(Screen.width/2-100, Screen.height/2-30, 60, 50), mario);
		GUI.DrawTexture(new Rect(Screen.width/2-35, Screen.height/2-30, 50, 50), cross_tex);
		drawNumber(Screen.width/2, Screen.height/2-35, 60, Constants.life);
	}
	
	private void drawNumber(float x, float y, float size, int number)
	{
		char[] chars = number.ToString().ToCharArray();
		foreach( char s in chars)
		{
			int i = int.Parse(s.ToString());
			GUI.DrawTexture(new Rect(x, y, size, size), (Texture)numbers[i]);
			x += size/1.5f;
		}
	}
}
