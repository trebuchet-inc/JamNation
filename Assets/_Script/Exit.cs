using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Exit : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
		Npc n = other.GetComponentInParent<Npc>();

		if(n != null && n.canExit)
		{
			CrowdManager.Instance.PushToPool(n);
		}
	}
}
