using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFeedback : MonoBehaviour {
	[HideInInspector] public Text txt;

	Canvas _canvas;
	Transform _head;
	float _timer;

	public 	void Init () {
		txt = GetComponentInChildren<Text>();
		_canvas = GetComponentInChildren<Canvas>();
		_head = Camera.main.transform;
	}
	

	void Update () {
		_timer += Time.deltaTime;
		_canvas.transform.position += Vector3.up * Time.deltaTime;
		_canvas.transform.LookAt(_head.position);
		Color c = txt.color;
		float a = 4f - _timer;
		txt.color = new Color(c.r, c.g, c.b, a);
		if(_timer >= 5){
			Destroy(this.gameObject);
		}
	}
}
