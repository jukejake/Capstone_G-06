using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class OrderButtons : SerializedMonoBehaviour {

	[ValueDropdown("myValues")]
	public int Axis;
	private ValueDropdownList<int> myValues = new ValueDropdownList<int>() { { "x", 0 },{ "y", 1 },{ "z", 2 } };// The selectable values for the dropdown.

	public float StartValue;
	public float IncreaseValue;

	[Button]
	public void Order() {
		int i = 0;
		if (Axis == 0) {
			foreach (Transform child in transform) {
				var pos = child.localPosition;
				child.localPosition = new Vector3((StartValue + (IncreaseValue * i)), pos.y, pos.z);
				i++;
			}
		}
		if (Axis == 1) {
			foreach (Transform child in transform) {
				var pos = child.localPosition;
				child.localPosition = new Vector3(pos.x, (StartValue + (IncreaseValue * i)), pos.z);
				i++;
			}
		}
		if (Axis == 2) {
			foreach (Transform child in transform) {
				var pos = child.localPosition;
				child.localPosition = new Vector3(pos.x, pos.y, (StartValue + (IncreaseValue * i)));
				i++;
			}
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
