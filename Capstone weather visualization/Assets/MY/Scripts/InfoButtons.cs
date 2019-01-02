using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class InfoButtons : SerializedMonoBehaviour, IPointerClickHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler {

	#region Variables
	[BoxGroup("Display Options")]
	[HorizontalGroup("Display Options/1")]
	public bool PressToOpen = false;
	[HorizontalGroup("Display Options/1")]
	public bool HoverToOpen = false;
	[HorizontalGroup("Display Options/2")]
	public bool PressToClose = false;
	[HorizontalGroup("Display Options/2")]
	public bool HoverOffToClose = false;
	[HorizontalGroup("Display Options/3")]
	public bool AutoClose = false;
	[HorizontalGroup("Display Options/3")]
	public float CloseIn = 1.0f;
	private float Timer = 0.0f;
	[HorizontalGroup("Display Options/4")]
	public GameObject InfoBox;
	private RectTransform RT;
	private Vector3 RectSize = Vector3.zero;
	[HorizontalGroup("Display Options/5")]
	public float ScaleFactor = 1.0f;

	private bool Open = false;
	#endregion

	#region Functions
	private void Awake(){
		RT = InfoBox.GetComponent<RectTransform>();
	}

	private void Update() {
		//Timer
		if (Open == false && Timer != 0.0f) { Timer = 0.0f; }
		else if (Timer != 0.0f && Timer < CloseIn) { Timer += Time.deltaTime; }
		else if (Timer >= CloseIn) { SetState(false); Timer = 0.0f; }

		//Scaler
		if (Open) { IncreaseScale(); }
		else if (!Open) { DecreaseScale(); }
	}

	private void SetState(bool value) {
		Open = value;
		//if (Open) { InfoBox.SetActive(true); }
		//else { InfoBox.SetActive(false); }
	}

	private void IncreaseScale() {
		if (RectSize.x < 1.0f) {
			RectSize.x += ScaleFactor * Time.deltaTime;
			RectSize.y += ScaleFactor * Time.deltaTime;
			RectSize.z += ScaleFactor * Time.deltaTime;
			RT.localScale = RectSize;
		}
		else if (RectSize.x > 1.0f) {
			RT.localScale = Vector3.one;
		}
	}

	private void DecreaseScale() {
		if (RectSize.x > 0.0f) {
			RectSize.x -= ScaleFactor * Time.deltaTime * 2.0f;
			RectSize.y -= ScaleFactor * Time.deltaTime * 2.0f;
			RectSize.z -= ScaleFactor * Time.deltaTime * 2.0f;
			RT.localScale = RectSize;
		}
		else if (RectSize.x < 0.0f) {
			RT.localScale = Vector3.zero;
			//InfoBox.SetActive(false);
		}
	}

	public void OnPointerClick(PointerEventData eventData) {
		//Debug.Log("I was clicked.");
			 if (Open && PressToClose) { SetState(false); }
		else if (!Open && PressToOpen) { SetState(true); }
	}

	public void OnDrag(PointerEventData eventData) {
		//Debug.Log("I'm being dragged.");
	}

	public void OnPointerEnter(PointerEventData eventData) {
		//Debug.Log("Someones here.");
		if (!Open && HoverToOpen) { SetState(true); }
	}

	public void OnPointerExit(PointerEventData eventData) {
		//Debug.Log("Someone has left.");
		if (Open && HoverOffToClose && !AutoClose) { SetState(false); }
		else if (Open && AutoClose) { Timer += Time.deltaTime; }
	}
	#endregion
}
