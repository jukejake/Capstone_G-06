using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Sirenix.OdinInspector;


public class RadialSlider: SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

	#region Variables
	private bool isPointerDown = false;
	[BoxGroup("Display Options")]
	[HorizontalGroup("Display Options/1"), LabelWidth(60)]
	public Image FillArea;
	[HorizontalGroup("Display Options/1"), LabelWidth(90)]
	public float AngleNumber = 0.0f;
	[HorizontalGroup("Display Options/2"), LabelWidth(70)]
	public float MaxValue = 100.0f;
	[HorizontalGroup("Display Options/3"), LabelWidth(90)]
	public WheelController SetFunction;
	#endregion

	#region Functions
	public void OnPointerEnter(PointerEventData eventData) {
		StartCoroutine("TrackPointer");            
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		StopCoroutine("TrackPointer");
	}

	public void OnPointerDown(PointerEventData eventData) {
		isPointerDown = true;
	}

	public void OnPointerUp(PointerEventData eventData) {
		isPointerDown = false;
	}

	IEnumerator TrackPointer() {
		var ray = GetComponentInParent<GraphicRaycaster>();
		var input = FindObjectOfType<StandaloneInputModule>();
		
		if (ray != null && input != null) {
			while (Application.isPlaying) {
				// TODO: if mouse button down
				if (isPointerDown) {
					Vector2 localPos; // Mouse position  
					RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, Input.mousePosition, ray.eventCamera, out localPos);

					//local position is the mouse position.
					float angle = Mathf.Clamp(((Mathf.Atan2(-localPos.y, localPos.x)*180.0f/Mathf.PI+180.0f)/360.0f), 0.0f, 0.5f);

					FillArea.fillAmount = angle;

					FillArea.color = Color.Lerp(Color.green, Color.red, angle*2.0f);

					AngleNumber = (angle*2.0f*MaxValue);

					SetFunction.SetSpeed(AngleNumber);
					//Debug.Log(localPos+" : "+angle);	
				}
				yield return 0;
			}        
		}
		else { UnityEngine.Debug.LogWarning( "Could not find GraphicRaycaster and/or StandaloneInputModule" ); }      
	}


	#endregion
}
