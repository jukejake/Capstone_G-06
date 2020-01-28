/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class SpawnObjects : SerializedMonoBehaviour {

    #region Variables
    public static SpawnObjects instance = null;
    [OdinSerialize]
	//A dictionary that has all the names and Gameobjects of the cars
	public Dictionary<string, GameObject> IDTable = new Dictionary<string, GameObject>();
	public Dictionary<string, SpawnItem> ShowTable = new Dictionary<string, SpawnItem>();
	public Transform FrontDynos;
	public Transform BackDynos;
	public Transform BackDynoCases;
	[InfoBox("True for Destroy | False for Hide")]
	public bool DestroyOrHide = true;
    #endregion

    #region Functions
    private void Awake() {
        instance = this;
    }
    private void Start() {
		if (!DestroyOrHide) { SpawnAll(); }
		//Invoke("HideAll", 0.50f);
	}

	[Button]
	public void SwitchMethod() {
		//Switch method
		DestroyOrHide = !DestroyOrHide;
		//Remove children
		Remove();
		//If Hide
		if (!DestroyOrHide) { SpawnAll(); }
		else { ShowTable.Clear(); }
	}

	//Function to show all vehicles.
	private void SpawnAll() {
		foreach (var item in IDTable) {
			//Spawn the new car.
			GameObject car = Instantiate(item.Value, this.transform.position, this.transform.rotation, this.transform);
			car.name = item.Value.name;
			//Set position of front Dynos.
			if (car.transform.Find("Front Wheels")) { 
				var Cpos = car.transform.position;
				var CNewX = (FrontDynos.position - car.transform.Find("Front Wheels").transform.position);
				car.transform.position = new Vector3(CNewX.x, Cpos.y, CNewX.z);
			}
			//Set position of back Dynos.
			if (car.transform.Find("Front Wheels")) {
				var Dpos = BackDynos.position;
				var BW = car.transform.Find("Back Wheels").transform.position;
				//Use world position; to get the right position.
				BackDynos.position = new Vector3(BW.x, Dpos.y, BW.z);
				//Use local position; as it is now in the correct position.
				ShowTable.Add(item.Key, new SpawnItem(false, car, BackDynos.position)); //localPosition
			}
			else { ShowTable.Add(item.Key, new SpawnItem(false, car, Vector3.zero)); }
			car.SetActive(false);
		}
		BackDynos.position = new Vector3(0, BackDynos.position.y, 0);
	}

	//Function to show a specific vehicle.
	public void ShowVehicle(string key) {

        //Only spawn 1 at a time.
        if (SpawnAnimation.instance.stage != 0) { return; }
		//Instantiate Destroy Method
		if (DestroyOrHide) {
			GameObject t;
			if (IDTable.TryGetValue(key, out t)) {
				//Remove the previous car.
				Remove();
				//Spawn the new car.
				GameObject temp = Instantiate(t, this.transform.position, this.transform.rotation, this.transform);
				temp.name = t.name;
				//Set position of front Dynos.
				var Cpos = temp.transform.position;
                if (temp.transform.Find("Front Wheels"))
                {
                    var CNewX = (FrontDynos.position - temp.transform.Find("Front Wheels").transform.position);
                    temp.transform.position = new Vector3(CNewX.x, Cpos.y, CNewX.z);
                }
				//Set position of back Dynos.
				var Dpos = BackDynos.position;
                if (temp.transform.Find("Back Wheels"))
                {
                    var BW = temp.transform.Find("Back Wheels").transform.position;
                    BackDynos.position = new Vector3(BW.x, Dpos.y, BW.z);
                    BackDynoCases.position = new Vector3(BW.x, BackDynoCases.position.y, BW.z);
                }
			}
		}
		//Hide Un-hide Method
		else { 
			HideAll();
			SpawnItem t;
			if (ShowTable.TryGetValue(key, out t)) {
				t.Selected = true;
				//Un-hide the vehicle.
				t.Vehicle.SetActive(true);
				//Set position of back Dynos.
				BackDynos.position = t.BackDynoPos; //localPosition
				BackDynoCases.position = new Vector3(t.BackDynoPos.x, BackDynoCases.position.y, t.BackDynoPos.z);
			}
		}

        //Play spawning animation
        SpawnAnimation.instance.Play();
    }

	//Function to clear all of the children on the Object
	public void Clear() {
		if (!DestroyOrHide) { HideAll(); }
		else { Remove(); }
	}

	//Function to remove all of the children on the Object
	private void Remove() {
		foreach (Transform child in this.transform) { Destroy(child.gameObject); }
	}

	//Function to hide all of the children on the Object
	private void HideAll() {
		foreach (var item in ShowTable) {
			var key = item.Key;
			SpawnItem t;
			if (ShowTable.TryGetValue(key, out t)) {
				t.Selected = false;
				t.Vehicle.SetActive(false);
			}
		}
	}

	public struct SpawnItem {
		public bool Selected;
		public GameObject Vehicle;
		public Vector3 BackDynoPos;

		public SpawnItem(bool v, GameObject temp, Vector3 position) : this() {
			this.Selected = v;
			this.Vehicle = temp;
			this.BackDynoPos = position;
		}
	}
	#endregion

}
