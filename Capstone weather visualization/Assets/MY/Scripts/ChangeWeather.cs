/*////
//Written by Jacob Rosengren
//Date: January 2020
//BUSI 4995U Capstone
////*/

using UnityEngine;

public class ChangeWeather : MonoBehaviour {

    #region Variables
    public FlowData flowData;
    public int State = 0;
    public GameObject wind;
    public GameObject rain;
    public GameObject snow;
    #endregion

    #region Functions
    public void SetWeather(){
        if (State == 0) {
            State = 1;
            rain.SetActive(true);
            wind.SetActive(false);
            flowData.SetWeather(1);
            flowData.SetWeather(-1);
        }
        else if (State == 1) {
            State = 2;
            snow.SetActive(true);
            rain.SetActive(false);
            flowData.SetWeather(2);
            flowData.SetWeather(-1);
        }
        else {
            State = 0;
            wind.SetActive(true);
            snow.SetActive(false);
            flowData.SetWeather(0);
            flowData.SetWeather(-1);
        }
    }
    #endregion
}
