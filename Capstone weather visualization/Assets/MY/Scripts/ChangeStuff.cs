using UnityEngine;
using UnityEngine.UI;

public class ChangeStuff : MonoBehaviour {

	#region Setup
	// Use this for initialization
	private void Start () {
       InvokeRepeating("DelayedUpdate", 1.0f, 0.10f);
    }
    private void DelayedUpdate() {
		SetRotation();
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
	private bool RotateLeft = false;
	private bool RotateRight = false;
	public float RotateBy = 1.0f;
	private float RotateY = 0.0f;
	private Vector3 Rotation;
	public GameObject Obj;

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
}
