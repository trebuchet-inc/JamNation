using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTracking : MonoBehaviour {

	[HideInInspector]
	public Transform target;
	private Transform player;
	private Transform eye;

	// Use this for initialization
	void Start () {
		target = transform.GetChild(0);
		player = GameObject.FindGameObjectWithTag("MainCamera").transform;
		eye = GetComponentInChildren<Eye>().transform;
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((player.position-transform.position).magnitude <2.5f)
		{
			eye.forward = (player.position-transform.position).normalized;
		} else
		{
			eye.forward = (target.position-transform.position).normalized;
		}
		

		
	}
}
