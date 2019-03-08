/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;
using UnityEngine.UI;

public class UI_Menu_Models : MonoBehaviour {

	#region Variables
	private bool ToggleOn = false;
	public void ToggleState() { ToggleOn = !ToggleOn; }

	public GameObject[] ButtonObjects;
	public GameObject[] gameObjects;
	public int YposFactor = 40;
	public int YposFactor2 = 200;
	#endregion

	#region Functions
	//Function to toggle just the button visibility
	public void ToggleButtonVisibility() {
		foreach (var item in ButtonObjects) {
			item.SetActive(ToggleOn);
		}
	}
	//Function to toggle the visibility of everything
	public void ToggleAllVisibility() {
		foreach (var item in gameObjects) {
			item.SetActive(ToggleOn);
		}
	}
	//Resize/Re-order the position of the objects in the UI
	public void Resize() {
		int num = 0;
		int Ypos = 0;
		foreach (var item in gameObjects) {
			num += 1;
			if (item.activeSelf) {
				if (num % 2 == 0) { Ypos -= YposFactor2; }
				else if (num % 2 == 1) { Ypos -= YposFactor; item.GetComponent<RectTransform>().localPosition = new Vector3(0, Ypos, 0); }
			}
		}
	}
	#endregion
}
