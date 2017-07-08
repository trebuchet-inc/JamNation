using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MonsterLove.StateMachine;

[RequireComponent(typeof(Rigidbody),typeof(NavMeshAgent))]
public class Npc : MonoBehaviour 
{
	public NpcInfo info;

	public Item targetItem;

	private NavMeshAgent _agent;
	private Rigidbody _rb;
	private Transform _modelAnchor;

	public enum States
	{
		Wander,
		Target,
		KO
	}
	
	public StateMachine<States> stm;

	public void Init()
	{
		stm = StateMachine<States>.Initialize(this);
		
		_agent = GetComponent<NavMeshAgent>();
		_rb = GetComponent<Rigidbody>();
		_modelAnchor = transform.Find("model");

		_agent.speed = info.moveSpeed;
		Instantiate(info.model, _modelAnchor.position, Quaternion.identity, _modelAnchor);
	}

	private void Wander_Enter()
	{
		StartCoroutine(Wander());
	}

	private void Wander_Exit()
	{
		StopCoroutine(Wander());
	}

	public void GoTo(Vector3 pos)
	{		
		_agent.SetDestination(pos);
	}

	private IEnumerator Wander()
	{
		yield return new WaitForSeconds(Random.Range(1f,4f));

		GoTo(RandNavMeshPos(transform.position, 15));
		StartCoroutine(Wander());
	}

	public Vector3 RandNavMeshPos(Vector3 origin, float dist)
    {
        Vector3 randDir = UnityEngine.Random.insideUnitSphere * dist;

        randDir += origin;

        NavMeshHit hit;
        NavMesh.SamplePosition(randDir, out hit, dist, NavMesh.AllAreas);

        return hit.position;
    }
}
