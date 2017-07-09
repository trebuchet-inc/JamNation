using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MonsterLove.StateMachine;

[RequireComponent(typeof(Rigidbody),typeof(NavMeshAgent))]
public class Npc : MonoBehaviour 
{
	public NpcInfo info;

	public ShelveSpawnerWrapper targetShelve;

	private NavMeshAgent _agent;
	private Rigidbody _rb;
	private Transform _modelAnchor;
	private Transform _itemHolder;
	private Animator _chanim;

	public List<Item> itemsInHands = new List<Item>();
	private Item _lastItem;

	public enum States
	{
		Wander,
		Target,
		Taking,
		Exiting,
		KO
	}
	
	public StateMachine<States> stm;

	public void Init()
	{
		stm = StateMachine<States>.Initialize(this);
		
		_rb = GetComponent<Rigidbody>();

		_agent = GetComponent<NavMeshAgent>();
		_agent.speed = info.moveSpeed;
		
		_modelAnchor = transform.Find("model");
		Instantiate(info.model, _modelAnchor.position, Quaternion.identity, _modelAnchor);
		_itemHolder = FindGrandchild(transform, "holder");
		_chanim = GetComponentInChildren<Animator>();	
	}

	public void Reset()
	{
		foreach (Item item in itemsInHands)
		{
			Destroy(item.gameObject);
		}
		_rb.constraints = RigidbodyConstraints.FreezeAll;
		_rb.useGravity = false; 
		itemsInHands.Clear();
		_lastItem = null;
		targetShelve = null;
	}

	private void Wander_Enter()
	{
		StartCoroutine(Wander());
		_chanim.SetBool("isWalking", true);
	}

	private void Wander_Update()
	{
		if(_agent != null)
		{
			if(_agent.velocity.magnitude > 0.1f)
			{
				_chanim.SetBool("isWalking", true);
			}
			else
			{
				_chanim.SetBool("isIdle", true);
			}
		}
	}

	private void Wander_Exit()
	{
		StopCoroutine(Wander());
	}
	
	private void Target_Enter()
	{
		if(targetShelve == null) CrowdManager.Instance.RollTarget(info.favoriteItemSize);
		
		GoTo(targetShelve.transform.position);

		_chanim.SetBool("isWalking", true);
	}

	private void Target_Update()
	{
		if(targetShelve == null) stm.ChangeState(States.Wander);

		if(_agent.remainingDistance < 1.2f)
		{
			stm.ChangeState(States.Taking);
		}
	}

	private void Target_Exit()
	{
		_agent.isStopped = true;		
	}

	private void Taking_Enter()
	{
		transform.LookAt(new Vector3(targetShelve.transform.position.x, transform.position.y, targetShelve.transform.position.z));

		_rb.isKinematic = true;

		StartCoroutine(TakingSequence());
	}

	private void Taking_Update()
	{
		if(Vector3.Distance(transform.position, targetShelve.transform.position) > 1.5f)
		{
			StopCoroutine(TakingSequence());
			stm.ChangeState(States.Target);
		}
	}

	private void Taking_Exit()
	{
		_rb.isKinematic = false;
	}

	private void Exiting_Enter()
	{
		_agent.ResetPath();
		_agent.isStopped = false;
		GoTo(CrowdManager.Instance.FindExit().position);
		_chanim.SetBool("isWalking", true);
	}	

	private IEnumerator TakingSequence()
	{
		for (int i = 0; i < targetShelve.stock.Count; i++)
		{
			TakeItem(targetShelve.stock[i]);

			yield return new WaitForSeconds(0.4f);
		}

		stm.ChangeState(States.Exiting);
	}

	private void TakeItem(Item item)
	{
		Vector3 pos = Vector3.zero;
		
		_chanim.SetBool("isTaking", true);
		_chanim.SetBool("isHolding", true);
		
		itemsInHands.Add(item);

		item.transform.SetParent(_itemHolder);
		
		if(_lastItem != null)
		{
			pos = new Vector3(_itemHolder.transform.position.x + (Random.value * 0.1f),
										_lastItem.transform.position.y + (Random.value * 0.15f),
										_itemHolder.transform.position.z + (Random.value * 0.1f));
		}
		else 
		{
			pos = _itemHolder.transform.position;
		}

		item.rb.isKinematic = true;		
		item.transform.SetPositionAndRotation(pos, Quaternion.identity);
		_lastItem = item;
		
		targetShelve.stock.Remove(item);	
	}

	private void KO_Enter()
	{
		_agent.isStopped = true;
		_agent.enabled = false;

		_chanim.SetBool("isHolding", false);
		_chanim.SetBool("isWalking", false);		
	}

	private IEnumerator GetHit(Vector3 epicentre)
	{
		yield return 0;
		
		foreach (Item item in itemsInHands)
		{
			item.transform.parent = null;
			item.rb.isKinematic = false;
			item.rb.AddExplosionForce(5, epicentre, 3, Random.Range(1.0f,3.0f), ForceMode.Impulse);
		}
		
		itemsInHands.Clear();
		_rb.constraints = RigidbodyConstraints.None;
		_rb.useGravity = true; 
		_rb.AddExplosionForce(info.forceOnPunch, epicentre, 0, Random.Range(2.5f, 5.0f), ForceMode.Impulse);

		yield return new WaitForSeconds(3.0f);

		CrowdManager.Instance.PushToPool(this);
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

	private Transform FindGrandchild(Transform parent, string tName)
	{
		Transform[] ts = parent.GetComponentsInChildren<Transform>();

		foreach (Transform t in ts)
		{
			if(t.name == tName) return t;
		}
		
		return null;
	}

	private void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.CompareTag("Hand"))
		{
			AkSoundEngine.PostEvent("Play_Fatlord", gameObject);

			StartCoroutine(GetHit(col.contacts[Random.Range(0, col.contacts.Length)].point));
			stm.ChangeState(States.KO);
		}
	}
}
