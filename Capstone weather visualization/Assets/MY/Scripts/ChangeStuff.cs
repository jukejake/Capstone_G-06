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
    public GameObject TopWall;
    [ToggleGroup("ToggleNozzle")]
    public float Movement = 10.0f;
    [ToggleGroup("ToggleNozzle")]
    public void SetNozzle(Slider UI) { Movement = UI.value; }
    private void ChangeNozzleSize() {
        FrontWall.transform.localPosition = new Vector3(0, 5,-0.5f-Movement);
        BackWall.transform.localPosition  = new Vector3(0, 5, 0.5f+Movement);
        TopWall.transform.localPosition   = new Vector3(0, 5.5f+Movement, 0);
    }
    #endregion
}
