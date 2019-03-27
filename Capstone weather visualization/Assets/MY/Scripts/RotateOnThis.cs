/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;
using UnityEngine.EventSystems;

public class RotateOnThis : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	public rotateController RC;
	void Update()
    {

        if (!RC.CanZoom) { return; }
        if (RC._isRotating)
        {
			// offset
			RC._mouseOffset = (Input.mousePosition - RC._mouseReference);   // Find difference in old mouse/touch position to new one

			// apply rotation
			//_rotation.y += (_mouseOffset.x + _mouseOffset.y) * rotation_sensitivity;
			RC._rotation.y += (RC._mouseOffset.x) * RC.rotation_sensitivity_Y;    // Rotate the scene/object on its Y axis (straight up and down) if you move the mouse horizontally (x)
            RC._rotation.y = Mathf.Clamp(RC._rotation.y, RC.minPanY, RC.maxPanY);   // Clamp between -20, 20 (defaults)

            RC._rotation.x -= (RC._mouseOffset.y) * RC.rotation_sensitivity_X;     // Rotate scene on its X axis (left and right) if you move the mouse up/down
			RC._rotation.x = Mathf.Clamp(RC._rotation.x, RC.minPanX, RC.maxPanX);   // Clamp between -10, 10 (defaults)

			// rotate
			//gameObject.transform.Rotate(_rotation);
			RC.transform.localEulerAngles = new Vector3(RC._rotation.x, RC._rotation.y, RC.transform.localEulerAngles.z);   // Apply rotations

			// store new mouse position
			RC._mouseReference = Input.mousePosition;
        }

        if (Input.touchCount == 2)
        {
			RC._isRotating = false;
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 lastTouch0Pos = touch0.position - touch0.deltaPosition;
            Vector2 lastTouch1Pos = touch1.position - touch1.deltaPosition;

            float previousMagnitude = (lastTouch0Pos - lastTouch1Pos).magnitude;
            float currentMagnitude = (touch0.position - touch1.position).magnitude;

            float difference = currentMagnitude - previousMagnitude;

            RC.zoom(difference * RC.zoomSensitivity);
        }
        
    }

	//If a mouse clicked the UI was detected
	public void OnPointerDown(PointerEventData eventData) {
		if ((Input.touchCount == 1) || Input.GetMouseButton(0)) { 
			// rotating flag
			RC._isRotating = true;

			// store mouse position
			RC._mouseReference = Input.mousePosition;
		}
	}
	public void OnPointerUp(PointerEventData eventData) {
		// rotating flag
		RC._isRotating = false;
	}
}
