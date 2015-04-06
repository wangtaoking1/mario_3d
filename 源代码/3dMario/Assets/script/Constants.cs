using UnityEngine;
using System.Collections;

public static class Constants
{
	public static int life = 3;
	public static int coin = 0;
	public static int curLevel = 1;
	public static int score = 0;

	public static bool musicEnable = true;
	public static bool soundEnable = true;
	public static float musicVolume = 0.5f;
	public static float soundVolume = 0.5f;
	
	public static void AddCoin()
	{
		coin++;
		if( coin == 100 )
		{
			life++;
			coin =0;
		}
	}
	
	public static void AddScore(int sco)
	{
		score += sco;
	}
}
