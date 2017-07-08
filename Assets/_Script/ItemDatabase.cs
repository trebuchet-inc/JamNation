using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {

	public GameObject[] items;
	public static ItemDatabase Instance;

	// List<Item> allStock = new List<Item>();
	// List<ShelveSpawnerWrapper> 
	public static int Score;
	// Use this for initialization
	
	void Awake()
	{
		Instance = this;		
	}

	void Start () {
		
	}

	void Update () {
		
	}
}
