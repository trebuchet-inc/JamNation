using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnVraieNom : MonoBehaviour {
	public GameObject ObjToSpawn;

	void Awake () {
		Instantiate(ObjToSpawn, transform.position, transform.rotation);
	}
}
