using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour {

	private MeshRenderer rend;

	// Use this for initialization
	void Start () {
		rend = transform.Find("model").GetComponentInChildren<MeshRenderer>();
		foreach(Material m in rend.materials)
		{
			m.color = Random.ColorHSV(0.5f,1f,0.5f,1f,0.5f,1f,1f,1f);
		}
		// rend.materials.color = Random.ColorHSV(0f,1f,0f,1f,0f,1f,1f,1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
