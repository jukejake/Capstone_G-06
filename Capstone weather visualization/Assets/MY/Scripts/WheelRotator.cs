using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotator : MonoBehaviour {

    #region Variables
    WheelController WC;
    private float RotateAmount;
    public Transform[] LeftWheels;
    public Transform[] RightWheels;
    #endregion

    #region Setup
    // Use this for initialization
    void Start () {
        WC = FindObjectOfType<WheelController>();
        RotateAmount = WC.RotateAmount;
        InvokeRepeating("DelayedUpdate", 1.0f, 0.50f);
    }
	
	void DelayedUpdate() {
        RotateAmount = WC.RotateAmount;
    }

    private void FixedUpdate() {
        foreach (var wheel in LeftWheels) {
            wheel.Rotate(Vector3.left, RotateAmount);
        }
        foreach (var wheel in RightWheels) {
            wheel.Rotate(Vector3.right, RotateAmount);
        }
    }


    #endregion
}
