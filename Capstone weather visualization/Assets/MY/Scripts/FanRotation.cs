/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;

public class FanRotation : MonoBehaviour {
	#region Variables

	public int Speed = 125;
	private float WheelSize = 4; //In meters
	private float RotateAmount;
	#endregion

	#region Functions
	private void Start () {
		RotateAmount = (Speed / (WheelSize * Mathf.PI));
	}
	
	private void Update () {
		this.transform.Rotate(Vector3.forward, RotateAmount);
	}
	#endregion
}
