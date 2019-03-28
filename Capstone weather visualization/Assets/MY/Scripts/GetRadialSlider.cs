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

public class GetRadialSlider : MonoBehaviour, IPointerDownHandler,IPointerUpHandler{
	#region Variables
	private bool isPointerDown = false;
	public float ErrorRange;
	private float Percentage;
	private float angle;
	private float MaxSpeed = 250.0f;
	public float StartAngle = 0.0f;
	public Vector2 Clamp = new Vector2(0.0f, 360.0f);
    public Vector2 MinMaxVal = new Vector2(0.0f, 1.0f);
    public Vector2 Emission = new Vector2(50.0f, 500.0f);

    public Vector2 StartLife = new Vector2(0.0f, 60.0f);


    public WheelController SetFunction;
	public FlowData SetFunction2;

    private GraphicRaycaster ray;
    private StandaloneInputModule input;

    public AdultLink.SpeedBar bar;

    #endregion

    #region Functions
    private void Awake() {
        ray = GetComponentInParent<GraphicRaycaster>();
        input = FindObjectOfType<StandaloneInputModule>();
    }

    private void FixedUpdate() {
		if (bar.fillPercentage > (Percentage + ErrorRange)) {
			bar.status = AdultLink.Status.braking;
			//Set the speed to the Value
			if (SetFunction != null) { SetFunction.SetSpeed(bar.fillPercentage * MaxSpeed); }
			if (SetFunction2 != null) {
				SetFunction2.SetSpeed(bar.fillPercentage * MaxSpeed);
				SetFunction2.SetEmissionRate(Emission.x + (bar.fillPercentage * Emission.y));
                SetFunction2.SetLifeTime(StartLife.x + ((1 - bar.fillPercentage) * StartLife.y));
            }
            if (bar.fillPercentage < MinMaxVal.x) {
                bar.fillPercentage = 0;
            }
        }
		else if (bar.fillPercentage < (Percentage - ErrorRange)) {
			bar.status = AdultLink.Status.accel;
			//Set the speed to the Value
			if (SetFunction != null) { SetFunction.SetSpeed(bar.fillPercentage * MaxSpeed); }
			if (SetFunction2 != null) {
				SetFunction2.SetSpeed(bar.fillPercentage * MaxSpeed);
				SetFunction2.SetEmissionRate(Emission.x + (bar.fillPercentage * Emission.y));
                SetFunction2.SetLifeTime(StartLife.x + ((1 - bar.fillPercentage) * StartLife.y));
            }
            if (bar.fillPercentage > MinMaxVal.y) {
                bar.fillPercentage = 1;
            }
        }
		else {
            bar.status = AdultLink.Status.idle;
            SetFunction.SetSpeed(bar.fillPercentage * MaxSpeed);
            SetFunction2.SetSpeed(bar.fillPercentage * MaxSpeed);
        }

	}

	////When the mouse is over the UI, Track it.
	//public void OnPointerEnter(PointerEventData eventData) {
	//	StartCoroutine("TrackPointer");
	//}
	////When the mouse is not over the UI, Don't track it.
	//public void OnPointerExit(PointerEventData eventData) {
	//	StopCoroutine("TrackPointer");
	//}
	//If a mouse clicked the UI was detected.
	public void OnPointerDown(PointerEventData eventData) {
		isPointerDown = true;
        CaptureRaycast(eventData);

    }
	//If a mouse clicked ended was detected.
	public void OnPointerUp(PointerEventData eventData) {
		isPointerDown = false;
        //CaptureRaycast(eventData);
    }
 //   public void OnSelect(BaseEventData eventData) {
	//	StartCoroutine("TrackPointer");
 //       isPointerDown = true;
 //   }

 //   public void OnDeselect(BaseEventData eventData) {
	//	StopCoroutine("TrackPointer");
 //       isPointerDown = false;
 //   }

 //   public void OnUpdateSelected(BaseEventData eventData) {
 //       throw new System.NotImplementedException();
 //   }

    public void Track() { 
        isPointerDown = true;
		StartCoroutine("TrackPointer");
    }
    public void StopTrack() {
        isPointerDown = false;
        StopCoroutine("TrackPointer");
    }

    //Track the mouse position
    IEnumerator TrackPointer() {

		if (ray != null && input != null && isPointerDown) {
			while (Application.isPlaying) {
				Vector2 localPos;

				//Get mouse position
				RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, Input.mousePosition, ray.eventCamera, out localPos);

				//Calculate mouse position relative to the Start Angle
				var theta = Mathf.Deg2Rad*StartAngle;
				localPos = new Vector2( ((localPos.x * Mathf.Cos(theta)) - (localPos.y * Mathf.Sin(theta))), ((localPos.x * Mathf.Sin(theta)) + (localPos.y * Mathf.Cos(theta))) );
                localPos.y = Mathf.Clamp(localPos.y, 0, Clamp.y);
                //Debug.Log(localPos);
				//Calculate the angle (from 0~1) of the slider based on mouse position
				angle = Mathf.Clamp(((Mathf.Atan2(-localPos.y, localPos.x)*180.0f/Mathf.PI+180.0f)/360.0f), (Clamp.x/360.0f), (Clamp.y/360.0f));

				Percentage = angle.Map((Clamp.x / 360.0f), (Clamp.y / 360.0f),0,1);
				
				yield return 0;
			}
		}
		else { UnityEngine.Debug.LogWarning( "Could not find GraphicRaycaster and/or StandaloneInputModule" ); }
	}

    public void CaptureRaycast(PointerEventData eventData) {
		if (ray != null && input != null) {
			Vector2 localPos;
            //Get mouse position
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, eventData.position, ray.eventCamera, out localPos);
            
            //Calculate mouse position relative to the Start Angle
            var theta = Mathf.Deg2Rad * StartAngle;
            localPos = new Vector2(((localPos.x * Mathf.Cos(theta)) - (localPos.y * Mathf.Sin(theta))), ((localPos.x * Mathf.Sin(theta)) + (localPos.y * Mathf.Cos(theta))));
            localPos.y = Mathf.Clamp(localPos.y, 0, Clamp.y);
            //Debug.Log(localPos);
            //Calculate the angle (from 0~1) of the slider based on mouse position
            angle = Mathf.Clamp(((Mathf.Atan2(-localPos.y, localPos.x) * 180.0f / Mathf.PI + 180.0f) / 360.0f), (Clamp.x / 360.0f), (Clamp.y / 360.0f));
            
            Percentage = angle.Map((Clamp.x / 360.0f), (Clamp.y / 360.0f), 0, 1);
        }
	}
    public void SetPercentage(Slider p) { Percentage = p.value; }

    #endregion
}
