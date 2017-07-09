using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour {
	public static float Timer;
	public static int Score;

	public ScoreBoard[] boards;
	public int[] paliers;
	public GameObject Spawn;

	GameObject _player;
	bool _end;
	
	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () 
	{
		if(!_end)
		{
			Timer -= Time.deltaTime;
			if(Timer <= 0)
			{
				Timer = 0;
				_end = true;
				StartCoroutine(EndSequence());
			}
		}
	}

	IEnumerator EndSequence(){
		SteamVR_Fade.View(new Color(0,0,0,1), 2.0f);
		yield return new WaitForSeconds(2.0f);
		SteamVR_Fade.View(new Color(0,0,0,0), 2.0f);
		_player.transform.position = Spawn.transform.position;
		yield return new WaitForSeconds(5.0f);
		for(int i = 0; i < boards.Length; i++)
		{
			if(Score > paliers[i]){
				boards[i].Ok();
			}
			else{
				boards[i].NotOk();
			}
			yield return new WaitForSeconds(1.0f);
		}
	}
}
