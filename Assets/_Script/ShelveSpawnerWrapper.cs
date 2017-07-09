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
	public List<Item> stock = new List<Item>();
	private Transform spawner;
	[HideInInspector] public bool SpawnDone;

	private void Start () 
	{
		GameObject preview = transform.Find("PreviewModel").gameObject;
		spawnZone = preview.transform.localScale;
		preview.SetActive(false);
		GameObject item = RollForItem();
		
		ObjectivesManager.Instance.itemsInStore.Add(item.GetComponent<ItemInfo>());

		spawner = transform.GetChild(0);
		switch ((int)shelveSpawnType)
		{			
			case 0: 
				StartCoroutine(TopMessSpawner(item));
			break;

			case 1: 
				StartCoroutine(OrderedArray(item));
			break;

			case 2: 
				StartCoroutine(ForStack(item));
			break;

			default:
				StartCoroutine(TopMessSpawner(item));
			break;
		}		
	}

	private GameObject RollForItem()
	{
		GameObject itemRolled = new GameObject();

		float topEggRoll = 0f;

		for(int i = 0; i < ItemDatabase.Instance.items.Length;i++)
		{
			Item itemScript = ItemDatabase.Instance.items[i].GetComponent<Item>();
			ItemSize size = itemScript.item.itemSize;
			ItemType type = itemScript.item.itemType;
			if (size == preferedSize && type == preferedType)
			{
				float f = Random.Range(0.1f,100f);
				if (f>topEggRoll)
				{
					topEggRoll = f;
					itemRolled = ItemDatabase.Instance.items[i];
				}
			}		
		}
		
		return itemRolled;
	}

	private IEnumerator TopMessSpawner(GameObject it) 
	{
		for (int i = 0; i < itemAmount; i++)
		{
			GameObject g = (GameObject)Instantiate(it,spawner.position,Quaternion.identity,transform);
			Item newItem = g.GetComponent<Item>();
			newItem.Init();
			stock.Add(newItem);
			yield return new WaitForSeconds(0.2f);
		}
		SpawnDone = true;
	}

	private IEnumerator OrderedArray(GameObject it)
	{
		yield return 0;

		Transform t =  transform.Find("OrderArray");
		int count = t.childCount;
			
		for (int i = 0; i < count; i++)
		{
			Vector3 p = t.GetChild(i).position;
			GameObject g = (GameObject)Instantiate(it,p,Quaternion.identity,transform);
			Item newItem = g.GetComponent<Item>();
			newItem.Init();
			stock.Add(newItem);

			yield return 0;
		}
	}

	private IEnumerator ForStack(GameObject it)
	{
		yield return 0;

		BoxCollider col = it.GetComponentInChildren<BoxCollider>();
				
		float itemXLength = col.size.x;
		float itemYLength = col.size.y;
		float itemZLength = col.size.z;
	
		int itemsXSpace = (int)(spawnZone.x/itemXLength);
		int itemsYSpace = (int)(spawnZone.y/itemYLength);
		int itemsZSpace = (int)(spawnZone.z/itemZLength);
		int itemAmount = itemsXSpace*itemsYSpace*itemsZSpace;
		Vector3 spaceAjust = new Vector3(spawnZone.x/2,-spawnZone.y/2,spawnZone.z/2);
		Vector3 adjustment = new Vector3(-itemXLength/2,itemYLength/2,-itemZLength/2);

		for (int x = 0; x < itemsXSpace; x++)
		{
			for (int y = 0; y < itemsYSpace; y++)
			{
				for (int z = 0; z < itemsZSpace; z++)
				{
					Vector3 posMultiplier = new Vector3(-x*itemXLength,y*itemYLength,-z*itemZLength);
					Vector3 randomizer = new Vector3(Random.Range(-0.01f,0.01f),Random.Range(-0.01f,0.01f),Random.Range(-0.01f,0.01f));
					Vector3 pos = spawner.localPosition + spaceAjust + posMultiplier +adjustment+ randomizer;
					GameObject g = (GameObject)Instantiate(it,pos,Quaternion.identity,transform);
					g.transform.localPosition = pos;
					g.transform.localRotation = spawner.localRotation;						
					Item newItem = g.GetComponent<Item>();
					newItem.Init();
					stock.Add(newItem);							
				}
			}
			yield return 0;
		}
		SpawnDone = true;
	}
}
