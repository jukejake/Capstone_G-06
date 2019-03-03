﻿/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;

public class WheelController : SerializedMonoBehaviour {

	#region Variables
	[Range(0,250)]
	public int Speed;
	private int OldSpeed;
	private float WheelSize = 0.6884924f; //27.106 inches to meters
	public float RotateAmount { get; set; }
	public TextMeshProUGUI SpeedText;

	private bool UseTreadmill = false;
	[ShowIf("UseTreadmill")]
	public Renderer Treadmill;
	[ShowIf("UseTreadmill")]
	public float TreadmillSpeed = 0.2f;
	private float offset = 0.0f;
	[HideIf("UseTreadmill")]
	public GameObject Dynos;
	#endregion

	#region Setup
	//Used for initialization.
	private void Awake() {
		RotateAmount = (Speed / (WheelSize*Mathf.PI));
		InvokeRepeating("DelayedUpdate", 1.0f, 0.20f);
	}
	//Create a slower Update.
	void DelayedUpdate() {
		//If the speed is different, re-calculate the speed.
		if (OldSpeed == Speed) { return; }

		RotateAmount = (Speed / (WheelSize*Mathf.PI));
		SpeedText.text = Speed.ToString();
		OldSpeed = Speed;
	}
	//Cancel the Invoke when the object is destroyed.
	private void OnDestroy() { CancelInvoke("DelayedUpdate"); }
	private void FixedUpdate() {
		if (UseTreadmill != false) {
			offset += Time.deltaTime * RotateAmount * TreadmillSpeed;
			Treadmill.material.SetTextureOffset("_MainTex", new Vector2(0, -offset));
		}
	}


	//Function to set the speed based on a UI slider.
	public void SetSpeed(Slider obj) {
		Speed = (int)obj.value;
	}
	//Function to set the speed based on a float value.
	public void SetSpeed(float value) {
		Speed = (int)value;
	}

	[Button]
	public void SwitchRollers() {
		if (!UseTreadmill) {
			Dynos.SetActive(false);
			Treadmill.gameObject.SetActive(true);
			UseTreadmill = true;
		}
		else {
			Dynos.SetActive(true);
			Treadmill.gameObject.SetActive(false);
			UseTreadmill = false;
		}
	}
	
	#endregion

}
