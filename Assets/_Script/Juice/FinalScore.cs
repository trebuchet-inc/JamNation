using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour {
	Text _text;

	void Start () {
		_text = GetComponentInChildren<Text>();
	}

	public void ShowScore () {
		_text.text = "Your Score :\n" + TimerManager.Score;
	}

	public void Comment (int ok) {
		switch(ok){
			case 0 :
				_text.text = "So bad";
				_text.color = Color.red;
				AkSoundEngine.PostEvent("Play_JudgementSad", gameObject);
			break;

			case 1 :
				_text.text = "Meh";
				_text.color = Color.red;
				AkSoundEngine.PostEvent("Play_JudgementSad", gameObject);
			break;

			case 2 :
				_text.text = "Close one";
				_text.color = Color.green;
				AkSoundEngine.PostEvent("Play_JudgementNormal", gameObject);
			break;

			case 3 :
				_text.text = "You are a master consumer";
				_text.color = Color.green;
				AkSoundEngine.PostEvent("Play_JudgementGood", gameObject);
			break; 
		}
	}
}
