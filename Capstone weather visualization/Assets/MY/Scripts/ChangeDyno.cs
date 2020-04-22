/*////
//Written by Jacob Rosengren
//Date: January 2020
//BUSI 4995U Capstone
////*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class ChangeDyno : SerializedMonoBehaviour {

    #region Variables

    public static ChangeDyno instance = null;
    
    [BoxGroup("ToggleRotation")]
    public GameObject Obj;
    [BoxGroup("ToggleRotation")]
    public float RotateBy = 1.0f;
    private float RotateByHidden = 1.0f;
    private bool RotateLeft = false;
    private bool RotateRight = false;
    private float RotateY = 0.0f;
    private Vector3 Rotation;

    public bool Display = false;
    [ShowIf(memberName: "Display")]
    public TextMeshProUGUI DisplayText;

    #endregion

    #region Functions

    private void Awake() {
        instance = this;
    }
    private void Start() {
        RotateByHidden = RotateBy;
    }

    private void FixedUpdate() {
        //If none are set to rotate, leave.
        if (!RotateLeft && !RotateRight) { return; }
        //Rotate Left
        if (RotateLeft) { RotateY -= RotateBy; }
        //Rotate Right
        if (RotateRight) { RotateY += RotateBy; }
        //Set Rotation
        Rotation = new Vector3(0.0f, RotateY, 0.0f);
        Obj.transform.localRotation = Quaternion.Euler(Rotation);
        //Set text to the rotation amount.
        if (DisplayText) {         
            //Make sure that 'RotateY' is between 0 and 360
            //this is only to make puting it into text easier
            if (RotateY < 0) { RotateY += 360; }
            if (RotateY > 359) { RotateY -= 360; }
            DisplayText.text = RotateY.ToString();
        }
    }
    //Public function to switch the object to rotate.
    public void SetObjToRot(GameObject obj) { Obj = obj; }
	//Toggle turning left on.
	public void RotationLeft(bool mode) { RotateLeft = mode; }
	//Toggle turning right on.
	public void RotationRight(bool mode) { RotateRight = mode; }
    //Reset
    public void ResetRotation() {
        Rotation = Vector3.zero;
        Obj.transform.localRotation = Quaternion.Euler(Rotation);
        RotateY = 0;
        if (DisplayText) { DisplayText.text = RotateY.ToString(); }
    }
    //Stop Rotation
    public void StopRotation() {
        RotateByHidden = RotateBy;
        RotateBy = 0;
    }
    //Allow Rotation
    public void AllowRotation() {
        RotateBy = RotateByHidden;
    }
	#endregion
}
