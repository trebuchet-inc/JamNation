using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTrailBehavior : MonoBehaviour {
	Rigidbody _rb;
	ParticleSystem _particle;
	Transform _head;

	void Start () {
		_rb = transform.parent.GetComponent<Rigidbody>();
		_particle = GetComponentInChildren<ParticleSystem>();
		_head = Camera.main.transform;		
	}

	void Update () {
		if(_rb.velocity.magnitude > 5f){
			if(!_particle.isPlaying){
				_particle.Play();
			}
			transform.position = Vector3.Lerp(transform.position, _head.position + _rb.velocity.normalized, Time.deltaTime * 10);
			transform.rotation =Quaternion.LookRotation(_rb.velocity.normalized, Vector3.up);
		}
		else{
			if(_particle.isPlaying){
				_particle.Stop();
			}
		}
	}
}
