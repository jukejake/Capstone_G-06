/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/
using UnityEngine;

public class LightingArrayConnecters : MonoBehaviour {
    //This is used to reinstall the connecter that connect the lights to the ceiling.
    //This is done because they F-up when you switch between the menus and they get hidden.
    //Once this gets called it will delete itself.

	#region Variables
	public Transform CF; //Front
	public Transform CL; //Left
	public Transform CM; //Middle
	public Transform CR; //Right
	public Transform CB; //Back

	public MimicPosition[] MF; //Front
	public MimicPosition[] ML; //Left
	public MimicPosition[] MM; //Middle
	public MimicPosition[] MR; //Right
	public MimicPosition[] MB; //Back
	#endregion

	#region Functions
	private void Start () {
		foreach (var item in MF) { item.ObjectToMimic = CF; }
		foreach (var item in ML) { item.ObjectToMimic = CL; }
		foreach (var item in MM) { item.ObjectToMimic = CM; }
		foreach (var item in MR) { item.ObjectToMimic = CR; }
		foreach (var item in MB) { item.ObjectToMimic = CB; }
		Destroy(this,0.1f);
	}
	#endregion
}
