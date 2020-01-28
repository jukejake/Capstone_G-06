/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//Updated: January 2020
//BUSI 4995U Capstone
//
//This is Demo code and needs to be redone.
//Removed Nozzle settings to ChangeNozzle
//Removed Dyno settings to ChangeDyno
////*/

using UnityEngine;
using UnityEngine.UI;

public class ChangeStuff : MonoBehaviour {


	#region Set Scale
	private float OldScale = 1.0f;
	private float NewScale = 1.0f;
	//Sets the local scale of an object based on a UI slider.
	public void SetScale(GameObject obj) {
		NewScale = this.GetComponent<Slider>().value;
		if (OldScale != NewScale) {
			OldScale = NewScale;
			obj.transform.localScale = new Vector3(NewScale, NewScale, NewScale);
		}
	}
	#endregion

	#region Set Toggle
	private bool OldToggleState = true;
	private bool NewToggleState = true;
	//Toggles the state of an object bassed on a UI toggle
	public void SetToggle(GameObject obj) {
		NewToggleState = this.GetComponent<Toggle>().isOn;
		if (OldToggleState != NewToggleState) {
			OldToggleState = NewToggleState; 
			obj.SetActive(NewToggleState);
		}
	}
    #endregion

    //Removed Nozzle settings to ChangeNozzle

    //Removed Dyno settings to ChangeDyno

}
