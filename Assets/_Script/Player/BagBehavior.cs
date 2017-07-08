using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagBehavior : MonoBehaviour {
	public Transform overlapTransform;
	public Vector3 overlapBox;
	public LayerMask layer;
	public GameObject particule;

	HandSwitch handState;

	void Start () 
	{
		handState = GetComponent<HandSwitch>();
	}
	
	void Update()
	{
		if(handState.active){
			Collider[] col = Physics.OverlapBox(overlapTransform.transform.position, overlapBox, overlapTransform.rotation, layer);
			foreach(Collider c in col){
				Item item = c.attachedRigidbody.GetComponent<Item>();
				if(item != null){
					ObjectDatabase.Score += item.item.scoreValue;
					ScoreFeedback sf = Instantiate(particule, transform.position, transform.rotation).GetComponent<ScoreFeedback>();
					sf.Init();
					sf.txt.text = item.item.scoreValue.ToString();
					Destroy(item.gameObject);
				}
			}
		}
	}
}
