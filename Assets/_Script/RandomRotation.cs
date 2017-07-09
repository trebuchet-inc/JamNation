using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 orir = transform.localEulerAngles;
		Vector3 rand = new Vector3(Random.Range(-15f,15f),Random.Range(-15f,15f),Random.Range(-15f,15f));
		orir = orir + rand;
		transform.localEulerAngles = orir;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
