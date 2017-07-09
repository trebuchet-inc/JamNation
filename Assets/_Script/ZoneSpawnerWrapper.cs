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
	
	private GameObject[] GetZoneArray(ItemType type)
	{
		ItemDatabase itemdata = FindObjectOfType<ItemDatabase>();
		if (type == ItemType.Electronic)
		{
			return itemdata.electroZones;
		} else if (type == ItemType.Food)
		{
			return itemdata.foodZones;
		} else if (type == ItemType.Misc)
		{
			return itemdata.miscZones;
		}
		return null;
	} 
}
