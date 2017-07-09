using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amb : MonoBehaviour {
	public string[] events;
	public float minTime;
	public float maxTime;

	float timer;

	void Start () {
		timer = Random.Range(minTime, maxTime);
	}
	
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0){
			AkSoundEngine.PostEvent(events[Random.Range(0, events.Length)], gameObject);
			timer = Random.Range(minTime, maxTime);
		}
	}
}
