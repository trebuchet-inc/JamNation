﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisabler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		other.gameObject.SetActive(false);
	}
}
