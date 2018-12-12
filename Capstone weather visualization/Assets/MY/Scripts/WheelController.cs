using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;

public class WheelController : SerializedMonoBehaviour {

	#region Variables
	[Range(0,250)]
	public int Speed;
	private int OldSpeed;
	private float WheelSize = 0.6884924f; //27.106 inches to meters
	public float RotateAmount { get; set; }
	public TextMeshProUGUI SpeedText;
	#endregion

	#region Setup
	private void Awake() {
		RotateAmount = (Speed / WheelSize);
		InvokeRepeating("DelayedUpdate", 1.0f, 0.20f);
	}
	void DelayedUpdate() {
		if (OldSpeed == Speed) { return; }
		RotateAmount = (Speed / WheelSize);
		SpeedText.text = Speed.ToString();
		OldSpeed = Speed;
	}
	public void SetSpeed(Slider obj) {
		Speed = (int)obj.value;
	}
	#endregion

}
