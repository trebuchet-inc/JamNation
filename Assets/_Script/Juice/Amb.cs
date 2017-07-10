using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amb : MonoBehaviour {
	public string musique;

	public string[] events;
	public float minTime;
	public float maxTime;

	bool _playing;

	float timer;

	void Start () {
		timer = Random.Range(minTime, maxTime);
		
	}
	
	void Update () {
		if(TimerManager.started && !_playing){
			AkSoundEngine.PostEvent(musique, gameObject);
			_playing = true;

			//AkSoundEngine.SetRTPCValue()
		}

		timer -= Time.deltaTime;
		if(timer <= 0){
			AkSoundEngine.PostEvent(events[Random.Range(0, events.Length)], gameObject);
			timer = Random.Range(minTime, maxTime);
		}
	}
}
