using UnityEngine;


public class ToggleState : MonoBehaviour {
	public UI_Menu_Models UI_Menu;
	private bool ToggleOn = false;
	public void Toggle() {
		ToggleOn = !ToggleOn;
		this.gameObject.SetActive(ToggleOn);
		UI_Menu.Resize();
	}

}
