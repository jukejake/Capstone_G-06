/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class LightData : SerializedMonoBehaviour {

	#region Variables
	public float Range = 5.0f; //The range of the lights.
	public float SpotAngle = 60.0f; //The spot angle of the lights.
	public float Intensity = 1.0f; //The intensity of the lights.
	public float IntensityMuliplier = 1.0f; //The intensity muliplier of the lights.

	public GameObject[] Lights; //All the lights that the variables above will effect.
	#endregion

	#region Set Light data
	[Button]
	//Function to replace the data in Lights to the variables above.
	public void SetLights() {
		foreach (var item in Lights) {
			item.GetComponent<Light>().range = Range;
			item.GetComponent<Light>().spotAngle = SpotAngle;
			item.GetComponent<Light>().intensity = Intensity;
			item.GetComponent<Light>().bounceIntensity = IntensityMuliplier;
		}
	}
	#endregion

	#region Set Intensity
	private float OldIntensity = 1.0f;
	private float NewIntensity = 1.0f;
	//Function to set the intensity of the light with a UI slider.
	public void SetIntensity(Slider obj) {
		NewIntensity = obj.value;
		if (OldIntensity != NewIntensity) {
			OldIntensity = NewIntensity;
			Intensity = NewIntensity;
			SetLights();
		}
	}
	#endregion

}
