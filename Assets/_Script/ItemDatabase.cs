using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {

	public GameObject[] items;
	public GameObject[] foodZones;
	public GameObject[] miscZones;
	public GameObject[] electroZones;
	public static ItemDatabase Instance;

	// List<Item> allStock = new List<Item>();
	// List<ShelveSpawnerWrapper> 

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
