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
		_particle.Play();
	}

	public void NotOk(){
		X.SetActive(true);
		_particle.Play();
	}
}
