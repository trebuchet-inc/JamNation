using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrowdManager : MonoBehaviour 
{
	public GameObject NPCprefab;
	public NpcInfo[] infos;
	public Item[] items;

	private List<Npc> _pooledNPCs;
	
	private IEnumerator SpawnNPCs(int number = 6) // WOLOLO
	{
		yield return 0;

		for (int i = 0; i < number; i++)
		{
			
		}
	}

	private void NewNPC()
	{
		GameObject newNPC = (GameObject)Instantiate(NPCprefab);
		Npc npcInstance = newNPC.GetComponent<Npc>(); 

		npcInstance.info = RollInfo();
		npcInstance.targetItem = RollTarget();

		npcInstance.Init();

		npcInstance.gameObject.SetActive(false);
		_pooledNPCs.Add(npcInstance);
	}

	private NpcInfo RollInfo()
	{
		return infos[Random.Range(0, infos.Length)];
	}

	private Item RollTarget()
	{
		return items[Random.Range(0, items.Length)];
	}
}
