using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class WheelController : SerializedMonoBehaviour {

    #region Variables
    [Range(0,250)]
    public int Speed;
    private float WheelSize = 0.6884924f; //27.106 inches
    public float RotateAmount { get; set; }
    #endregion

    #region Setup
    private void Awake() {
        RotateAmount = (Speed / WheelSize);
        InvokeRepeating("DelayedUpdate", 1.0f, 0.50f);
    }
    void DelayedUpdate() {
        RotateAmount = (Speed / WheelSize);
    }
    public void SetSpeed(Slider obj) {
        Speed = (int)obj.value;
    }
    #endregion

}
