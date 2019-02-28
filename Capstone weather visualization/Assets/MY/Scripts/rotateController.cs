// =============================
// Written by Marco Valdez for:
// UOIT Capstone: BUSI 4995
// 2018-2019
// =============================

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]

public class rotateController : MonoBehaviour
{

    #region ROTATE
    public float rotation_sensitivity = 1f;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation = Vector3.zero;
    public bool _isRotating;

    public float zoomOutMin = 1.0f;
    public float zoomOutMax = 8.0f;
    public float zoomSensitivity = 1.0f;
    private float zoomCurrent = 2.0f;


    #endregion

    void Update()
    {
        if (_isRotating)
        {
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);

            // apply rotation
            _rotation.y += (_mouseOffset.x + _mouseOffset.y) * rotation_sensitivity;
            _rotation.y = Mathf.Clamp(_rotation.y, -20, 20);

            // rotate
            //gameObject.transform.Rotate(_rotation);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, _rotation.y, transform.localEulerAngles.z);

            // store new mouse position
            _mouseReference = Input.mousePosition;
        }

        if (Input.touchCount == 2)
        {
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
        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
    }

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

}