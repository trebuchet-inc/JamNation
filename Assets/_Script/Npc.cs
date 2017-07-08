using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody),typeof(NavMeshAgent))]
public class Npc : MonoBehaviour 
{
	public NpcInfo info;

	public Item targetItem;

	private NavMeshAgent _agent;
	private Rigidbody _rb;

	public void Init()
	{
		_agent = GetComponent<NavMeshAgent>();
		_rb = GetComponent<Rigidbody>();
	}

	public void GoTo(Vector3 pos)
	{		
		_agent.SetDestination(pos);
	}	
}
