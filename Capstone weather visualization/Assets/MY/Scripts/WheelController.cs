/*////
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

	//Function to set the speed based on a UI slider.
	public void SetSpeed(Slider obj) {
		Speed = (int)obj.value;
	}
	//Function to set the speed based on a float value.
	public void SetSpeed(float value) {
		Speed = (int)value;
	}
	
	#endregion

}
