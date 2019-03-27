// =============================
// Written by Marco Valdez for:
// UOIT Capstone: BUSI 4995
// 2018-2019
// =============================
/*////
//Updated by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;
using System.Collections;
using TouchScript.Gestures.TransformGestures;

//[RequireComponent(typeof(MeshRenderer))]

public class rotateController : MonoBehaviour
{
   // public ScreenTransformGesture TwoFingerMoveGesture;

    #region Variables
    public float rotation_sensitivity_Y = 0.75f;
    public float rotation_sensitivity_X = 0.2f;

    public Vector3 _mouseReference;
    public Vector3 _mouseOffset;
	public Vector3 _rotation = Vector3.zero;
    public bool _isRotating;

    public float zoomOutMin = -0.25f;         // Minimum distance from center object // 1.0
    public float zoomOutMax = -0.4f;         // Maximum distance from center object // 8.0
    public float zoomSensitivity = 1.0f;    // How sensitive the camera panning is to dragging (larger is more sensitive)


    public float maxPanY = 20.0f;           // Maximum deviance from "level" in the Y axis (up)
    public float minPanY = -20.0f;           // Maximum deviance from "level" in the Y axis (down)

    public float maxPanX = 10.0f;           // Maximum deviance from "level" in the Y axis (up)
    public float minPanX = -10.0f;           // Maximum deviance from "level" in the Y axis (down)

    public float zoomCurrent = -0.4f;       // Current zoom

    [HideInInspector]
    public bool CanZoom = true;


    #endregion
    //The update function has been moved to RotateOnThis script
    //  that way we can use a UI element to more the camera around.

    void OnMouseDown()
    {
        if ((Input.touchCount == 1) || Input.GetMouseButton(0))
            // rotating flag
            _isRotating = true;

        // store mouse position
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
    }

    public void SwitchZoom() {
        CanZoom = !CanZoom;
    }

    public void zoom(float increment)
    {

        zoomCurrent += increment * zoomSensitivity;
        zoomCurrent = Mathf.Clamp(zoomCurrent, zoomOutMin, zoomOutMax);

        Vector3 temp = new Vector3(0, 0, increment * zoomSensitivity);
        //Vector3 temp = new Vector3(0, 0, zoomCurrent);
        //Camera.main.transform.Translate(temp);
        Camera.main.transform.localPosition += temp;

        Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, 
            Camera.main.transform.localPosition.y, 
            Mathf.Clamp(Camera.main.transform.localPosition.z, zoomOutMin, zoomOutMax));
        //if (Camera.main.transform.position.z < -zoomOutMax)
        //{
        //    //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -zoomOutMax);
        //      Camera.main.transform.Translate(-temp);
       
        //}

        //if (Camera.main.transform.position.z > -zoomOutMin)
        //{
        //    //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -zoomOutMin);
        //    Camera.main.transform.Translate(-temp);
        //}
        //Camera.main.transform.position = Vector3.zero - temp;
    }

    public void SetIsRotating(bool doesRotate)
    {
      _isRotating = doesRotate;
    }

    public void ResetCamera()
    {
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
    }

   // private void OnEnable()
   // {
   //     TwoFingerMoveGesture.Transformed += twoFingerTransformHandler;
   // }
   //
   // private void OnDisable()
   // {
   //     TwoFingerMoveGesture.Transformed -= twoFingerTransformHandler;
   // }
   //
   // private void twoFingerTransformHandler(object sender, System.EventArgs e)
   // {
   //     Camera.main.transform.localPosition += Vector3.forward * (TwoFingerMoveGesture.DeltaScale - 1f);
   //     Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, Mathf.Clamp(Camera.main.transform.localPosition.z, -0.4f, -0.3f));
   // }

}
