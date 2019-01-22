/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;


public class RadialSlider: SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

	#region Variables
	private bool isPointerDown = false;
	[BoxGroup("Display Options")]
	[HorizontalGroup("Display Options/1"), LabelWidth(60)]
	public Image FillArea;
	[HorizontalGroup("Display Options/1"), LabelWidth(90)]
	public float Value = 0.0f;
	[HorizontalGroup("Display Options/2"), MinMaxSlider(0.0f, 360.0f, ShowFields = true), LabelWidth(90)]
	public Vector2 Clamp = new Vector2(0.0f, 360.0f);
	[HorizontalGroup("Display Options/3"), LabelWidth(90)]
	public float StartAngle = 0.0f; //beginning and end points
	[HorizontalGroup("Display Options/3"), LabelWidth(40)]
	public float Max = 360.0f;
	[HorizontalGroup("Display Options/4"), LabelWidth(70)]
	public Color Colour1 = Color.green;
	[HorizontalGroup("Display Options/4"), LabelWidth(70)]
	public Color Colour2 = Color.red;
	[HorizontalGroup("Display Options/5"), LabelWidth(90)]
	public WheelController SetFunction;
	[HorizontalGroup("Display Options/6"), LabelWidth(90)]
	public TextMeshProUGUI SetFunction2;
	#endregion

	#region Functions
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
				// TODO: if mouse button down
				if (isPointerDown) {
					Vector2 localPos; 
					
					//Get mouse position  
					RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, Input.mousePosition, ray.eventCamera, out localPos);

					//Calculate mouse position relative to the Start Angle
					var theta = Mathf.Deg2Rad*StartAngle;
					localPos = new Vector2( ((localPos.x * Mathf.Cos(theta)) - (localPos.y * Mathf.Sin(theta))), ((localPos.x * Mathf.Sin(theta)) + (localPos.y * Mathf.Cos(theta))) );

					//Calculate the angle (from 0~1) of the slider based on mouse position
					float angle = Mathf.Clamp(((Mathf.Atan2(-localPos.y, localPos.x)*180.0f/Mathf.PI+180.0f)/360.0f), (Clamp.x/360.0f), (Clamp.y/360.0f));

					//Rotate the slider by the Start Angle
					FillArea.gameObject.transform.localRotation = Quaternion.Euler(0, 0, -StartAngle);
					
					//Set the bar to the correct distance/angle (from 0~1)
					FillArea.fillAmount = angle;

					//Lerp the colours based on the angle (from 0~1)
					FillArea.color = Color.Lerp(Colour1, Colour2, ((angle*360.0f).Map(Clamp.x, Clamp.y, 0, Max)/Max) );

					//Map the Value to the maximum number it could be
					Value = (angle*Max);

					//Set the speed to the Value
					if (SetFunction != null) { SetFunction.SetSpeed(Value); }
					//Set the TextMeshPro text to the Value
					if (SetFunction2 != null) { SetFunction2.text = ((int)Value).ToString(); }
				}
				yield return 0;
			}        
		}
		else { UnityEngine.Debug.LogWarning( "Could not find GraphicRaycaster and/or StandaloneInputModule" ); }      
	}


	#endregion
}
