/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;

public class WheelController : SerializedMonoBehaviour {

    #region Variables

    public static WheelController instance = null;

    [Range(0,250)]
	public int Speed;
	private int OldSpeed;
	private float WheelSize = 0.6884924f; //27.106 inches to meters
	public float RotateAmount { get; set; }
	public TextMeshProUGUI SpeedText;

	[HideInInspector]
	public bool UseTreadmill;
	[ShowIf("UseTreadmill")]
	public Renderer Treadmill;
	[ShowIf("UseTreadmill")]
	public float TreadmillSpeed = 0.2f;
	private float offset = 0.0f;
	[HideIf("UseTreadmill")]
	public GameObject Dynos;

    public Transform Spawnpoint;
    #endregion

    #region Functions
    //Used for initialization.
    private void Awake() {
        instance = this;

		RotateAmount = (Speed / (WheelSize*Mathf.PI));
		if (UseTreadmill != false) { InvokeRepeating("TreadmillUpdate", 1.0f, 0.01f); }
	}
	//Create a Treadmill Update.
	void TreadmillUpdate() {
        float val = Time.deltaTime * RotateAmount * TreadmillSpeed;
        
        if (val > 0.05) {
            offset += val; //
            Treadmill.material.SetTextureOffset("_MainTex", new Vector2(0, -offset));
        }
	}
	//Cancel the Invoke when the object is destroyed.
	private void OnDestroy() { CancelInvoke("TreadmillUpdate"); }


	//Function to set the speed based on a UI slider.
	public void SetSpeed(Slider obj) {
		Speed = (int)obj.value;
		//If the speed is different, re-calculate the speed.
		if (OldSpeed == Speed) { return; }

		RotateAmount = (Speed / (WheelSize * Mathf.PI));
		SpeedText.text = Speed.ToString();
		OldSpeed = Speed;
	}
	//Function to set the speed based on a float value.
	public void SetSpeed(float value) {
		Speed = (int)value;
		//If the speed is different, re-calculate the speed.
		if (OldSpeed == Speed) { return; }

		RotateAmount = (Speed / (WheelSize * Mathf.PI));
		SpeedText.text = Speed.ToString();
		OldSpeed = Speed;
	}

	[Button]
	public void SwitchRollers() {
		if (!UseTreadmill) {
			Dynos.SetActive(false);
			Treadmill.gameObject.SetActive(true);
			UseTreadmill = true;
			InvokeRepeating("TreadmillUpdate", 1.0f, 0.01f);
            Spawnpoint.localPosition = new Vector3(1.0f, 0.1f, 0.0f);
            SpawnAnimation.instance.RegularPosition = new Vector3(1.0f, 0.1f, 0.0f);

        }
		else {
			Dynos.SetActive(true);
			Treadmill.gameObject.SetActive(false);
			UseTreadmill = false;
			CancelInvoke("TreadmillUpdate");
            Spawnpoint.localPosition = new Vector3(0.0f, 0.1f, 0.0f);
            SpawnAnimation.instance.RegularPosition = new Vector3(0.0f, 0.1f, 0.0f);
        }
	}
	
	#endregion

}
