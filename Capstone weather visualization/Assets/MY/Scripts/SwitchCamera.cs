/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;
using UnityEngine.UI;

public class SwitchCamera : MonoBehaviour {

	#region Variables
	public RenderTexture MainCamera;
	public RenderTexture SecondCamera;
	private RawImage ImageView;
	private int CameraStat = 0;

	private Vector3 MCPos;
	private Quaternion MCRot;
	private Vector3 TCPos;
	private Quaternion TCRot;
	private float TCFov;
	#endregion

	#region Functions
	private void Awake () {
		//Find all the cameras and note the position and rotation.
		ImageView = GetComponent<RawImage>();
		MCPos = GameObject.Find("Cameras/Front Camera").transform.localPosition;
		MCRot = GameObject.Find("Cameras/Front Camera").transform.localRotation;
		TCPos = GameObject.Find("Cameras/Top View Camera").transform.localPosition;
		TCRot = GameObject.Find("Cameras/Top View Camera").transform.localRotation;
		TCFov = GameObject.Find("Cameras/Top View Camera").GetComponent<Camera>().fieldOfView;
	}
	//Function to switch the cameras view.
	public void Switch() {
		switch (CameraStat) {
			//Switching from main camera to the second camera
			case 0:
				CameraStat += 1;
				ImageView.texture = MainCamera;
				Camera.main.gameObject.transform.localPosition = TCPos;
				Camera.main.gameObject.transform.localRotation = TCRot;
				Camera.main.fieldOfView = TCFov;
				Camera.main.gameObject.GetComponent<PanZoom>().enabled = false;
				break;
			//Switching from second camera to the main camera
			case 1:
				CameraStat = 0;
				ImageView.texture = SecondCamera;
				Camera.main.gameObject.transform.localPosition = MCPos;
				Camera.main.gameObject.transform.localRotation = MCRot;
				Camera.main.gameObject.GetComponent<PanZoom>().enabled = true;
				break;
			default:
				break;
		}
	}
	#endregion
}
