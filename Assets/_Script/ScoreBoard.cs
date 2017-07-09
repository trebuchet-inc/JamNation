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
		AkSoundEngine.PostEvent("Play_JudgementGood", gameObject);
		O.SetActive(true);
		_particle.Play();
	}

	public void NotOk(){
		AkSoundEngine.PostEvent("Play_JudgementSad", gameObject);
		X.SetActive(true);
		_particle.Play();
	}
}
