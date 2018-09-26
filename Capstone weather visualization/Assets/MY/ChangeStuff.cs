using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStuff : MonoBehaviour {

    // Use this for initialization
    //private void Start () {
    //   InvokeRepeating("DelayedUpdate", 1.0f, 1.0f);
    //}
    //private void DelayedUpdate() {
    //   NewScale = this.GetComponent<Slider>().value;
    //   if (OldScale != NewScale) { OldScale = NewScale; }
    //}

    #region Set Scale
    private float OldScale = 1.0f;
    private float NewScale = 1.0f;

    public void SetScale(GameObject obj) {
        NewScale = this.GetComponent<Slider>().value;
        if (OldScale != NewScale) { OldScale = NewScale; }
        obj.transform.localScale = new Vector3(NewScale, NewScale, NewScale);
    }
    #endregion

    #region Set Toggle
    private bool OldToggleState = true;
    private bool NewToggleState = true;

    public void SetToggle(GameObject obj) {
        NewToggleState = this.GetComponent<Toggle>().isOn;
        if (OldToggleState != NewToggleState) { OldToggleState = NewToggleState; }
        obj.SetActive(NewToggleState);
    }
    #endregion
}
