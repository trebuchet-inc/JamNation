using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour {
	GameObject highlight;
	Transform[] Graph;

	bool showing;
	bool isShow;

	void Start () {
		FindHighlight();
	}

	void Update(){
		if (highlight != null) {
			if (!isShow && showing) {
				highlight.gameObject.SetActive (true);
				isShow = true;
			}
			if (isShow && !showing) {
				highlight.gameObject.SetActive (false);
				isShow = false;
			}
			showing = false;
		} 
		else {
			print ("Spooky bug appear");
		}
	}

	public void FindHighlight(){
		Graph = GetComponentsInChildren<Transform>(true);
		for(int i = 0; i < Graph.Length; i++){
			if (Graph[i].name == "Highlight") {
				highlight = Graph[i].gameObject;
				highlight.SetActive(false);
				return;
			}
		}
	}

	public void show () {
		showing = true;
	}
}
