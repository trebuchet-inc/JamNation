using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Exit : MonoBehaviour {

	private void OnTriggerEnter(Collider col)
	{
		Npc n = col.gameObject.GetComponent<Npc>();

		if(n != null)
		{
			CrowdManager.Instance.PushToPool(n);
		}
	}
}
