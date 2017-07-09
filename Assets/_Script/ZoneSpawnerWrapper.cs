using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSpawnerWrapper : MonoBehaviour {

	public ItemType preferedZoneType;
	public GameObject myZone;


	// Use this for initialization

	void Awake()
	{
		preferedZoneType = (ItemType)Random.Range(0,3);
		float dice = 0.0f;
		int bigHead = 0;
		GameObject[] zoneArray = GetZoneArray(preferedZoneType);
		for (int i = 0; i < zoneArray.Length; i++)
		{
			float yahoo = Random.Range(0.1f,100f);
			if (yahoo > dice)
			{
				dice = yahoo;
				bigHead = i;
			}			
		}
		myZone = (GameObject)Instantiate(zoneArray[bigHead],transform.position,Quaternion.identity,transform);
	}

	void Start () {
		transform.GetChild(0).gameObject.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private GameObject[] GetZoneArray(ItemType type)
	{
		if (type == ItemType.Electronic)
		{
			return ItemDatabase.Instance.electroZones;
		} else if (type == ItemType.Food)
		{
			return ItemDatabase.Instance.foodZones;
		} else if (type == ItemType.Misc)
		{
			return ItemDatabase.Instance.miscZones;
		}
		return null;
	} 
}
