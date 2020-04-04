/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//Updated: January 2020
//BUSI 4995U Capstone
////*/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class InfoButtons : SerializedMonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

	#region Variables
	[BoxGroup("Display Options")]
	[HorizontalGroup("Display Options/3")]
	public bool AutoClose = false;
	[HorizontalGroup("Display Options/3")]
	public float CloseIn = 10.0f;
	private float Timer = 0.0f;
	[HorizontalGroup("Display Options/4")]
	public GameObject InfoBox;
	private RectTransform RT;
	private Vector3 RectSize = Vector3.zero;
	[HorizontalGroup("Display Options/5")]
	public float ScaleTime = 0.50f;

	private bool Open = false;
	private bool Done = true;


    [BoxGroup("Extra Options")]
    [HorizontalGroup("Extra Options/1")]
    [InfoBox("Keep Text Active.")]
    public bool KTA = true; //Keep Text Active
    [InfoBox("Will activate the text box after the bobble pops up."), HideIf("KTA")]
    [HorizontalGroup("Extra Options/2"), LabelWidth(60), HideIf("KTA")]
    public GameObject TextBox;
    
    public UnityEvent onCompleteCallback;
    #endregion

    #region Functions
    private void Awake(){
		RT = InfoBox.GetComponent<RectTransform>();
	}
    
    public void OnComplete() {
        if (onCompleteCallback != null) {
            onCompleteCallback.Invoke();
        }
    }
    private void ScaleUI() {
        if (!Done) { return; }
        Done = false;
        //When closing, decrease size.
        if (Open) {
            Open = false;
            //Rescale over time
            LeanTween.scale(RT.gameObject, Vector3.zero, ScaleTime).setOnComplete(SetDone);
        }
        //When opening, increase size.
        else {
            Open = true;
            //Rescale over time
            LeanTween.scale(RT.gameObject, Vector3.one, ScaleTime).setOnComplete(SetDone);
            if (AutoClose) { Invoke("ScaleUIClose", CloseIn); }
        }
    }
    public void ScaleUIClose() {
        if (!Done) { return; }
        //When closing, decrease size.
        if (Open) {
            Done = false;
            Open = false;
            //Rescale over time
            LeanTween.scale(RT.gameObject, Vector3.zero, ScaleTime).setOnComplete(SetDone);
        }
    }
    private void SetDone() {
        Done = true;
        if (!KTA) {
            if (Open) { TextBox.SetActive(true); }
            else { TextBox.SetActive(false); }
        }
    }
	//If a mouse clicked the UI was detected
	public void OnPointerClick(PointerEventData eventData) {
        ScaleUI();
	}
	//If a mouse entered the UI was detected
	public void OnPointerEnter(PointerEventData eventData) {
		//if (!Open && HoverToOpen && Done) { Open = true; Done = false; }
	}
	//If a mouse left the UI was detected
	public void OnPointerExit(PointerEventData eventData) {
		//if (Open && HoverOffToClose && !AutoClose) { Open = false; Done = false; }
		//else if (Open && AutoClose) { Timer += Time.deltaTime; }
	}

    public void ClickButtonDown() {
        ScaleUI();
    }
    public void ClickButtonUp() {
    }
    #endregion
}
