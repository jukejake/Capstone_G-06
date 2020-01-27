/*////
//Written by Jacob Rosengren
//Date: January 2020
//BUSI 4995U Capstone
////*/

using UnityEngine;
using Sirenix.OdinInspector;

public class SpawnAnimation : SerializedMonoBehaviour {

    #region Variables
    public static SpawnAnimation Instance = null;

    [BoxGroup("Objects")]
    [HorizontalGroup("Objects/1")]
    public Transform exhaust;
    [HorizontalGroup("Objects/2")]
    public Transform spawnObject;
    [HorizontalGroup("Objects/3")]
    public Transform platform;
    [HorizontalGroup("Objects/4")]
    public GameObject rotationUI;

    [BoxGroup("Settings")]
    [HorizontalGroup("Settings/1")]
    public float exhaustSpeed = 1.0f;
    [HorizontalGroup("Settings/1")]
    public float invokeEvery = 0.10f;
    [HorizontalGroup("Settings/2")]
    public float distance = 6.0f;
    [HorizontalGroup("Settings/3")]
    public float carSpeed = 5.0f;
    [HorizontalGroup("Settings/3")]
    public float carInvokeEvery = 0.10f;

    private int stage = 0;
    private bool CarStage;
    private float waiting = 0.0f;

    private bool isOn = true;
    #endregion

    #region Functions
    private void Awake() {
        Instance = this;
    }
    public void Switch() {
        isOn = !isOn;
    }

    [Button]
    public void Play() {
        if (isOn && stage == 0) {
            stage = 1;
            //Reset the platforms rotation 
            platform.rotation = Quaternion.Euler(Vector3.zero);
            rotationUI.GetComponent<ChangeStuff>().ResetRotation();
            //Stop Rotation
            rotationUI.GetComponent<ChangeStuff>().StopRotation();

            InvokeRepeating("Animation", 0.10f, invokeEvery);

            //Set car behind exhaust
            spawnObject.localPosition = new Vector3(20.0f,0.1f,0.0f);
        }
    }

    public void Animation(){

        //Waiting
        if (stage == 0) {
            //Error handling
            CancelInvoke("Animation");
            return;
        }

        //Move away from standard position
        if (stage == 1 && exhaust.localPosition.x < distance) {
            Vector3 newSpeed = new Vector3((exhaustSpeed * invokeEvery), 0, 0);
            exhaust.Translate(newSpeed);
        }
        else if (stage == 1) {
            stage = 2;
            //Start moving the car
            InvokeRepeating("CarAnimation", 0.10f, carInvokeEvery);
        }

        //Waiting
        if (stage == 2 && waiting < 4.0f) {
            waiting += invokeEvery;
            CarStage = true;
        }
        else if (stage == 2) { stage = 3; waiting = 0; }

        //Move to standard position
        if (stage == 3 && exhaust.localPosition.x > 0) {
            Vector3 newSpeed = new Vector3((-exhaustSpeed * invokeEvery), 0, 0);
            exhaust.Translate(newSpeed);
        }
        else if (stage == 3) {
            stage = 0;
            //Set position to zero (in case of overshooting)
            exhaust.localPosition = Vector3.zero;
            //Cancel Invoke
            CancelInvoke("Animation");
        }
    }
    public void CarAnimation() {

        //Move to standard position
        if (CarStage && spawnObject.localPosition.x > 0) {
            Vector3 newSpeed = new Vector3((-carSpeed * carInvokeEvery), 0, 0);
            spawnObject.Translate(newSpeed);
        }
        else {
            //Set position to zero (in case of overshooting)
            spawnObject.localPosition = new Vector3(0.0f, 0.1f, 0.0f);
            //Cancel Invoke
            CancelInvoke("CarAnimation");
            //Allow Rotation
            rotationUI.GetComponent<ChangeStuff>().AllowRotation();
        }
    }

    //Cancel the Invoke when the object is destroyed.
    private void OnDestroy() {
        CancelInvoke("Animation");
        CancelInvoke("CarAnimation");
    }

    #endregion
}
