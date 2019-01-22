﻿/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;
using Sirenix.OdinInspector;

public class ShowButton : SerializedMonoBehaviour {

	#region Variables
	[BoxGroup("Display Options")]
	[HorizontalGroup("Display Options/1")]
	public GameObject ToggleObject;
	[HorizontalGroup("Display Options/2")]
	public float ScaleFactor = 1.0f;
	private RectTransform RT;
	private Vector3 RectSize = Vector3.zero;
	private bool Open = false;
	#endregion

	#region Functions
	private void Awake() {
		RT = ToggleObject.GetComponent<RectTransform>();
	}

	public void ToggleOpen () {
		if (Open) { Open = false; }
		else if (!Open) { Open = true; ToggleObject.SetActive(true); }
	}

	private void Update () {
		//When opening, increase size.
		if (Open) { IncreaseScale(); }
		//When closing, decrease size.
		else if (!Open) { DecreaseScale(); }
	}
	//Expand the UI from 0 to 1.
	private void IncreaseScale() {
		//Rescale over time
		if (RectSize.x < 1.0f) {
			RectSize.x += ScaleFactor * Time.deltaTime;
			RectSize.y += ScaleFactor * Time.deltaTime;
			RectSize.z += ScaleFactor * Time.deltaTime;
			RT.localScale = RectSize;
		}
		//cap at 1
		else if (RectSize.x > 1.0f) {
			RT.localScale = Vector3.one;
		}
	}
	//Srink the UI from 1 to 0.
	private void DecreaseScale() {
		//Rescale over time
		if (RectSize.x > 0.0f) {
			RectSize.x -= ScaleFactor * Time.deltaTime * 2.0f;
			RectSize.y -= ScaleFactor * Time.deltaTime * 2.0f;
			RectSize.z -= ScaleFactor * Time.deltaTime * 2.0f;
			RT.localScale = RectSize;
		}
		//cap at 0
		else if (RectSize.x < 0.0f) {
			RT.localScale = Vector3.zero;
			ToggleObject.SetActive(false);
		}
	}
	#endregion
}
