/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//Updated: January 2020
//BUSI 4995U Capstone 
////*/

using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class SplashScreen : SerializedMonoBehaviour {

    #region Variables
    public static SplashScreen instance = null;

    [BoxGroup("Timer")]
    public float CounterInMinutes = 10.0f;
    [BoxGroup("Timer")]
    private float Counter;
    [BoxGroup("Timer")]
    public float TimeLeft = 0.0f;
    [BoxGroup("Timer")]
    public ChangeScene BSD;
    
    #endregion

    #region Functions

    private void Awake() {
        instance = this;
    }

    private void Start() {
		Counter = CounterInMinutes * 60;
		TimeLeft = Counter;
		//Switch to splash screen
		BSD.SwitchScene();
	}

	private void FixedUpdate() {

		//Need to check if a person is using it or not.
		if (!Input.anyKey && Input.touchCount == 0) {
			TimeLeft -= Time.fixedDeltaTime;
			if (TimeLeft < 0.0f) {
				//Switch to splash screen
				TimeLeft = Counter;
				BSD.SwitchScene();
			}
		}
		else { TimeLeft = Counter; }
	}

	private void OnBecameVisible() {
		TimeLeft = Counter;
    }
    #endregion
}
