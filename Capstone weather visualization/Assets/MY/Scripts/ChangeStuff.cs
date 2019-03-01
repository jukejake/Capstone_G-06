/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
//This is Demo code and needs to be redone.
////*/

using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class ChangeStuff : SerializedMonoBehaviour {

	#region Setup
	//Used for initialization.
	private void Start () {
	   InvokeRepeating("DelayedUpdate", 1.0f, 0.10f);
	}
	//Create a slower Update.
	private void DelayedUpdate() {
		if (ToggleRotation) { SetRotation(); }
		if (ToggleNozzle) { ChangeNozzleSize(); }
	}
	//Cancel the Invoke when the object is destroyed.
	private void OnDestroy() { CancelInvoke("DelayedUpdate"); }

	#endregion

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

	#region Set Rotation

	[ToggleGroup("ToggleRotation")]
	public bool ToggleRotation = false;
	[ToggleGroup("ToggleRotation")]
	public GameObject Obj;
	[ToggleGroup("ToggleRotation")]
	public float RotateBy = 1.0f;
	private bool RotateLeft = false;
	private bool RotateRight = false;
	private float RotateY = 0.0f;
	private Vector3 Rotation;
	//Public function to switch the object to rotate.
	public void SetObjToRot(GameObject obj) { Obj = obj; }
	//Toggle turning left on.
	public void RotationLeft(bool mode) { RotateLeft = mode; }
	//Toggle turning right on.
	public void RotationRight(bool mode) { RotateRight = mode; }
	//Will rotate on DelayedUpdate, if ToggleRotation.
	private void SetRotation() {
		//If none are set to rotate, leave.
		if (!RotateLeft && !RotateRight) { return; }
		//Rotate Left
		if (RotateLeft) { RotateY -= RotateBy; }
		//Rotate Right
		if (RotateRight) { RotateY += RotateBy; }
		//Set Rotation
		Rotation = new Vector3(0.0f, RotateY, 0.0f);
		Obj.transform.localRotation = Quaternion.Euler(Rotation);
	}
	#endregion

	#region Set Nozzle
	[ToggleGroup("ToggleNozzle")]
	public bool ToggleNozzle = false;
	[ToggleGroup("ToggleNozzle")]
	public GameObject FrontWall;
	[ToggleGroup("ToggleNozzle")]
	public GameObject BackWall;
	[ToggleGroup("ToggleNozzle")]
	public GameObject Canopy;
	[ToggleGroup("ToggleNozzle")]
	public GameObject SmokeFlow;
	[ToggleGroup("ToggleNozzle")]
	public GameObject RainFlow;
	[ToggleGroup("ToggleNozzle")]
	public GameObject SnowFlow;
	[ToggleGroup("ToggleNozzle")]
	public GameObject FlowPlane;
	[ToggleGroup("ToggleNozzle")]
	public int State = 0;
	[ToggleGroup("ToggleNozzle")]
	//Function to set the state of the Nozzle based on a slider.
	public void SetNozzle(Slider UI) { State = (int)UI.value; }
	//Will rotate on DelayedUpdate, if ToggleNozzle.
	private void ChangeNozzleSize() {

		//Set the position of the walls, and flow.
		//Change the scale of the flow.
		var _pss1 = SmokeFlow.GetComponent<ParticleSystem>().shape;
		var _pss2 =  RainFlow.GetComponent<ParticleSystem>().shape;
		var _pss3 =  SnowFlow.GetComponent<ParticleSystem>().shape;
		//(7.0m^2) = Size of the nozzle opening.
		if (State == 0) {
			FrontWall.transform.localPosition = new Vector3(0, 1.45f, -1.2f);
			BackWall.transform.localPosition  = new Vector3(0, 1.45f,  1.2f);
			Canopy.transform.localPosition    = new Vector3(0, 2.9f, 0);

			SmokeFlow.transform.localPosition = new Vector3(0, 1.4f, 0);
			 RainFlow.transform.localPosition = new Vector3(0, 1.4f, 0);
			 SnowFlow.transform.localPosition = new Vector3(0, 1.4f, 0);
			_pss1.scale = new Vector3(2, 2.7f, 1.0f);
			_pss2.scale = new Vector3(2, 2.7f, 1.0f);
			_pss3.scale = new Vector3(2, 2.7f, 1.0f);

			FlowPlane.transform.localPosition = new Vector3(0, 1.4f, 0);
			FlowPlane.transform.localScale = new Vector3(0.2f, 1, 0.27f);
		}
		//(13.0m^2) = Size of the nozzle opening.
		else if (State == 1) { 
			FrontWall.transform.localPosition = new Vector3(0, 1.45f, -2.25f);
			BackWall.transform.localPosition  = new Vector3(0, 1.45f,  2.25f);
			Canopy.transform.localPosition    = new Vector3(0, 2.9f, 0);

			SmokeFlow.transform.localPosition = new Vector3(0, 1.4f, 0);
			 RainFlow.transform.localPosition = new Vector3(0, 1.4f, 0);
			 SnowFlow.transform.localPosition = new Vector3(0, 1.4f, 0);
			_pss1.scale = new Vector3(4.25f, 2.7f, 1.0f);
			_pss2.scale = new Vector3(4.25f, 2.7f, 1.0f);
			_pss3.scale = new Vector3(4.25f, 2.7f, 1.0f);

			FlowPlane.transform.localPosition = new Vector3(0, 1.4f, 0);
			FlowPlane.transform.localScale = new Vector3(0.425f, 1, 0.27f);
		}
		////(14.5m^2) = Size of the nozzle opening.
		//else if (State == 2) {
		//    FrontWall.transform.localPosition = new Vector3(0, 1.45f, -2.5f);
		//    BackWall.transform.localPosition  = new Vector3(0, 1.45f,  2.5f);
		//    Canopy.transform.localPosition    = new Vector3(0, 2.9f, 0);
		//}
		//(22.0m^2) = Size of the nozzle opening.
		else if (State == 2) {
			FrontWall.transform.localPosition = new Vector3(0, 1.45f, -2.5f);
			BackWall.transform.localPosition  = new Vector3(0, 1.45f,  2.5f);
			Canopy.transform.localPosition    = new Vector3(0, 4.4f, 0);

			SmokeFlow.transform.localPosition = new Vector3(0, 2.16f, 0);
			 RainFlow.transform.localPosition = new Vector3(0, 2.16f, 0);
			 SnowFlow.transform.localPosition = new Vector3(0, 2.16f, 0);
			_pss1.scale = new Vector3(4.75f, 4.25f, 1.0f);
			_pss2.scale = new Vector3(4.75f, 4.25f, 1.0f);
			_pss3.scale = new Vector3(4.75f, 4.25f, 1.0f);

			FlowPlane.transform.localPosition = new Vector3(0, 2.16f, 0);
			FlowPlane.transform.localScale = new Vector3(0.475f, 1, 0.425f);
		}
	}
	#endregion
}
