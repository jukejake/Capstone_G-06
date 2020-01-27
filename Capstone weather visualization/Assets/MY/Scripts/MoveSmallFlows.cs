/*////
//Written by Jacob Rosengren
//Date: January 2020
//BUSI 4995U Capstone
////*/

using UnityEngine;
using UnityEngine.UI;

public class MoveSmallFlows : MonoBehaviour {

    #region Variables
    public Transform flows;
    public Slider slider;
    #endregion

    #region Functions
    void Update() {
        flows.transform.localPosition = new Vector3(0, 0, slider.value);
    }

    private void OnBecameInvisible() {
        slider.value = 0;
        flows.transform.localPosition = Vector3.zero;
    }
    private void OnEnable() {
        slider.value = 0;
        flows.transform.localPosition = Vector3.zero;
    }
    #endregion
}
