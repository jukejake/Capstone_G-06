/*////
//Written by Jacob Rosengren
//Date: 2020
//BUSI 4995U Capstone
////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SwitchManager : SerializedMonoBehaviour {
    #region Variables
    public float ScaleTime = 0.50f;
    [InfoBox("GameObject: Object to hide, Bool: Scale UI")]
    public Dictionary<GameObject, bool> table = new Dictionary<GameObject, bool>();
    #endregion

    #region Functions
    [Button]
    public void SetState(GameObject _object, bool state) {
		if (table.TryGetValue(_object, out bool t)) { t = state; }
        _object.SetActive(state);
    }
    public void SetOn(GameObject _object) {
		if (table.TryGetValue(_object, out bool t)) { t = true; }
        _object.SetActive(true);
    }
    [Button]
    public void SetAllOn() {
		foreach (var item in table) {
			var i = item.Key;
			if (table.TryGetValue(i, out bool t)) { t = true; }
            i.SetActive(true);
        }
    }
    public void SetOff(GameObject _object) {
		if (table.TryGetValue(_object, out bool t)) { t = false; }
        _object.SetActive(false);
    }
    [Button]
    public void SetAllOff() {
		foreach (var item in table) {
			var i = item.Key;
			if (table.TryGetValue(i, out bool t)) { t = false; }
            i.SetActive(false);
        }
    }
    public void Switch(GameObject _object) {
		if (table.TryGetValue(_object, out bool t)) { t = !t; }
        _object.SetActive(!_object.activeSelf);
    }
    [Button]
    public void SwitchAll() {
		foreach (var item in table) {
			var i = item.Key;
			if (table.TryGetValue(i, out bool t)) { t = !t; }
            i.SetActive(!i.activeSelf);
        }
	}


    public void ScaleUI(GameObject _object) {
        //When closing, decrease size.
        if (_object.activeSelf) {
            //Rescale over time
            LeanTween.scale(_object, Vector3.zero, ScaleTime);
            this.StartCoroutine(() => { _object.SetActive(false); }, ScaleTime);
        }
        //When opening, increase size.
        else {
            _object.SetActive(true);
            //Rescale over time
            LeanTween.scale(_object, Vector3.one, ScaleTime);
        }
    }
    public void ResetUI(GameObject _object) {
        _object.GetComponent<RectTransform>().localScale = Vector3.zero;
        _object.SetActive(false);
    }
    #endregion
}
