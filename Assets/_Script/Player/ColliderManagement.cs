using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManagement : MonoBehaviour {
	Camera _head;
	Rigidbody _rb;

	void Start () {
		_head = Camera.main;
		_rb = transform.parent.GetComponent<Rigidbody>();
	}
	
	void Update () {
		if(_rb.velocity.magnitude > 0.1f)
		transform.position = new Vector3(_head.transform.position.x,0,_head.transform.position.z);
		transform.eulerAngles = new Vector3(0,_head.transform.eulerAngles.y,0);
	}
}
