/*////
//Written by Jacob Rosengren
//Date: January 2020
//BUSI 4995U Capstone
////*/

using UnityEngine;

public class ChangeWeather : MonoBehaviour {

    #region Variables
    public static ChangeWeather instance = null;
    
    public int State = 0;
    public GameObject wind;
    public GameObject rain;
    public GameObject snow;
    #endregion

    #region Functions
    private void Awake() {
        instance = this;
    }

    public void SetWeather(){
        if (State == 0) {
            State = 1;
            rain.SetActive(true);
            wind.SetActive(false);
            FlowData.instance.SetWeather(1);
            FlowData.instance.SetWeather(-1);
        }
        else if (State == 1) {
            State = 2;
            snow.SetActive(true);
            rain.SetActive(false);
            FlowData.instance.SetWeather(2);
            FlowData.instance.SetWeather(-1);
        }
        else {
            State = 0;
            wind.SetActive(true);
            snow.SetActive(false);
            FlowData.instance.SetWeather(0);
            FlowData.instance.SetWeather(-1);
        }
    }

    public void ResetWeather() {
        State = 0;
        FlowData.instance.ClearWeather();

        wind.SetActive(true);
        rain.SetActive(false);
        snow.SetActive(false);
        FlowData.instance.ActiveFlow = -1;
        FlowData.instance.SetSpeed(0);
        FlowData.instance.SetWeather(0);
        FlowData.instance.SetWeather(-1);
    }
    #endregion
}
