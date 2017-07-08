using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManagement : MonoBehaviour {
	Camera _head;

	void Start () {
		_head = Camera.main;
	}
	
	void Update () {
		transform.position = new Vector3(_head.transform.position.x,0,_head.transform.position.z);
	}
}
