using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType 
{
	TopMess,
	OrderedArray,
	ForStack
}

public class ShelveSpawnerWrapper : MonoBehaviour {

	public SpawnType shelveSpawnType;
	public ItemSize preferedSize;
	public bool twoSizeWrapper;
	public ItemSize alternatePreferedSize;
	public ItemType preferedType;
	public int itemAmount;
	private Vector3 spawnZone;
	[HideInInspector]
	public GameObject item;
	[HideInInspector]
	public List<Item> stock = new List<Item>();
	private Transform spawner;
	[HideInInspector] public bool SpawnDone;

	private void Start () 
	{
		GameObject preview = transform.Find("PreviewModel").gameObject;
		spawnZone = preview.transform.localScale;
		preview.SetActive(false);
		RollForItem();
		spawner = transform.GetChild(0);
		switch ((int)shelveSpawnType)
		{			
			case 0: //Top Mess
				StartCoroutine(TopMessSpawner());
			break;

			case 1: //OrderedArray
			break;

			case 2: //ForStack
				float itemXLength = item.transform.localScale.x;
				float itemYLength = item.transform.localScale.y;
				float itemZLength = item.transform.localScale.z;
				int itemsXSpace = (int)(spawnZone.x/itemXLength);
				int itemsYSpace = (int)(spawnZone.y/itemYLength);
				int itemsZSpace = (int)(spawnZone.z/itemZLength);
				int itemAmount = itemsXSpace*itemsYSpace*itemsZSpace;
				for (int x = 0; x < itemsXSpace; x++)
				{
					for (int y = 0; y < itemsYSpace; y++)
					{
						for (int z = 0; z < itemsZSpace; z++)
						{
							Vector3 posMultiplier = new Vector3(x*itemXLength,y*itemYLength,z*itemZLength);
							Vector3 adjustment = new Vector3(-itemXLength/2,itemYLength/2,itemZLength/2);
							Vector3 randomizer = new Vector3(Random.Range(0.00f,0.02f),Random.Range(0.00f,0.02f),Random.Range(0.00f,0.02f));
							Vector3 pos = spawner.position + posMultiplier+adjustment+randomizer;
							GameObject g = (GameObject)Instantiate(item,pos,Quaternion.identity,transform);
							stock.Add(g.GetComponent<Item>());							
						}
					}
				}
				SpawnDone = true;
			break;

			default:
			break;
		}

		
	}

	private void RollForItem()
	{
		float topEggRoll = 0f;
		int bonjour = 0;
		for(int i = 0; i < ItemDatabase.Instance.items.Length;i++)
		{
			ItemSize size = ItemDatabase.Instance.items[i].GetComponent<Item>().item.itemSize;
			ItemType type = ItemDatabase.Instance.items[i].GetComponent<Item>().item.itemType;
			if (size == preferedSize && type == preferedType)
			{
				float f = Random.Range(0.1f,100f);
				if (f>topEggRoll)
				{
					bonjour = i;
				}
			}
			if (size == alternatePreferedSize && type == preferedType && twoSizeWrapper)
			{
				float f = Random.Range(0.1f,100f);
				if (f>topEggRoll)
				{
					bonjour = i;
				}
			} 
		}
		item = ItemDatabase.Instance.items[bonjour];
	}

	private IEnumerator TopMessSpawner() 
	{
		for (int i = 0; i < itemAmount; i++)
		{
			GameObject g = (GameObject)Instantiate(item,spawner.position,Quaternion.identity,transform);
			stock.Add(g.GetComponent<Item>());
			yield return new WaitForSeconds (0.1f);			
		}
		SpawnDone = true;
	}

}
