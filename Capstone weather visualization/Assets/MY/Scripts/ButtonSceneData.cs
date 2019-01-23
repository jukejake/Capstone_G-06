/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneData : MonoBehaviour {

	#region Variables
	//A string for the scene name that the ChangeScene function will change to.
	public string SceneName = "Default";
	public GameObject[] HideWhenPressed;
	public GameObject[] ShowWhenPressed;
	#endregion

	#region Functions
	//Function to change the unity scene
	public void ChangeScene() {
		SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
	}
	//Function to hide and show parts of the scene.
	public void SwitchScene() {
		foreach (var item in HideWhenPressed) {
			item.SetActive(false);
		}
		foreach (var item in ShowWhenPressed) {
			item.SetActive(true);
		}
	}
	#endregion
}
