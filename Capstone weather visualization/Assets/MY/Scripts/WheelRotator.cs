using UnityEngine;
using Sirenix.OdinInspector;

public class WheelRotator : SerializedMonoBehaviour {

	#region Variables
	WheelController WC;
	private float RotateAmount;
	public Transform[] NegativeRot;
	public Transform[] PositiveRot;
	public bool RotY = false;
	public bool UseOwnSize = false;
	[ShowIf("UseOwnSize")]
	public float WheelSizeInMeters = 16.4592f; //12.192f~16.4592f //40~54 feet to meters

	#endregion

	#region Setup
	// Use this for initialization
	void Start () {
		WC = FindObjectOfType<WheelController>();
		if (UseOwnSize) { RotateAmount = (WC.Speed / WheelSizeInMeters); }
		else { RotateAmount = WC.RotateAmount; }
		InvokeRepeating("DelayedUpdate", 1.0f, 0.50f);
	}
	
	void DelayedUpdate() {
		if (UseOwnSize) { RotateAmount = (WC.Speed / WheelSizeInMeters); }
		else { RotateAmount = WC.RotateAmount; }
	}

	private void FixedUpdate() {
		if (RotY){
			foreach (var wheel in NegativeRot) {
				wheel.Rotate(Vector3.down, RotateAmount);
			}
			foreach (var wheel in PositiveRot) {
				wheel.Rotate(Vector3.up, RotateAmount);
			}
		}
		else {
			foreach (var wheel in NegativeRot) {
				wheel.Rotate(Vector3.left, RotateAmount);
			}
			foreach (var wheel in PositiveRot) {
				wheel.Rotate(Vector3.right, RotateAmount);
			}
		}
		
	}


	#endregion
}
