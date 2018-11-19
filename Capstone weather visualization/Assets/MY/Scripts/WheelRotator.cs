using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotator : MonoBehaviour {

    #region Variables
    WheelController WC;
    private float RotateAmount;
    public Transform[] NegativeRot;
	public Transform[] PositiveRot;
	public bool RotY = false;
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
		if (RotY){
			foreach (var wheel in NegativeRot) {
				wheel.Rotate(Vector3.down, RotateAmount);
			}
			foreach (var wheel in PositiveRot) {
			    wheel.Rotate(Vector3.up, RotateAmount);
			}
		}
		else {
			foreach (var wheel in NegativeRot) {
				wheel.Rotate(Vector3.left, RotateAmount);
			}
			foreach (var wheel in PositiveRot) {
				wheel.Rotate(Vector3.right, RotateAmount);
			}
		}
        
    }


    #endregion
}
