using UnityEngine;
using Sirenix.OdinInspector;

public class FlowData : SerializedMonoBehaviour {

	#region Variables
	[Range(0.01f, 0.2f)]
	public float StartSize = 0.1f;
	[Range(1, 200)]
	public int Rate = 5;
	[Range(0.3f, 5.0f)]
	public float XScale = 0.3f;
	[Range(0.3f, 5.0f)]
	public float YScale = 0.3f;
	[Range(0.1f, 20.0f)]
	public float Speed = 1.0f;

	public bool Trail = true;

	public MegaFlow _MegaFlow;

	public GameObject[] Flows;
	#endregion

	#region Set Light data
	[Button]
	public void SetFlows1() {
		foreach (var item in Flows) {
			
			var _psm = item.GetComponent<ParticleSystem>().main;
			_psm.startSize = StartSize;
			
			var _pse = item.GetComponent<ParticleSystem.EmissionModule>();
			_pse.rateOverTime = Rate;

			var _pss = item.GetComponent<ParticleSystem.ShapeModule>();
			_pss.scale = new Vector3(XScale, YScale, 1.0f);

			var _pst = item.GetComponent<ParticleSystem.TrailModule>();
			_pst.enabled = Trail;

			var _Child = item.transform.GetChild(0);
			if (_Child) {
				_Child.localScale = new Vector3(XScale*0.1f, 1.0f, YScale*0.1f);
			}
		}
	}
	[Button]
	public void SetFlows2() {
		_MegaFlow.Scale = Speed;
		foreach (var item in Flows) {
			
			var _psm = item.GetComponent<ParticleSystem>().main;
			_psm.startSize = StartSize;
			
			var _pse = item.GetComponent<ParticleSystem>().emission;
			_pse.rateOverTime = Rate;

			var _pss = item.GetComponent<ParticleSystem>().shape;
			_pss.scale = new Vector3(XScale, YScale, 1.0f);

			var _pst = item.GetComponent<ParticleSystem>().trails;
			_pst.enabled = Trail;

			var _Child = item.transform.GetChild(0);
			if (_Child) {
				_Child.localScale = new Vector3(XScale*0.1f, 1.0f, YScale*0.1f);
			}
		}
	}
	#endregion
}
