using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneData : MonoBehaviour {

	#region Variables
	public string SceneName = "Default";
	#endregion

	#region Functions
	public void ChangeScene() {
		SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
	}
	#endregion
}
