/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MovableLightingArray : SerializedMonoBehaviour {

	#region Variables
	public float MoveSpeed = 1.0f;
	public float RotSpeed = 10.0f;
	[InfoBox("Rot Cap is expected to be between -90 and 90.")]
	[InfoBox("Might need to ONLY have one Rotation active at a time.")]
	[InfoBox("Movement Cap Y is expected to be negative.")]
	public Dictionary<string, LightingState> LightingTable = new Dictionary<string, LightingState>();
	#endregion

	#region Public Functions 
	public void Select_Light(string key) {
		LightingState t;
		if (LightingTable.TryGetValue(key, out t)) { t.Selected = true; }
	}

	public void Move_All_Lights(int _d)		 { MoveDirection(_d); } //0 = Up, 1 = Down, 2 = Inward, 3 = Outward.
	public void Rotate_All_Lights(bool _r)	 { RotateDirection(_r); } //True = Inwards, False = Outwards.

	public void Move_Front_Lights(int _d)	 { MoveDirection(_d); } //0 = Up, 1 = Down, 2 = Inward, 3 = Outward.
	public void Move_Left_Lights(int _d)	 { MoveDirection(_d); } //0 = Up, 1 = Down, 2 = Inward, 3 = Outward.
	public void Move_Middle_Lights(int _d)	 { MoveDirection(_d); } //0 = Up, 1 = Down, 2 = Inward, 3 = Outward.
	public void Move_Right_Lights(int _d)	 { MoveDirection(_d); } //0 = Up, 1 = Down, 2 = Inward, 3 = Outward.
	public void Move_Back_Lights(int _d)	 { MoveDirection(_d); } //0 = Up, 1 = Down, 2 = Inward, 3 = Outward.

	public void Rotate_Front_Lights(bool _r) { RotateDirection(_r); } //True = Inwards, False = Outwards.
	public void Rotate_Left_Lights(bool _r)	 { RotateDirection(_r); } //True = Inwards, False = Outwards.
	public void Rotate_Right_Lights(bool _r) { RotateDirection(_r); } //True = Inwards, False = Outwards.
	public void Rotate_Back_Lights(bool _r)	 { RotateDirection(_r); } //True = Inwards, False = Outwards.
	#endregion

	#region Private Functions
	[Button]
	private void MoveDirection(int _d) {
		float deltaT = (Time.deltaTime * MoveSpeed);
		//Go through all the lights to see which ones are selected.
		foreach (var light in LightingTable) {
			//Which ever light is selected, compute the movements on.
			if (light.Value.Selected) {
				var Local = light.Value.Light.transform.localPosition;
				switch (_d) {
					//Up
					case 0:
						if (Local.y < 0) {
							light.Value.Light.transform.Translate(0, deltaT, 0);
						}
						if (Local.y > 0) {
							light.Value.Light.transform.localPosition = new Vector3(Local.x, 0, Local.z);
						}
						break;
					//Down
					case 1:
						if (Local.y > light.Value.MovementCap.y) {
							light.Value.Light.transform.Translate(0, -deltaT, 0);
						}
						if (Local.y < light.Value.MovementCap.y) {
							light.Value.Light.transform.localPosition = new Vector3(Local.x, light.Value.MovementCap.y, Local.z);
						}
						break;
					//Inward
					case 2:
						///X-axis
						//Positive
						if (light.Value.MovementCap.x > 0) { 
							if (Local.x > 0) {
								light.Value.Light.transform.Translate(-deltaT, 0, 0);
							}
							if (Local.x < 0) {
								light.Value.Light.transform.localPosition = new Vector3(0, Local.y, Local.z);
							}
						}
						//Negative
						else if (light.Value.MovementCap.x < 0){
							if (Local.x < 0) {
								light.Value.Light.transform.Translate(deltaT, 0, 0);
							}
							if (Local.x > 0) {
								light.Value.Light.transform.localPosition = new Vector3(0, Local.y, Local.z);
							}
						}
						///Z-axis
						//Positive
						if (light.Value.MovementCap.z > 0) { 
							if (Local.z > 0) {
								light.Value.Light.transform.Translate(0, 0, -deltaT);
							}
							if (Local.z < 0) {
								light.Value.Light.transform.localPosition = new Vector3(Local.x, Local.y, 0);
							}
						}
						//Negative
						else if (light.Value.MovementCap.z < 0){
							if (Local.z < 0) {
								light.Value.Light.transform.Translate(0, 0, deltaT);
							}
							if (Local.z > 0) {
								light.Value.Light.transform.localPosition = new Vector3(Local.x, Local.y, 0);
							}
						}
						break;
					//Outward
					case 3:
						///X-axis
						//Positive
						if (light.Value.MovementCap.x > 0) { 
							if (Local.x < light.Value.MovementCap.x) {
								light.Value.Light.transform.Translate(deltaT, 0, 0);
							}
							if (Local.x > light.Value.MovementCap.x) {
								light.Value.Light.transform.localPosition = new Vector3(light.Value.MovementCap.x, Local.y, Local.z);
							}
						}
						//Negative
						else if (light.Value.MovementCap.x < 0){
							if (Local.x > light.Value.MovementCap.x) {
								light.Value.Light.transform.Translate(-deltaT, 0, 0);
							}
							if (Local.x < light.Value.MovementCap.x) {
								light.Value.Light.transform.localPosition = new Vector3(light.Value.MovementCap.x, Local.y, Local.z);
							}
						}
						///Z-axis
						//Positive
						if (light.Value.MovementCap.z > 0) { 
							if (Local.z < light.Value.MovementCap.z) {
								light.Value.Light.transform.Translate(0, 0, deltaT);
							}
							if (Local.z > light.Value.MovementCap.z) {
								light.Value.Light.transform.localPosition = new Vector3(Local.x, Local.y, light.Value.MovementCap.z);
							}
						}
						//Negative
						else if (light.Value.MovementCap.z < 0){
							if (Local.z > light.Value.MovementCap.z) {
								light.Value.Light.transform.Translate(0, 0, -deltaT);
							}
							if (Local.z < light.Value.MovementCap.z) {
								light.Value.Light.transform.localPosition = new Vector3(Local.x, Local.y, light.Value.MovementCap.z);
							}
						}
						break;
					default: break;
				}
			}
		}
	}
	[Button]
	private void RotateDirection(bool _r) {
		float deltaT = (Time.deltaTime * RotSpeed);
		//Go through all the lights to see which ones are selected.
		foreach (var light in LightingTable) {
			var Local = light.Value.Light.transform.localRotation.eulerAngles;
			//Which ever light is selected, compute the Inwards rotation on.
			if (light.Value.Selected && _r) {
				///X axis
				//Positive
				if (light.Value.RotCap.x > 0) {
					if (Local.x > 0 && Local.x <= (360 - light.Value.RotCap.x)) {
						light.Value.Light.transform.Rotate(-deltaT, 0, 0);
					}
					if (Local.x < 0 || Local.x > (360 - light.Value.RotCap.x)) {
						light.Value.Light.transform.localRotation = Quaternion.Euler(0, 0, Local.z);
					}
				}
				//Negative
				else if (light.Value.RotCap.x < 0) {
					if (Local.x < 0 || Local.x >= (360 + light.Value.RotCap.x)) {
						light.Value.Light.transform.Rotate(deltaT, 0, 0);
					}
					if (Local.x > 0 && Local.x < (360 + light.Value.RotCap.x)) {
						light.Value.Light.transform.localRotation = Quaternion.Euler(0, 0, Local.z);
					}
				}
				///Z axis
				//Positive
				if (light.Value.RotCap.y > 0) {
					if (Local.z > 0 && Local.z <= (360 - light.Value.RotCap.y)) {
						light.Value.Light.transform.Rotate(0, 0, -deltaT);
					}
					if (Local.z < 0 || Local.z > (360 - light.Value.RotCap.y)) {
						light.Value.Light.transform.localRotation = Quaternion.Euler(Local.x, 0, 0);
					}
				}
				//Negative
				else if (light.Value.RotCap.y < 0) {
					if (Local.y < 0 || Local.z >= (360 + light.Value.RotCap.y)) {
						light.Value.Light.transform.Rotate(0, 0, deltaT);
					}
					if (Local.y > 0 && Local.z < (360 + light.Value.RotCap.y)) {
						light.Value.Light.transform.localRotation = Quaternion.Euler(Local.x, 0, 0);
					}
				}
			}
			//Which ever light is selected, compute the Outwards rotation on.
			else if (light.Value.Selected && !_r) {
				///X axis
				//Positive
				if (light.Value.RotCap.x > 0) {
					if (Local.x < light.Value.RotCap.x) {
						light.Value.Light.transform.Rotate(deltaT, 0, 0);
					}
					if (Local.x > light.Value.RotCap.x) { // && Local.x < (360-light.Value.RotCap.x)
						light.Value.Light.transform.localRotation = Quaternion.Euler(light.Value.RotCap.x, 0, Local.z);
					}
				}
				//Negative
				//For some reason it switches it's Z axis sometimes but wont display that it did until it flips.
				else if (light.Value.RotCap.x < 0) {
					if (Local.x == 0 || Local.x > (360+light.Value.RotCap.x)) { // Local.x > light.Value.RotCap.x && 
						light.Value.Light.transform.Rotate(-deltaT, 0, 0);
					}
					if (Local.x != 0 && Local.x < (360+light.Value.RotCap.x)) { //Local.x > light.Value.RotCap.x && 
						light.Value.Light.transform.localRotation = Quaternion.Euler(light.Value.RotCap.x, 0, Local.z);
					}
				}
				///Z axis
				//Positive
				if (light.Value.RotCap.y > 0) { 
					if (Local.z < light.Value.RotCap.y) {
						light.Value.Light.transform.Rotate(0, 0, deltaT);
					}
					if (Local.z > light.Value.RotCap.y) {
						light.Value.Light.transform.localRotation = Quaternion.Euler(Local.x, 0, light.Value.RotCap.y);
					}
				}
				//Negative
				else if (light.Value.RotCap.y < 0) {
					if (Local.z == 0 || Local.z > (360+light.Value.RotCap.y)) {
						light.Value.Light.transform.Rotate(0, 0, -deltaT);
					}
					if (Local.z != 0 && Local.z < (360+light.Value.RotCap.y)) {
						light.Value.Light.transform.localRotation = Quaternion.Euler(Local.x, 0, light.Value.RotCap.y);
					}
				}
			}
		}
	}

	public struct LightingState {
		public bool Selected;
		public GameObject Light;
		public Vector2 RotCap;
		public Vector3 MovementCap;
	}
	#endregion
}
