/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;
public static class ExtensionMethods {
	//Map float function, to map a value from one range to another, using Floats.
	public static float Map(this float value, float fromMin, float fromMax, float toMin, float toMax) {
		return  (((toMax - toMin) * ((value - fromMin) / (fromMax - fromMin))) + toMin);
	}
	//Map float function, to map a value from one range to another, using Floats with both minumims set to 0.
	public static float Map(this float value, float fromMax, float toMax) {
		return  (((toMax - 0) * ((value - 0) / (fromMax - 0))) + 0);
	}
	//Map float function, to map a value from one range to another, using Vector2s.
	public static float Map(this float value, Vector2 from, Vector2 to) {
		return  (((to.y - to.x) * ((value - from.x) / (from.y - from.x))) + to.x);
	}
}