using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {

	public GameObject[] items;
	public static ItemDatabase Instance; 
	// Use this for initialization
	
	void Awake()
	{
		Instance = this;		
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
