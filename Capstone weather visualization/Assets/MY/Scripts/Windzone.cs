/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
//Not In Use
////*/

using System.Collections.Generic;
using UnityEngine;

public class Windzone : MonoBehaviour {
	//NOT IN USE

	private List<Rigidbody> RigidbodiesInWindZoneList = new List<Rigidbody>();
	public Vector3 windDirection = Vector3.right;
	public float windStrength = 5;

	private void OnTriggerEnter(Collider col) {
		Rigidbody objectRigid = col.gameObject.GetComponent<Rigidbody>();
		if(objectRigid != null){ RigidbodiesInWindZoneList.Add(objectRigid); }
	}

	private void OnTriggerExit(Collider col) {
		Rigidbody objectRigid = col.gameObject.GetComponent<Rigidbody>();
		if (objectRigid != null){ RigidbodiesInWindZoneList.Remove(objectRigid); }
	}

	private void FixedUpdate() {
		if(RigidbodiesInWindZoneList.Count > 0) {
			foreach (Rigidbody rigid in RigidbodiesInWindZoneList) { rigid.AddForce(windDirection * windStrength); }
		}
	}
}
