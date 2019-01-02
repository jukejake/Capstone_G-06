using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class ChangeStuff : MonoBehaviour {

	#region Setup
	// Use this for initialization
	private void Start () {
	   InvokeRepeating("DelayedUpdate", 1.0f, 0.10f);
	}
	private void DelayedUpdate() {
		if (ToggleRotation) { SetRotation(); }
		if (ToggleNozzle) { ChangeNozzleSize(); }
	}
	#endregion

	#region Set Scale
	private float OldScale = 1.0f;
	private float NewScale = 1.0f;

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

	public void SetObjToRot(GameObject obj) { Obj = obj; }
	public void RotationLeft(bool mode) { RotateLeft = mode; }
	public void RotationRight(bool mode) { RotateRight = mode; }
	private void SetRotation() {
		if (!RotateLeft && !RotateRight) { return; }
		if (RotateLeft) { RotateY -= RotateBy; }
		if (RotateRight) { RotateY += RotateBy; }
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
	public GameObject Flow;
	[ToggleGroup("ToggleNozzle")]
	public GameObject FlowPlane;
	[ToggleGroup("ToggleNozzle")]
	public int State = 0;
	[ToggleGroup("ToggleNozzle")]
	public void SetNozzle(Slider UI) { State = (int)UI.value; }
	private void ChangeNozzleSize() {

		var _pss = Flow.GetComponent<ParticleSystem>().shape;
		//7.0m^2
		if (State == 0) {
			FrontWall.transform.localPosition = new Vector3(0, 1.45f, -1.2f);
			BackWall.transform.localPosition  = new Vector3(0, 1.45f,  1.2f);
			Canopy.transform.localPosition    = new Vector3(0, 2.9f, 0);

			Flow.transform.localPosition = new Vector3(0, 1.4f, 0);
			_pss.scale = new Vector3(2, 2.7f, 1.0f);
			FlowPlane.transform.localPosition = new Vector3(0, 0, 0);
			FlowPlane.transform.localScale = new Vector3(0.2f, 1, 0.27f);
		}
		//13.0m^2
		else if (State == 1) { 
			FrontWall.transform.localPosition = new Vector3(0, 1.45f, -2.25f);
			BackWall.transform.localPosition  = new Vector3(0, 1.45f,  2.25f);
			Canopy.transform.localPosition    = new Vector3(0, 2.9f, 0);

			Flow.transform.localPosition = new Vector3(0, 1.4f, 0);
			_pss.scale = new Vector3(4.25f, 2.7f, 1.0f);
			FlowPlane.transform.localPosition = new Vector3(0, 0, 0);
			FlowPlane.transform.localScale = new Vector3(0.425f, 1, 0.27f);
		}
		////14.5m^2
		//else if (State == 2) {
		//    FrontWall.transform.localPosition = new Vector3(0, 1.45f, -2.5f);
		//    BackWall.transform.localPosition  = new Vector3(0, 1.45f,  2.5f);
		//    Canopy.transform.localPosition    = new Vector3(0, 2.9f, 0);
		//}
		//22.0m^2
		else if (State == 2) {
			FrontWall.transform.localPosition = new Vector3(0, 1.45f, -2.5f);
			BackWall.transform.localPosition  = new Vector3(0, 1.45f,  2.5f);
			Canopy.transform.localPosition    = new Vector3(0, 4.4f, 0);

			Flow.transform.localPosition = new Vector3(0, 2.16f, 0);
			_pss.scale = new Vector3(4.75f, 4.25f, 1.0f);
			FlowPlane.transform.localPosition = new Vector3(0, 0, 0);
			FlowPlane.transform.localScale = new Vector3(0.475f, 1, 0.425f);
		}
	}
	#endregion
}
