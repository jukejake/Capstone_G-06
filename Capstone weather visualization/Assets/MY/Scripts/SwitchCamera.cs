/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//Updated: January 2020
//BUSI 4995U Capstone
////*/

using UnityEngine;
using UnityEngine.UI;

public class SwitchCamera : MonoBehaviour {

    #region Variables
    public static SwitchCamera instance = null;

    public RenderTexture MainCamera;
	public RenderTexture SecondCamera;
	private RawImage ImageView;
    [HideInInspector]
	public int CameraStat = 0;
    public Transform MainC;
    public Transform SecondC;
	#endregion

	#region Functions
	private void Awake () {
        instance = this;
        ImageView = GetComponent<RawImage>();

    }
	//Function to switch the cameras view.
	public void Switch() {
		switch (CameraStat) {
			//Switching from main camera to the second camera
			case 0:
				CameraStat += 1;
				ImageView.texture = MainCamera;
                Camera.main.gameObject.transform.position = SecondC.position;
				Camera.main.gameObject.transform.rotation = SecondC.rotation;
                Camera.main.fieldOfView = SecondC.GetComponent<Camera>().fieldOfView;
				break;
			//Switching from second camera to the main camera
			case 1:
				CameraStat = 0;
				ImageView.texture = SecondCamera;
				Camera.main.gameObject.transform.position = MainC.position;
                Camera.main.gameObject.transform.rotation = MainC.rotation;
                Camera.main.fieldOfView = MainC.GetComponent<Camera>().fieldOfView;
                break;
			default:
				break;
		}
	}
	#endregion
}
