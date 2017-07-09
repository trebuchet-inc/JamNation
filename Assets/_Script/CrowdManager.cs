using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public class CrowdManager : MonoBehaviour 
{
	public static CrowdManager Instance;
	
	public GameObject NPCprefab;
	public NpcInfo[] infos;

	public int startingAmount;
	public int maxPooledAmount;
	private List<ShelveSpawnerWrapper> _shelves = new List<ShelveSpawnerWrapper>();
	private List<Npc> _pooledNPCs;

	//DEBUG.LOL
	public Transform spawn;
	public Transform[] exits;

	private void Awake()
	{
		Instance = this;
	}

	public void Go()
	{
		_pooledNPCs = new List<Npc>();
		StartCoroutine(SpawnInLine(spawn.position, startingAmount));
	}

	private GameObject Spawn()
	{
		Npc npcToSpawn = new Npc();

		if(_pooledNPCs.Count < maxPooledAmount)
		{
			NewNPC();
		}
		
		npcToSpawn = PullFromPool();
		
		npcToSpawn.gameObject.SetActive(true);		

		return npcToSpawn.gameObject;
	}
	
	private IEnumerator SpawnInLine(Vector3 basePos, int number = 6, float initialDelay = 0) // WOLOLO
	{
		yield return new WaitForSeconds(initialDelay);
		
		for (int i = 0; i < number; i++)
		{
			Vector3 pos = new Vector3(basePos.x, basePos.y, basePos.z + i * 1.5f);		
			GameObject newNPC = Spawn();
			newNPC.transform.SetPositionAndRotation(pos, Quaternion.identity);
			newNPC.GetComponent<Npc>().stm.ChangeState(Npc.States.Target);	

			yield return new WaitForSeconds(0.15f);	
		}
	}

	private void SpawnAtRandomPosition()
	{
		GameObject newNPC = Spawn();
		Transform e = FindExit();
		Vector3 pos = new Vector3(e.position.x + Random.value, e.position.y, e.position.z);
		newNPC.transform.SetPositionAndRotation(pos, Quaternion.identity);
		newNPC.GetComponent<Npc>().stm.ChangeState(Npc.States.Target);	
	}

	private Npc PullFromPool()
	{
		Npc npcToPull = _pooledNPCs.OrderBy(n => Random.value).FirstOrDefault();		

		npcToPull.Reset();
		_pooledNPCs.Remove(npcToPull);

		return npcToPull;
	}

	public void PushToPool(Npc npcToPush)
	{
		npcToPush.gameObject.SetActive(false);
		_pooledNPCs.Add(npcToPush);

		SpawnAtRandomPosition();
	}

	private void NewNPC()
	{
		GameObject newNPC = (GameObject)Instantiate(NPCprefab);
		Npc npcInstance = newNPC.GetComponent<Npc>(); 

		npcInstance.info = RollInfo();

		npcInstance.Init();

		npcInstance.gameObject.SetActive(false);
		_pooledNPCs.Add(npcInstance);
	}

	private NpcInfo RollInfo()
	{
		return infos[Random.Range(0, infos.Length)];
	}

	public ShelveSpawnerWrapper RollTarget(ItemSize favoriteSize)
	{
		if(_shelves.Count == 0) _shelves = FindObjectsOfType<ShelveSpawnerWrapper>().ToList();

		ShelveSpawnerWrapper shelve = _shelves.Where(s => s.preferedSize == favoriteSize).OrderBy(s => Random.value).FirstOrDefault();

		if(shelve != null)
		{
			return shelve;
		}

		return _shelves.OrderBy(s => Random.value).FirstOrDefault();		
	}

	public Transform FindExit()
	{
		return exits[Random.Range(0,exits.Length)];
	}
}
