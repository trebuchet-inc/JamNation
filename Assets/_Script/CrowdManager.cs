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

	private List<ShelveSpawnerWrapper> _shelves = new List<ShelveSpawnerWrapper>();
	private List<Npc> _pooledNPCs;

	//DEBUG
	public Transform spawn;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		_pooledNPCs = new List<Npc>();
		StartCoroutine(SpawnNPCs(spawn.position, 12));
	}
	
	private IEnumerator SpawnNPCs(Vector3 basePos, int number = 6) // WOLOLO
	{
		for (int i = 0; i < number; i++)
		{
			if(_pooledNPCs.Count == 0)
			{
				NewNPC();
			}
			
			Npc npcToSpawn = PullFromPool();

			Vector3 pos = new Vector3(basePos.x, basePos.y, basePos.z + i*1.5f);
			npcToSpawn.gameObject.SetActive(true);
			npcToSpawn.transform.SetPositionAndRotation(pos, Quaternion.identity);
			
			// npcToSpawn.stm.ChangeState(Npc.States.Wander);

			if(Random.value > 0.5f)
			{
				npcToSpawn.stm.ChangeState(Npc.States.Wander);
			}
			else
			{
				npcToSpawn.stm.ChangeState(Npc.States.Target);
			}

			yield return new WaitForSeconds(0.1f);
		}
	}

	private Npc PullFromPool()
	{
		Npc npcToPull = _pooledNPCs.OrderBy(n => Random.value).FirstOrDefault();
		
		npcToPull.targetShelve = RollTarget(npcToPull.info.favoriteItemSize);

		_pooledNPCs.Remove(npcToPull);

		return npcToPull;
	}

	private void PushToPool(Npc npcToPush)
	{
		npcToPush.gameObject.SetActive(false);
		_pooledNPCs.Add(npcToPush);
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

		return  _shelves.OrderBy(s => Random.value).FirstOrDefault();
	}
}
