using UnityEngine;
using UnityEngine.UI;

public class UI_Menu_Models : MonoBehaviour {

	#region Setup
	void Start () {
       InvokeRepeating("DelayedUpdate", 1.0f, 0.20f);
	}
	private void DelayedUpdate() {

	}
	#endregion

	#region Variables
	private bool ToggleOn = false;
	public void ToggleState() { ToggleOn = !ToggleOn; }

	public GameObject[] ButtonObjects;
	public GameObject[] gameObjects;
	#endregion

	#region Functions
	public void ToggleButtonVisibility() {
		foreach (var item in ButtonObjects) {
			item.SetActive(ToggleOn);
		}
	}
	public void ToggleAllVisibility() {
		foreach (var item in gameObjects) {
			item.SetActive(ToggleOn);
		}
	}
	public void Resize() {
		int num = 0;
		int Ypos = 200;
		foreach (var item in gameObjects) {
			num += 1;
			if (item.activeSelf) {
				if (num % 2 == 0) { Ypos -= 200; }
				else if (num % 2 == 1) { Ypos -= 30; item.GetComponent<RectTransform>().localPosition = new Vector3(0, Ypos, 0); }
				
			}
		}
	}
	#endregion
}
