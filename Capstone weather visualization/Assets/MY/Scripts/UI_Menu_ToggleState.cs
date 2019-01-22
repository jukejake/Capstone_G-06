/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;

public class UI_Menu_ToggleState : MonoBehaviour {
	public UI_Menu_Models UI_Menu;
	private bool ToggleOn = false;
	//Function to toggle the state of the UI_Menu
	public void Toggle() {
		ToggleOn = !ToggleOn;
		this.gameObject.SetActive(ToggleOn);
		UI_Menu.Resize();
	}

}
