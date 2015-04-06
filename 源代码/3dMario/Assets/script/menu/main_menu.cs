using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class main_menu : MonoBehaviour {
	
	//states
	public const int state_mainMenu = 0;
	public const int state_startGame = 1;
	public const int state_option = 2;
	public const int state_help = 3;
	public const int state_exit = 4;
	
	//skin
	public GUISkin myskin;
	
	//windows
	public Rect windowRect0 = new Rect(20, 20, 150, 350);
	public Rect windowRect1 = new Rect(20, 20, 220, 350);
	
	//background
	public Texture background;
	
	//gamestate
	public int gameState;
	
	private Texture back;
	private Texture operation;
	private Texture music;
	private Texture sound;
	
	public AudioSource bgmusic;
	
	// Use this for initialization
	void Start()
	{
		gameState = state_mainMenu;
		
		back = (Texture)Resources.Load("Images/Back");
		operation = (Texture)Resources.Load("Images/operation");
		music = (Texture)Resources.Load("Images/music");
		sound = (Texture)Resources.Load("Images/sound");
		
		bgmusic.volume = Constants.musicVolume;
		bgmusic.Play();
	}

	void OnGUI(){
		
		switch(gameState)
		{
		case state_mainMenu:
			renderMainMenu();
			break;
		case state_startGame:
			renderStartGame();
			break;
		case state_option:
			renderOption();
			break;
		case state_help:
			renderHelp();
			break;
		case state_exit:
			Application.Quit();
			break;
		}
	}
	
	void renderMainMenu()
	{
		GUI.skin = myskin;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
		
		windowRect0 = GUI.Window(0, windowRect0, setWindow0, "");
		
	}
	
	void setWindow0(int winId)
	{
		//GUI.DragWindow();
		if (GUI.Button(new Rect(0, 30, 120, 50), "Start"))
		{
			gameState = state_startGame;
		}
		
		if (GUI.Button(new Rect(0, 90, 120, 50), "Settings"))
		{
			gameState = state_option;
		}
		
		if (GUI.Button(new Rect(0, 150, 120, 50), "Help"))
		{
			gameState = state_help;
		}
		
		if (GUI.Button(new Rect(0, 210, 120, 50), "Exit"))
		{
			gameState = state_exit;
		}
	}
	
	
	void renderStartGame()
	{
		Constants.curLevel = 1;
		Constants.life = 3;
		Constants.score = 0;
		Constants.coin = 0;
		Application.LoadLevel("stage");
	}
	
	void renderOption()
	{
		GUI.skin = myskin;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
		GUI.color = Color.black;
		GUI.Label(new Rect(30, 50, 300, 150), music);
		Constants.musicVolume = GUI.VerticalSlider(new Rect(80, 120, 30, 200), Constants.musicVolume, 1.0f, 0.0f);
		bgmusic.volume = Constants.musicVolume;
		GUI.Label(new Rect(Screen.width-170, 20, 400, 200), sound);
		Constants.soundVolume = GUI.VerticalSlider(new Rect(Screen.width-120, 120, 30, 200), Constants.soundVolume, 1.0f, 0.0f);
		
		if( GUI.Button(new Rect(Screen.width-150, Screen.height/2 + 100, 120, 50), back) )
		{
			gameState = state_mainMenu;
		}
	}
	
	void renderHelp()
	{
		GUI.skin = myskin;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), operation);
		
		if( GUI.Button(new Rect(Screen.width-150, Screen.height/2 + 100, 120, 50), back) )
		{
			gameState = state_mainMenu;
		}
	}
	
}
