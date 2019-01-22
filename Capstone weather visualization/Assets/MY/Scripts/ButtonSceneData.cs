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
	#endregion

	#region Functions
	//Function to change the scene
	public void ChangeScene() {
		SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
	}
	#endregion
}
