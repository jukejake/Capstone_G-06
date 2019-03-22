// =============================
// Written by Marco Valdez for:
// UOIT Capstone: BUSI 4995
// 2018-2019
// =============================

using UnityEngine;
using System.Collections;
using TouchScript.Gestures.TransformGestures;

//[RequireComponent(typeof(MeshRenderer))]

public class rotateController : MonoBehaviour
{
    public ScreenTransformGesture TwoFingerMoveGesture;

    #region ROTATE
    public float rotation_sensitivity_Y = 0.75f;
    public float rotation_sensitivity_X = 0.2f;

    public Vector3 _mouseReference;
    public Vector3 _mouseOffset;
	public Vector3 _rotation = Vector3.zero;
    public bool _isRotating;

    public float zoomOutMin = 1.0f;         // Minimum distance from center object
    public float zoomOutMax = 8.0f;         // Maximum distance from center object
    public float zoomSensitivity = 1.0f;    // How sensitive the camera panning is to dragging (larger is more sensitive)


    public float maxPanY = 20.0f;           // Maximum deviance from "level" in the Y axis (up)
    public float minPanY = -20.0f;           // Maximum deviance from "level" in the Y axis (down)

    public float maxPanX = 10.0f;           // Maximum deviance from "level" in the Y axis (up)
    public float minPanX = -10.0f;           // Maximum deviance from "level" in the Y axis (down)

    private float zoomCurrent = 2.0f;       // Current zoom

	public BoxCollider2D _object;


    #endregion

    void Update()
    {
        if (_isRotating)
        {
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);   // Find difference in old mouse/touch position to new one

            // apply rotation
            //_rotation.y += (_mouseOffset.x + _mouseOffset.y) * rotation_sensitivity;
            _rotation.y += (_mouseOffset.x) * rotation_sensitivity_Y;    // Rotate the scene/object on its Y axis (straight up and down) if you move the mouse horizontally (x)
            _rotation.y = Mathf.Clamp(_rotation.y, minPanY, maxPanY);   // Clamp between -20, 20 (defaults)

            _rotation.x -= (_mouseOffset.y) * rotation_sensitivity_X;     // Rotate scene on its X axis (left and right) if you move the mouse up/down
            _rotation.x = Mathf.Clamp(_rotation.x, minPanX, maxPanX);   // Clamp between -10, 10 (defaults)

            // rotate
            //gameObject.transform.Rotate(_rotation);
            transform.localEulerAngles = new Vector3(_rotation.x, _rotation.y, transform.localEulerAngles.z);   // Apply rotations

            // store new mouse position
            _mouseReference = Input.mousePosition;
        }

        if (Input.touchCount > 1)
        {
            Debug.Log("It works");
            _isRotating = false;
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 lastTouch0Pos = touch0.position - touch0.deltaPosition;
            Vector2 lastTouch1Pos = touch1.position - touch1.deltaPosition;

            float previousMagnitude = (lastTouch0Pos - lastTouch1Pos).magnitude;
            float currentMagnitude = (touch0.position - touch1.position).magnitude;

            float difference = currentMagnitude - previousMagnitude;

            zoom(difference * 0.01f * zoomSensitivity);
        }

        zoom(Input.GetAxis("Mouse ScrollWheel"));
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        //Debug.Log(Input.touchCount);
    }

    void OnMouseDown()
    {
        if ((Input.touchCount == 1) || Input.GetMouseButton(0))
        {
            // rotating flag
            _isRotating = true;
        }
       // else
           // _isRotating = false;

        // store mouse position
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
    }

    void zoom(float increment)
    {
        zoomCurrent += increment * zoomSensitivity;
        zoomCurrent = Mathf.Clamp(zoomCurrent, zoomOutMin, zoomOutMax);

        Vector3 temp = new Vector3(0, 0, increment * zoomSensitivity);
        //Vector3 temp = new Vector3(0, 0, zoomCurrent);
        Camera.main.transform.Translate(temp);

        if (Camera.main.transform.position.z < -zoomOutMax)
        {
            //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -zoomOutMax);
            Camera.main.transform.Translate(-temp);
        }

        if (Camera.main.transform.position.z > -zoomOutMin)
        {
            //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -zoomOutMin);
            Camera.main.transform.Translate(-temp);
        }
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

    private void OnEnable()
    {
        TwoFingerMoveGesture.Transformed += twoFingerTransformHandler;
    }

    private void OnDisable()
    {
        TwoFingerMoveGesture.Transformed -= twoFingerTransformHandler;
    }

    private void twoFingerTransformHandler(object sender, System.EventArgs e)
    {
        Camera.main.transform.localPosition += Vector3.forward * (TwoFingerMoveGesture.DeltaScale - 1f);
        Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, Mathf.Clamp(Camera.main.transform.localPosition.z, -0.4f, -0.3f));
    }

}
