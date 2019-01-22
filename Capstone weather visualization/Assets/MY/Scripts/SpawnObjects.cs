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

	//Function that will spawn a GameObject based on a key.
	public void SpawnObject(string key) {
		GameObject t;
		if (IDTable.TryGetValue(key, out t)) {
			Clear();
			GameObject temp = Instantiate(t, this.transform.position, this.transform.rotation, this.transform);
			temp.name = t.name;
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
