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
	public float Range = 15.0f; //The range of the lights.
	public float SpotAngle = 60.0f; //The spot angle of the lights.
	public float Intensity = 1.0f; //The intensity of the lights.
	public float IntensityMuliplier = 1.0f; //The intensity muliplier of the lights.
	public Material LightMaterial;
	private Color c;
	public float MaxSliderRange = 10.0f; //The range of the slider controlling the lights.

	public GameObject[] Lights; //All the lights that the variables above will effect.
	#endregion

	#region Functions
	private void Start() {
		c = LightMaterial.color;
		//LightMaterial.shader = Shader.Find("_EmissionColor");
		LightMaterial.SetColor("_EmissionColor", new Vector4(c.r, c.g, c.b, 0) * Intensity.Map(0.0f, MaxSliderRange, 0.0f, 3.0f));
	}

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
	//Function to set the intensity of the light with a UI slider.
	public void SetIntensity(Slider obj) {
		Intensity = obj.value;
		foreach (var item in Lights) {
			item.GetComponent<Light>().intensity = Intensity;
		}
		LightMaterial.SetColor("_EmissionColor", new Vector4(c.r, c.g, c.b, 0) * Intensity.Map(0.0f, 10.0f, 0.0f, 3.0f));
	}
	#endregion

}
