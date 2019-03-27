using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchText : MonoBehaviour {
	#region Variables
	private bool HideFirst = false;
	public GameObject FirstTextElement;
	public GameObject SecondTextElement;
	#endregion

	#region Functions
	public void Switch () {
		if (!HideFirst) {
			FirstTextElement.SetActive(false);
			SecondTextElement.SetActive(true);
			HideFirst = true;
		}
		else {
			FirstTextElement.SetActive(true);
			SecondTextElement.SetActive(false);
			HideFirst = false;
		}
	}
	#endregion
}
