/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;
using Sirenix.OdinInspector;

public class MimicPosition : SerializedMonoBehaviour {

	#region Variables
	public Transform ObjectToMimic;
	[HorizontalGroup("X")]
	public bool MimicX = false;
	[HorizontalGroup("X")]
	public float XOffset = 0.0f;
	[HorizontalGroup("Y")]
	public bool MimicY = false;
	[HorizontalGroup("Y")]
	public float YOffset = 0.0f;
	[HorizontalGroup("Z")]
	public bool MimicZ = false;
	[HorizontalGroup("Z")]
	public float ZOffset = 0.0f;
	#endregion

	#region Functions
	void Start() {
		
	}
	
	void Update() {
		Vector3 temp = this.transform.position;
		if (MimicY) { temp.y = ObjectToMimic.position.y+YOffset; }
		if (MimicX) { temp.x = ObjectToMimic.position.x+XOffset; }
		if (MimicZ) { temp.z = ObjectToMimic.position.z+ZOffset; }
		this.transform.position = temp;
	}
	#endregion
}
