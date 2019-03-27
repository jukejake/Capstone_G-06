﻿/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;

public class SplashScreen : MonoBehaviour {

	#region Variables
	public float CounterInMinutes = 10.0f;
	private float Counter;
	public float TimeLeft = 0.0f;
	public ButtonSceneData BSD;
	#endregion

	#region Functions
	private void Start() {
		Counter = CounterInMinutes * 60;
		TimeLeft = Counter;
		//Switch to splash screen
		BSD.SwitchScene();
	}

	private void FixedUpdate() {

		//Need to check if a person is using it or not.
		if (!Input.anyKey && Input.touchCount == 0) {
			TimeLeft -= Time.fixedDeltaTime;
			if (TimeLeft < 0.0f) {
				//Switch to splash screen
				TimeLeft = Counter;
				BSD.SwitchScene();
			}
		}
		else { TimeLeft = Counter; }
	}

	private void OnBecameVisible() {
		TimeLeft = Counter;
	}


	#endregion
}
