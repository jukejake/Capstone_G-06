using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class SpawnObjects : SerializedMonoBehaviour {

	[OdinSerialize]
	public Dictionary<string, GameObject> IDTable = new Dictionary<string, GameObject>();

	public void SpawnObject(string key) {
		GameObject t;
		if (IDTable.TryGetValue(key, out t)) {
			foreach (Transform child in this.transform) { Destroy(child.gameObject); }
			GameObject temp = Instantiate(t, this.transform.position, this.transform.rotation, this.transform);
			temp.name = t.name;
		}
	}
}
