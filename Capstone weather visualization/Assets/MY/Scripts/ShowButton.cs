/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//Updated: January 2020
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
    public float ScaleTime = 0.50f;
    private RectTransform RT;
	private bool Open = false;
	private bool Done = true;
    #endregion

    #region Functions
    private void Awake() {
		RT = ToggleObject.GetComponent<RectTransform>();
	}
    public void ScaleUI() {
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
            ToggleObject.SetActive(true);
            //Rescale over time
            LeanTween.scale(RT.gameObject, Vector3.one, ScaleTime).setOnComplete(SetDone);
        }
    }
    private void SetDone() {
        Done = true;
        //After closeing.
        if (!Open) { ToggleObject.SetActive(false); }
    }

    public void ResetButton() {
        RT.localScale = Vector3.zero;
        ToggleObject.SetActive(false);
        Done = true;
        Open = false;
    }
	#endregion
}
