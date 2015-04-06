using UnityEngine;
using System.Collections;

public class PrettyMap : MonoBehaviour
{
	public GameObject target;
	public AudioSource bgmusic;
	public AudioSource flagmusic;
	float ahead = 2;

	void Start ()
	{
		if( Constants.musicEnable && !bgmusic.isPlaying)
			bgmusic.Play();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 pos = transform.position;
		Vector3 other = target.transform.position;
		transform.position = new Vector3( pos.x, pos.y, other.z + ahead);
		if( other.z > 179f && Constants.musicEnable && !flagmusic.isPlaying)
		{
			bgmusic.Stop();
			flagmusic.Play();
		}
	}
}
