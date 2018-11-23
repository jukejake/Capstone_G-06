using UnityEngine;
using Sirenix.OdinInspector;

public class FlowData : SerializedMonoBehaviour {

	#region Variables
	[Range(0.01f, 0.2f)]
	public float StartSize = 0.1f;
	[Range(1, 50)]
	public int Rate = 5;

	public bool Trail = true;

	public GameObject[] Flows;
	#endregion

	#region Set Light data
	[Button]
	public void SetFlows() {
		foreach (var item in Flows) {

			var PS = item.GetComponent<ParticleSystem>();

			var why0 = PS.main;
			why0.startSize = StartSize;
			
			var why1 = item.GetComponent<ParticleSystem.EmissionModule>();
			why1.rateOverDistance = Rate;

			var why2 = item.GetComponent<ParticleSystem.TrailModule>();
			why2.enabled = Trail;
		}
	}
	#endregion
}
