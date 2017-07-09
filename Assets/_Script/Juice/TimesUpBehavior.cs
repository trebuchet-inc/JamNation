using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimesUpBehavior : MonoBehaviour {
	Transform _head;
	Transform _canvas;
	bool _active;
	bool _freeze;
	float _timer;

	void Start()
	{
		_head = Camera.main.transform;
		_canvas = transform.GetChild(0);
		_canvas.gameObject.SetActive(false);
	}
	
	void Update () 
	{
		if(TimerManager.Timer <= 0 && !_freeze){
			if(!_active){
				_canvas.gameObject.SetActive(true);
				transform.position = _head.position;
				transform.rotation = Quaternion.LookRotation(-_head.transform.forward, _head.transform.up);
				_active = true;
			}

			transform.position = _head.position;
			transform.rotation = Quaternion.Lerp(transform.rotation, _head.rotation, Time.deltaTime * 10);
			_timer += Time.deltaTime;
			if(_timer >= 4){
				_canvas.gameObject.SetActive(false);
				_freeze = true;
			}
		}
	}
}