/*////
//Written by Jacob Rosengren
//Date: April 2020
////*/

using UnityEngine;
using Sirenix.OdinInspector;

public class LookAt : SerializedMonoBehaviour {

	#region Variables
	public Transform target;
	#endregion

	#region Functions
	[Button]
	private void LookAtTarget () {
		transform.LookAt(target);
	}
	#endregion
}
