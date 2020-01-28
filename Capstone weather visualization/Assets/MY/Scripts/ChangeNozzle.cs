/*////
//Written by Jacob Rosengren
//Date: January 2020
//BUSI 4995U Capstone
////*/

using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;

public class ChangeNozzle : SerializedMonoBehaviour {

    #region Variables
    public static ChangeNozzle instance = null;

    [FoldoutGroup("ToggleNozzle")]
    [BoxGroup("ToggleNozzle/Objects")]
    public GameObject FrontWall;
    [BoxGroup("ToggleNozzle/Objects")]
    public GameObject FrontMover;
    [BoxGroup("ToggleNozzle/Objects")]
    public GameObject BackWall;
    [BoxGroup("ToggleNozzle/Objects")]
    public GameObject BackMover;
    [BoxGroup("ToggleNozzle/Objects")]
    public float[] FlapRotation = { 0.0f, 10.0f, 10.0f };
    [BoxGroup("ToggleNozzle/Objects")]
    public float[] FlapMoverSize = { 1.0f, 0.16f, 0.16f };

    [BoxGroup("ToggleNozzle/Objects")]
    public GameObject Canopy;
    [BoxGroup("ToggleNozzle/Objects")]
    public float[] CanopyPosition = { 0.0f, 0.0f, 1.8f };

    [BoxGroup("ToggleNozzle/Objects")]
    public TextMeshProUGUI textUI;

    [BoxGroup("ToggleNozzle/Flows")]
    public GameObject SmokeFlow;
    [BoxGroup("ToggleNozzle/Flows")]
    public GameObject RainFlow;
    [BoxGroup("ToggleNozzle/Flows")]
    public GameObject SnowFlow;
    [BoxGroup("ToggleNozzle/Flows")]
    public GameObject FlowPlane;

    [HideInInspector]
    public int State = 0;

    #endregion

    #region Functions

    private void Awake() {
        instance = this;
    }
    
    //Function to set the state of the Nozzle to zero.
    public void ResetNozzle(){
        State = 0;
        ChangeNozzleSize();
        SetNozzleTo(0);
    }
    //Function to set the state of the Nozzle based on a button.
    public void SetNozzle(TextMeshProUGUI text){
        if (State == 2) { State = 0; }
        else { State++; }
        text.text = (State + 1).ToString();
        ChangeNozzleSize();
    }
    //Function to set the state of the Nozzle based on a button.
    public void SetNozzle(){
        if (State == 2) { State = 0; }
        else { State++; }
        textUI.text = (State + 1).ToString();
        ChangeNozzleSize();
    }
    //Function to set the state of the Nozzle based on a slider.
    public void SetNozzle(Slider UI) {
		State = (int)UI.value;
		ChangeNozzleSize();
	}
    //Function to set the state of the Nozzle based on a given number.
    public void SetNozzleTo(int num){
        State = num;
        textUI.text = (num + 1).ToString();
        ChangeNozzleSize();
    }
	//Will rotate on DelayedUpdate, if ToggleNozzle.
	private void ChangeNozzleSize() {

		//Set the position of the walls, and flow.
		//Change the scale of the flow.
		var _pss1 = SmokeFlow.GetComponent<ParticleSystem>().shape;
		var _pss2 =  RainFlow.GetComponent<ParticleSystem>().shape;
		var _pss3 =  SnowFlow.GetComponent<ParticleSystem>().shape;


		FrontWall.transform.localRotation = Quaternion.Euler(0, 0, FlapRotation[State]);
		 BackWall.transform.localRotation = Quaternion.Euler(0, 0, FlapRotation[State]);
		   Canopy.transform.localPosition = new Vector3(0, CanopyPosition[State], 0);
		  FrontMover.transform.localScale = new Vector3(FlapMoverSize[State], 1, 1);
		   BackMover.transform.localScale = new Vector3(FlapMoverSize[State], 1, 1);
		//(7.0m^2) = Size of the nozzle opening.
		if (State == 0) {
			SmokeFlow.transform.localPosition = new Vector3(0, 1.4f, 0);
			 RainFlow.transform.localPosition = new Vector3(0, 1.4f, 0);
			 SnowFlow.transform.localPosition = new Vector3(0, 1.4f, 0);
			_pss1.scale = new Vector3(2, 2.7f, 1.0f);
			_pss2.scale = new Vector3(2, 2.7f, 1.0f);
			_pss3.scale = new Vector3(2, 2.7f, 1.0f);

			FlowPlane.transform.localPosition = new Vector3(0, 1.4f, 0);
			FlowPlane.transform.localScale = new Vector3(0.2f, 1, 0.27f);
		}
		//(13.0m^2) = Size of the nozzle opening.
		else if (State == 1) { 
			SmokeFlow.transform.localPosition = new Vector3(0, 1.4f, 0);
			 RainFlow.transform.localPosition = new Vector3(0, 1.4f, 0);
			 SnowFlow.transform.localPosition = new Vector3(0, 1.4f, 0);
			_pss1.scale = new Vector3(4.25f, 2.7f, 1.0f);
			_pss2.scale = new Vector3(4.25f, 2.7f, 1.0f);
			_pss3.scale = new Vector3(4.25f, 2.7f, 1.0f);

			FlowPlane.transform.localPosition = new Vector3(0, 1.4f, 0);
			FlowPlane.transform.localScale = new Vector3(0.425f, 1, 0.27f);
		}
		////(14.5m^2) = Size of the nozzle opening.
		///
		//(22.0m^2) = Size of the nozzle opening.
		else if (State == 2) {
			SmokeFlow.transform.localPosition = new Vector3(0, 2.16f, 0);
			 RainFlow.transform.localPosition = new Vector3(0, 2.16f, 0);
			 SnowFlow.transform.localPosition = new Vector3(0, 2.16f, 0);
			_pss1.scale = new Vector3(4.75f, 4.25f, 1.0f);
			_pss2.scale = new Vector3(4.75f, 4.25f, 1.0f);
			_pss3.scale = new Vector3(4.75f, 4.25f, 1.0f);

			FlowPlane.transform.localPosition = new Vector3(0, 2.16f, 0);
			FlowPlane.transform.localScale = new Vector3(0.475f, 1, 0.425f);
		}
	}

    #endregion
}
