using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class HandSwitch : MonoBehaviour {
	[HideInInspector] 
	public bool active;

	Animator _anim;
	NVRHand _hand;

	void Start () {
		_anim = GetComponent<Animator>();
		_hand = transform.parent.GetComponent<NVRHand>();
		if(_hand == null){
			_hand = transform.parent.parent.GetComponent<NVRHand>();
		}
	}
	
	void Update () {
		if(_hand.UseButtonDown){
			if(active){
				_anim.SetTrigger("Close");
				_hand.Freeze = false;
				active = false;
			}
			else{
				_anim.SetTrigger("Open");
				_hand.Freeze = true;
				active = true;
			}	
		}
	}
}
