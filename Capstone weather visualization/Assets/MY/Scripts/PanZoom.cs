using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanZoom : MonoBehaviour {

	#region Variables
	public float PanSpeed = 2.0f;
	public float ZoomSpeed = 10.0f;
	public Vector2 BoundsY = new Vector2(40.0f, 40.0f);
	private Vector2 ZoomBounds = new Vector2(10.0f, 80.0f);

	private Camera camera;

	private Vector3 lastPanPosition;

	private int panFingerId; //Touch mode only
	private bool wasZoomingLastFrame; //Touch mode only
	private Vector2[] lastZoomPositions; //Touch mode only
	#endregion

	#region SetUp
	void Awake() {
		camera = GetComponent<Camera>();
	}
	#endregion

	#region Update

	void Update() {
		if (EventSystem.current.IsPointerOverGameObject()) { return; }
		if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer) {
	        HandleTouch();
	    } else {
	        HandleMouse();
	    }
	}
	void HandleMouse() {
	    if (Input.GetMouseButtonDown(0)) {
	        lastPanPosition = Input.mousePosition;
	    } else if (Input.GetMouseButton(0)) {
	        PanCamera(Input.mousePosition);
	    }
	    float scroll = Input.GetAxis("Mouse ScrollWheel");
	    ZoomCamera(scroll, ZoomSpeed);
	}
	
	void HandleTouch() {
		switch(Input.touchCount) {
		
		case 1: // Panning
		    wasZoomingLastFrame = false;
		    Touch touch = Input.GetTouch(0);
		    if (touch.phase == TouchPhase.Began) {
		        lastPanPosition = touch.position;
		        panFingerId = touch.fingerId;
		    } else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved) {
		        PanCamera(touch.position);
		    }
		    break;
		
		case 2: // Zooming
		    Vector2[] newPositions = new Vector2[]{Input.GetTouch(0).position, Input.GetTouch(1).position};
		    if (!wasZoomingLastFrame) {
		        lastZoomPositions = newPositions;
		        wasZoomingLastFrame = true;
		    } else {
		        float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
		        float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
		        float offset = newDistance - oldDistance;
		
		        ZoomCamera(offset, ZoomSpeed);
		        lastZoomPositions = newPositions;
		    }
		    break;
		    
		default: 
		    wasZoomingLastFrame = false;
		    break;
		}
	}

	#endregion

	#region Pann & Zoom

	void PanCamera(Vector3 newPanPosition) {
		Vector3 offset = camera.ScreenToViewportPoint(lastPanPosition - newPanPosition);
		transform.Rotate(Vector3.down, offset.x*PanSpeed);
		var RotY = transform.localRotation.eulerAngles.y;
		if (180 >= transform.localRotation.eulerAngles.y && transform.localRotation.eulerAngles.y > BoundsY[1]) { RotY = BoundsY[1]; }
		if ((360.0f-BoundsY[0]) > transform.localRotation.eulerAngles.y && transform.localRotation.eulerAngles.y > 180.0f) { RotY = (360.0f-BoundsY[0]); }
		transform.localRotation = Quaternion.Euler(5, RotY, 0);
	}
	
	void ZoomCamera(float offset, float speed) {
	    if (offset == 0) {
	        return;
	    }
		camera.fieldOfView = Mathf.Clamp(camera.fieldOfView - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
	}

	#endregion
}
