using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {
	public static float Map(this float value, float fromMin, float fromMax, float toMin, float toMax) {
		return  (((toMax - toMin) * ((value - fromMin) / (fromMax - fromMin))) + toMin);
	}
	public static float Map(this float value, float fromMax, float toMax) {
		return  (((toMax - 0) * ((value - 0) / (fromMax - 0))) + 0);
	}
}