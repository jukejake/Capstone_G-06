using UnityEngine;
using Sirenix.OdinInspector;

public class ShowButton : SerializedMonoBehaviour {

	#region Variables
	[BoxGroup("Display Options")]
	[HorizontalGroup("Display Options/1")]
	public GameObject ToggleObject;
	[HorizontalGroup("Display Options/2")]
	public float ScaleFactor = 1.0f;
	private RectTransform RT;
	private Vector3 RectSize = Vector3.zero;
	private bool Open = false;
	#endregion

	#region Functions
	private void Awake() {
		RT = ToggleObject.GetComponent<RectTransform>();
	}

	public void ToggleOpen () {
		if (Open) { Open = false; }
		else if (!Open) { Open = true; ToggleObject.SetActive(true); }
	}

	private void Update () {
		if (Open) { IncreaseScale(); }
		else if (!Open) { DecreaseScale(); }
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
			ToggleObject.SetActive(false);
		}
	}
	#endregion
}
