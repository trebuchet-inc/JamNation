using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockScript : MonoBehaviour {
	Text _text;

	void Start () {
		_text = GetComponentInChildren<Text>();
	}
	
	void Update () {
		int minute = (int)(TimerManager.Timer / 60f);
		int seconde = (int)(TimerManager.Timer % 60f);
		int  mSeconde = (int)((TimerManager.Timer - (int)TimerManager.Timer) * 100); 

		string min = minute >= 10 ? minute.ToString() : "0" + minute;
		string sec = seconde >= 10 ? seconde.ToString() : "0" + seconde;
		string ms = mSeconde >= 10 ? mSeconde.ToString() : "0" + mSeconde;

		_text.text = min + ":" + sec + ":" + ms;
	}
}
