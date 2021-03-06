﻿/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;
using Sirenix.OdinInspector;

public class OrderButtons : SerializedMonoBehaviour {

	[ValueDropdown("myValues")]
	public int Axis;
	//The selectable values for the dropdown.
	//Will display x, y, and z in a dropdown.
	private ValueDropdownList<int> myValues = new ValueDropdownList<int>() { { "x", 0 },{ "y", 1 },{ "z", 2 } };

	//The start value that the first UI element will begin at.
	public float StartValue;
	//The value that all other UI elements will increase by.
	public float IncreaseValue;

	[Button]
	//Function re-order the UI in all the children
	public void Order() {
		int i = 0;
		switch (Axis){
			 //X axis
			case 0:
				foreach (Transform child in transform) {
					var pos = child.localPosition;
					child.localPosition = new Vector3((StartValue + (IncreaseValue * i)), pos.y, pos.z);
					i++;
				}
				break;
			//Y axis
			case 1:
				foreach (Transform child in transform) {
					var pos = child.localPosition;
					child.localPosition = new Vector3(pos.x, (StartValue + (IncreaseValue * i)), pos.z);
					i++;
				}
				this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, i * Mathf.Abs(IncreaseValue));
				break;
			//Z axis
			case 2:
				foreach (Transform child in transform) {
					var pos = child.localPosition;
					child.localPosition = new Vector3(pos.x, pos.y, (StartValue + (IncreaseValue * i)));
					i++;
				}
				break;
			default: break;
		}

		//foreach (Transform child in transform) {
		//	var pos = child.localPosition;
		//	if (Axis == 0) { child.localPosition = new Vector3((StartValue + (IncreaseValue * i)), pos.y, pos.z); }
		//	if (Axis == 1) { child.localPosition = new Vector3(pos.x, (StartValue + (IncreaseValue * i)), pos.z); }
		//	if (Axis == 2) { child.localPosition = new Vector3(pos.x, pos.y, (StartValue + (IncreaseValue * i))); }
		//	i++;
		//}
	}
}
