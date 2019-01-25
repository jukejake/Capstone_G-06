/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
//Need to optimize
////*/

using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class SpawnObjects : SerializedMonoBehaviour {

	[OdinSerialize]
	//A dictionary that has all the names and Gameobjects of the cars
	public Dictionary<string, GameObject> IDTable = new Dictionary<string, GameObject>();
	public Transform FrontDynos;
	public Transform BackDynos;

	//Function that will spawn a GameObject based on a key.
	public void SpawnObject(string key) {
		GameObject t;
		if (IDTable.TryGetValue(key, out t)) {
			//Remove the previous car.
			Clear();
			//Spawn the new car.
			GameObject temp = Instantiate(t, this.transform.position, this.transform.rotation, this.transform);
			temp.name = t.name;
			//Set position of front Dynos.
			var Cpos = temp.transform.position;
			var CNewX = (FrontDynos.position - temp.transform.Find("Front Wheels").transform.position);
			temp.transform.position = new Vector3(CNewX.x, Cpos.y, CNewX.z);
			//Set position of back Dynos.
			var Dpos = BackDynos.position;
			var BW = temp.transform.Find("Back Wheels").transform.position;
			BackDynos.position = new Vector3(BW.x, Dpos.y, BW.z);
		}
	}
	//Function to clear all of the children on the Object
	public void Clear() {
		foreach (Transform child in this.transform) { Destroy(child.gameObject); }
	}



	/*
	 Need to optimize the code;
	 Spawn all objects at the beginning and just hide and unhide them.
	 */

}
