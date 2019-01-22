/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;
using Sirenix.OdinInspector;

public class WheelRotator : SerializedMonoBehaviour {

	#region Variables
	private WheelController WC;
	private float RotateAmount;
	public Transform[] NegativeRot;
	public Transform[] PositiveRot;
	public bool RotY = false;
	public bool UseOwnSize = false;
	[ShowIf("UseOwnSize")]
	public float WheelSizeInMeters = 16.4592f; //12.192f~16.4592f //40~54 feet to meters

	#endregion

	#region Setup
	//Used for initialization.
	void Start () {
		WC = FindObjectOfType<WheelController>();
		if (UseOwnSize) { RotateAmount = (WC.Speed / (WheelSizeInMeters*Mathf.PI)); }
		else { RotateAmount = WC.RotateAmount; }
		InvokeRepeating("DelayedUpdate", 1.0f, 0.50f);
	}
	//Create a slower Update.
	void DelayedUpdate() {
		if (UseOwnSize) { RotateAmount = (WC.Speed / (WheelSizeInMeters*Mathf.PI)); }
		else { RotateAmount = WC.RotateAmount; }
	}
	//Cancel the Invoke when the object is destroyed.
	private void OnDestroy() { CancelInvoke("DelayedUpdate"); }

	private void FixedUpdate() {
		//Rotate on the Y axis
		if (RotY) {
			//Negative rotation
			foreach (var wheel in NegativeRot) {
				wheel.Rotate(Vector3.down, RotateAmount);
			}
			//Positive rotation
			foreach (var wheel in PositiveRot) {
				wheel.Rotate(Vector3.up, RotateAmount);
			}
		}
		//Rotate on the X & Z axis
		else {
			//Negative rotation
			foreach (var wheel in NegativeRot) {
				wheel.Rotate(Vector3.left, RotateAmount);
			}
			//Positive rotation
			foreach (var wheel in PositiveRot) {
				wheel.Rotate(Vector3.right, RotateAmount);
			}
		}
		
	}


	#endregion
}
