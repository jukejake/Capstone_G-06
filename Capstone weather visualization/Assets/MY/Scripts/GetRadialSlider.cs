/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/
///
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class GetRadialSlider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {
	#region Variables
	private bool isPointerDown = false;
	public float ErrorRange;
	private float Percentage;
	private float angle;
	private float Speed = 250.0f;
	public float StartAngle = 0.0f;
	public Vector2 Clamp = new Vector2(0.0f, 360.0f);
	public Vector2 Emission = new Vector2(50.0f, 500.0f);


	public WheelController SetFunction;
	public FlowData SetFunction2;

	public AdultLink.SpeedBar bar;

	#endregion

	#region Functions

	private void FixedUpdate() {
		if (bar.fillPercentage > (Percentage + ErrorRange)) {
			bar.status = AdultLink.Status.braking;
			//Set the speed to the Value
			if (SetFunction != null) { SetFunction.SetSpeed(bar.fillPercentage * Speed); }
			if (SetFunction2 != null) {
				SetFunction2.SetSpeed(bar.fillPercentage * Speed);
				SetFunction2.SetEmissionRate(Emission.x + (bar.fillPercentage * Emission.y));
			}
		}
		else if (bar.fillPercentage < (Percentage - ErrorRange)) {
			bar.status = AdultLink.Status.accel;
			//Set the speed to the Value
			if (SetFunction != null) { SetFunction.SetSpeed(bar.fillPercentage * Speed); }
			if (SetFunction2 != null) {
				SetFunction2.SetSpeed(bar.fillPercentage * Speed);
				SetFunction2.SetEmissionRate(Emission.x + (bar.fillPercentage * Emission.y));
			}
		}
		else { bar.status = AdultLink.Status.idle; }

	}

	//When the mouse is over the UI, Track it.
	public void OnPointerEnter(PointerEventData eventData) {
		StartCoroutine("TrackPointer");
	}
	//When the mouse is not over the UI, Don't track it.
	public void OnPointerExit(PointerEventData eventData) {
		StopCoroutine("TrackPointer");
	}
	//If a mouse clicked the UI was detected.
	public void OnPointerDown(PointerEventData eventData) {
		isPointerDown = true;
	}
	//If a mouse clicked ended was detected.
	public void OnPointerUp(PointerEventData eventData) {
		isPointerDown = false;
	}
	//Track the mouse position
	IEnumerator TrackPointer() {
		var ray = GetComponentInParent<GraphicRaycaster>();
		var input = FindObjectOfType<StandaloneInputModule>();

		if (ray != null && input != null) {
			while (Application.isPlaying) {
				if (isPointerDown) {
					Vector2 localPos;

					//Get mouse position
					RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, Input.mousePosition, ray.eventCamera, out localPos);

					//Calculate mouse position relative to the Start Angle
					var theta = Mathf.Deg2Rad*StartAngle;
					localPos = new Vector2( ((localPos.x * Mathf.Cos(theta)) - (localPos.y * Mathf.Sin(theta))), ((localPos.x * Mathf.Sin(theta)) + (localPos.y * Mathf.Cos(theta))) );

					//Calculate the angle (from 0~1) of the slider based on mouse position
					angle = Mathf.Clamp(((Mathf.Atan2(-localPos.y, localPos.x)*180.0f/Mathf.PI+180.0f)/360.0f), (Clamp.x/360.0f), (Clamp.y/360.0f));

					Percentage = angle.Map((Clamp.x / 360.0f), (Clamp.y / 360.0f),0,1);
				}
				yield return 0;
			}
		}
		else { UnityEngine.Debug.LogWarning( "Could not find GraphicRaycaster and/or StandaloneInputModule" ); }
	}

	#endregion
}
