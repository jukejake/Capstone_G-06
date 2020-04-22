/*////
//Written by Jacob Rosengren
//Date: January 2020
//BUSI 4995U Capstone 
//Updated: April 2020
////*/

using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class ResetDefault : SerializedMonoBehaviour {

    #region Variables

    public static ResetDefault instance = null;

    [BoxGroup("Reset")]
    public RadialSlider radialSlider;
    [BoxGroup("Reset")]
    public Slider lightsSlider;
    [BoxGroup("Reset")]
    public GameObject[] lightToggles;
    [BoxGroup("Reset")]
    public ShowButton solarToggle;
    [BoxGroup("Reset")]
    public ShowButton cameraToggle;
    [BoxGroup("Reset")]
    public SwitchOn cameraSwitch;

    #endregion

    #region Functions

    private void Awake() {
        instance = this;
    }
    [Button]
    public void ResetToDefault() {

        //Clear all the spawned objects
        SpawnObjects.instance.Clear();

        //Switch the Camera back to the Main camera
        if (SwitchCamera.instance && SwitchCamera.instance.CameraStat == 1) { SwitchCamera.instance.Switch(); }
        if (cameraSwitch.IsOn) { cameraSwitch.Switch(); }
        cameraToggle.ResetButton();

        //Reset the Dyno rotation
        ChangeDyno.instance.ResetRotation();

        //Reset the Nozzle position
        ChangeNozzle.instance.ResetNozzle();

        //Reset the Weather
        ChangeWeather.instance.ResetWeather();
        solarToggle.ResetButton();

        //Reset the Speed
        WheelController.instance.SetSpeed(0);
        radialSlider.ResetSlider();


        //=====LIGHTS=====//

        //Reset light intensity
        LightData.instance.SetLights();
        //Reset position & rotation & toggles (just the toggle index)
        MovableLightingArray.instance.ResetLights();

        //Reset slider
        lightsSlider.value = 1;

        //Reset toggles UI
        foreach (var item in lightToggles) {
            if (item.transform.GetChild(0).GetChild(0).gameObject.activeSelf) {
                item.transform.GetChild(0).GetChild(0).GetComponent<SwitchOn>().Switch();
            }
        }
    }
	#endregion
}
