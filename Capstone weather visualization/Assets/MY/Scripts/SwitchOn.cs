/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/
using UnityEngine;

public class SwitchOn : MonoBehaviour {
    public bool IsOn = false;
    public void Start() {
        gameObject.SetActive(IsOn);
    }
    public void Switch() {
        IsOn = !IsOn;
        gameObject.SetActive(IsOn);
    }
}
