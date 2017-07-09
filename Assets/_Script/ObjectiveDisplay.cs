using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ObjectiveDisplay : MonoBehaviour {
	
	Text _text;

	private void Start () 
	{
		_text = GetComponentInChildren<Text>();

		ObjectivesManager.Instance.CreateObjectives();
		
		string objectiveText = "<b>COMPLETE AT LEAST 3</b>\n";

		foreach (Objective obj in ObjectivesManager.Instance.objectivesToComplete)
		{
			objectiveText += obj.qtyToGet + "  X  " + obj.itemToGet.name + "s\n";
		}

		_text.text = objectiveText;
	}
	
}
