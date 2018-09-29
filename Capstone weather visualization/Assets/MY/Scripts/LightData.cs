using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class LightData : SerializedMonoBehaviour {

	#region Variables
	public float Range = 5.0f;
	public float SpotAngle = 60.0f;
	public float Intensity = 1.0f;
	public float IntensityMuliplier = 1.0f;

	public GameObject[] Lights;
	#endregion

	#region Set Light data
	[Button]
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
