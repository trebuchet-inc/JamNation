using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {

	public GameObject[] items;
	public GameObject[] foodZones;
	public GameObject[] miscZones;
	public GameObject[] electroZones;
	public static ItemDatabase Instance;
	
	void Awake()
	{
		Instance = this;		
	}
}
