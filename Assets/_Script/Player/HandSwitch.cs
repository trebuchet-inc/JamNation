using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class HandSwitch : MonoBehaviour {
	Animator _anim;
	NVRHand _hand;
	bool _active;

	void Start () {
		_anim = GetComponent<Animator>();
		_hand = transform.parent.GetComponent<NVRHand>();
	}
	
	void Update () {
		if(_hand.UseButtonDown){
			if(_active){
				_anim.SetTrigger("Close");
				_hand.Freeze = false;
				_active = false;
			}
			else{
				_anim.SetTrigger("Open");
				_hand.Freeze = true;
				_active = true;
			}	
		}
	}
}
