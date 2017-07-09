using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour {
	public static float Timer;
	public static int Score;

	public float timer;
	public ScoreBoard[] boards;
	public int[] paliers;
	public GameObject Spawn;

	public static bool started;

	GameObject _player;
	FinalScore _finalScore;
	bool _end;
	bool _init;
	
	void Start () {
		Timer = timer;
		_player = GameObject.FindGameObjectWithTag("Player");
		_finalScore = FindObjectOfType<FinalScore>();
	}
	
	void Update () 
	{
		if(!_init && started)
		{
			FindObjectOfType<CrowdManager>().Go();
			_init = true;
		}

		if(!_end && started)
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
		yield return new WaitForSeconds(2.0f);
		SteamVR_Fade.View(new Color(0,0,0,1), 2.0f);
		yield return new WaitForSeconds(2.0f);
		SteamVR_Fade.View(new Color(0,0,0,0), 2.0f);
		_finalScore.ShowScore();
		_player.transform.position = Spawn.transform.position;
		yield return new WaitForSeconds(5.0f);
		int OkNb= 0;
		for(int i = 0; i < boards.Length; i++)
		{
			if(ObjectivesManager.Instance.CheckObjectives() >= paliers[i]){
				boards[i].Ok();
				OkNb++;
			}
			else{
				boards[i].NotOk();
			}
			yield return new WaitForSeconds(1.0f);
		}
		yield return new WaitForSeconds(1.0f);
		_finalScore.Comment(OkNb);
	}
}
