using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class HandMoving : MonoBehaviour {
	public float speed;
	public float headOffset;

	Transform _head;
	Rigidbody _rb;
	Vector3 _prevPoint;
	NVRHand _hand;
	bool _amplitudeReach;
	bool _back;

	void Start () {
		_head = Camera.main.transform;
		_rb = transform.parent.GetComponent<Rigidbody>();
		_hand = GetComponent<NVRHand>();
	}
	
	void Update () {
		Vector3 headLocalPos = transform.InverseTransformPoint(_head.position);
		if(Vector3.Distance(_prevPoint,transform.localPosition) > 0.1f){
			_amplitudeReach = true;
		}
		if((headLocalPos.z > headOffset && _back) || (headLocalPos.z < headOffset && !_back)){
			if(_hand.RunButtonPressed && _amplitudeReach && transform.up.y > 0.0 && transform.forward.y < 0.5f && transform.forward.y > -0.5f){
				Vector3 dir = new Vector3(transform.forward.x,0,transform.forward.z).normalized;
				_rb.AddForce(dir * speed, ForceMode.Impulse);
			}
			_back = !_back;
			_prevPoint = transform.localPosition;
			_amplitudeReach = false;
		}
	}
}
