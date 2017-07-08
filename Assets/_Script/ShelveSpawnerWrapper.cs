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
	public ItemType preferedType;
	public int itemAmount;
	[HideInInspector]
	public GameObject item;
	[HideInInspector]
	public List<GameObject> stock = new List<GameObject>();
	private Transform spawner;

	

	void Awake()
	{
		
	}

	// Use this for initialization
	void Start () {
		transform.FindChild("PreviewModel").gameObject.SetActive(false);
		RollForItem();
		switch ((int)shelveSpawnType)
		{			
			case 0: //Top Mess
			spawner = transform.GetChild(0);
			StartCoroutine(TopMessSpawner());
			break;
			case 1: //OrderedArray
			break;
			case 2: //ForStack
			break;
			default:
			break;
		}

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void RollForItem()
	{
		float topEggRoll = 0f;
		int bonjour = 0;
		for(int i =0; i < ObjectDatabase.Instance.items.Length;i++)
		{
			ItemSize size = ObjectDatabase.Instance.items[i].GetComponent<Item>().item.itemSize;
			ItemType type = ObjectDatabase.Instance.items[i].GetComponent<Item>().item.itemType;
			if (size == preferedSize && type == preferedType)
			{
				float f = Random.Range(0.1f,100f);
				if (f>topEggRoll)
				{
					bonjour = i;
				}
			}
		}
		item = ObjectDatabase.Instance.items[bonjour];
	}

	private IEnumerator TopMessSpawner() 
	{
		for (int i = 0; i < itemAmount; i++)
		{
			GameObject g = (GameObject)Instantiate(item,spawner.position,Quaternion.identity);
			stock.Add(g);
			yield return new WaitForSeconds (0.1f);			
		}
	}

}
