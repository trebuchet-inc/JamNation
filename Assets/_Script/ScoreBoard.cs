using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour {
	public GameObject O;
	public GameObject X;
	ParticleSystem _particle;
	
	void Start () {
		_particle = GetComponentInChildren<ParticleSystem>();
		O.SetActive(false);
		X.SetActive(false);
	}
	
	public void Ok(){
		O.SetActive(true);
		AkSoundEngine.PostEvent("Play_JudgementGood", gameObject);
		_particle.Play();
	}

	public void NotOk(){
		X.SetActive(true);
		AkSoundEngine.PostEvent("Play_JudgementSad", gameObject);
		_particle.Play();
	}
}
