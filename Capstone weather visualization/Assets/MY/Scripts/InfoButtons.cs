/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class InfoButtons : SerializedMonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

	#region Variables
	[BoxGroup("Display Options")]
	[HorizontalGroup("Display Options/1")]
	public bool PressToOpen = false;
	[HorizontalGroup("Display Options/1")]
	public bool HoverToOpen = false;
	[HorizontalGroup("Display Options/2")]
	public bool PressToClose = false;
	[HorizontalGroup("Display Options/2")]
	public bool HoverOffToClose = false;
	[HorizontalGroup("Display Options/3")]
	public bool AutoClose = false;
	[HorizontalGroup("Display Options/3")]
	public float CloseIn = 1.0f;
	private float Timer = 0.0f;
	[HorizontalGroup("Display Options/4")]
	public GameObject InfoBox;
	private RectTransform RT;
	private Vector3 RectSize = Vector3.zero;
	[HorizontalGroup("Display Options/5")]
	public float ScaleFactor = 1.0f;

	private bool Open = false;
	private bool Done = true;

	#endregion

	#region Functions
	private void Awake(){
		RT = InfoBox.GetComponent<RectTransform>();
	}


	private void Update() {

		//Timer
		//Timer is active so increase Timer.
		if (Timer != 0.0f && Timer < CloseIn) { Timer += Time.deltaTime; }
		//Set to 0.
		else if (Open == false && Timer != 0.0f) { Timer = 0.0f; }
		//Timer is done so set to 0.
		else if (Timer >= CloseIn) { Open = false; Done = false; Timer = 0.0f; }

        if (Done) { return; }
        //Scaler
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
		else if (RectSize.x >= 1.0f) {
			RT.localScale = Vector3.one;
			Done = true;
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
		else if (RectSize.x <= 0.0f) {
			RT.localScale = Vector3.zero;
			Done = true;
		}
	}
	//If a mouse clicked the UI was detected
	public void OnPointerClick(PointerEventData eventData) {
			 if (Open && PressToClose) { Open = false; Done = false; }
		else if (!Open && PressToOpen) { Open = true; Done = false; }
	}
	//If a mouse entered the UI was detected
	public void OnPointerEnter(PointerEventData eventData) {
		if (!Open && HoverToOpen) { Open = true; Done = false; }
	}
	//If a mouse left the UI was detected
	public void OnPointerExit(PointerEventData eventData) {
		if (Open && HoverOffToClose && !AutoClose) { Open = false; Done = false; }
		else if (Open && AutoClose) { Timer += Time.deltaTime; }
	}

    public void ClickButtonDown() {
        if (Open && PressToClose) { Open = false; Done = false; }
        else if (!Open && PressToOpen) { Open = true; Done = false; }
    }
    public void ClickButtonUp() {
		if (Open && HoverOffToClose && !AutoClose) { Open = false; Done = false; }
		else if (Open && AutoClose) { Timer += Time.deltaTime; }
    }
    #endregion
}
