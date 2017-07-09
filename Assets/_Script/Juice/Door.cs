using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	Animator _anim;

	void Start()
	{
		_anim = GetComponent<Animator>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.attachedRigidbody != null && other.attachedRigidbody.tag == "Player"){
			_anim.SetBool("Open", true);
			TimerManager.started = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.attachedRigidbody != null && other.attachedRigidbody.tag == "Player"){
			_anim.SetBool("Open", false);
		}
	}
}
