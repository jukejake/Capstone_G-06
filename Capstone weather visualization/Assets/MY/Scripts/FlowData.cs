using UnityEngine;
using Sirenix.OdinInspector;


public class FlowData : SerializedMonoBehaviour {

	#region Variables
	[BoxGroup("General")]
	[HorizontalGroup("General/Start"), Range(0.5f, 20.0f), LabelWidth(60)]
	public float LifeTime = 5.0f;
	[HorizontalGroup("General/Start"), Range(0.01f, 0.2f), LabelWidth(70)]
	public float StartSize = 0.1f;
	[HorizontalGroup("General/Rate", Width = 0.15f), LabelWidth(30)]
	public int Max = 500;
	[HorizontalGroup("General/Rate"), Range(0, 200), LabelWidth(40)]
	public int Rate = 5;
	[HorizontalGroup("General/Speed", Width = 0.3f), LabelWidth(100)]
	public bool BasedOnSpeed = false;
	[HorizontalGroup("General/Speed"), Range(0.1f, 20.0f), LabelWidth(70)]
	public float AirSpeed = 1.0f;
	[BoxGroup("General")]
	public MegaFlow _MegaFlow;
	[BoxGroup("General")]
	public GameObject[] BigFlows;

	private WheelController WC;

	[BoxGroup("Small Flows"), Range(0.3f, 5.0f)]
	public float XScale = 0.3f;
	[BoxGroup("Small Flows"), Range(0.3f, 5.0f)]
	public float YScale = 0.3f;
	[BoxGroup("Small Flows")]
	public GameObject[] SmallFlows;
	#endregion

	#region Trail
	[ToggleGroup("Trails")]
	public bool Trails = true;
	private float T_LifeTime = 0.05f;
	private Material ParticleMaterial;
	private Material TrailMaterial;

	[HorizontalGroup("Trails/2", Width = 0.2f), LabelWidth(50)]
	public float Wind_L = 0.05f;
	[HorizontalGroup("Trails/2"), LabelWidth(60)]
	public Material Wind_PM;
	[HorizontalGroup("Trails/2"), LabelWidth(60)]
	public Material Wind_TM;

	[HorizontalGroup("Trails/3", Width = 0.2f), LabelWidth(50)]
	public float Rain_L = 0.05f;
	[HorizontalGroup("Trails/3"), LabelWidth(60)]
	public Material Rain_PM;
	[HorizontalGroup("Trails/3"), LabelWidth(60)]
	public Material Rain_TM;

	[HorizontalGroup("Trails/4", Width = 0.2f), LabelWidth(50)]
	public float Snow_L = 0.05f;
	[HorizontalGroup("Trails/4"), LabelWidth(60)]
	public Material Snow_PM;
	[HorizontalGroup("Trails/4"), LabelWidth(60)]
	public Material Snow_TM;
	#endregion

	#region Set Light data
	private void Start() {
		WC = FindObjectOfType<WheelController>();
	}
	private void FixedUpdate() {
		if (BasedOnSpeed) {
			_MegaFlow.Scale = AirSpeed = ((float)WC.Speed).Map(250,5);
		}
	}

	[ToggleGroup("Trails"), Button]
	public void SetMaterials(int _m = 0) {
		switch (_m) {
			case 0:
				T_LifeTime = Wind_L;
				if (Wind_PM != null) { ParticleMaterial = Wind_PM; }
				if (Wind_TM != null) { TrailMaterial = Wind_TM; }
				break;
			case 1:
				T_LifeTime = Rain_L;
				if (Rain_PM != null) { ParticleMaterial = Rain_PM;}
				if (Rain_TM != null) { TrailMaterial = Rain_TM; }
				break;
			case 2:
				T_LifeTime = Snow_L;
				if (Snow_PM != null) { ParticleMaterial = Snow_PM;}
				if (Snow_TM != null) { TrailMaterial = Snow_TM; }
				break;
			default:
				break;
		}
	}
	
	[Button]
	public void SetFlows1() {
		_MegaFlow.Scale = AirSpeed;
		foreach (var item in SmallFlows) {
			var _psm = item.GetComponent<ParticleSystem>().main;
			_psm.startSize = StartSize;
			_psm.startLifetime = LifeTime;
			_psm.maxParticles = Max;

			var _pse = item.GetComponent<ParticleSystem.EmissionModule>();
			_pse.rateOverTime = Rate;

			var _pss = item.GetComponent<ParticleSystem.ShapeModule>();
			_pss.scale = new Vector3(XScale, YScale, 1.0f);

			var _pst = item.GetComponent<ParticleSystem.TrailModule>();
			_pst.enabled = Trails;
			_pst.lifetime = T_LifeTime;

			if (ParticleMaterial != null && TrailMaterial != null) {
				var _psr = item.GetComponent<ParticleSystemRenderer>();
				_psr.material = ParticleMaterial;
				_psr.trailMaterial = TrailMaterial;
			}

			var _Child = item.transform.GetChild(0);
			if (_Child) {
				_Child.localScale = new Vector3(XScale*0.1f, 1.0f, YScale*0.1f);
			}
		}
		foreach (var item in BigFlows) {
			var _psm = item.GetComponent<ParticleSystem>().main;
			_psm.startSize = StartSize;
			_psm.startLifetime = LifeTime;
			_psm.maxParticles = Max;

			var _pse = item.GetComponent<ParticleSystem.EmissionModule>();
			_pse.rateOverTime = Rate;

			var _pst = item.GetComponent<ParticleSystem.TrailModule>();
			_pst.enabled = Trails;
			_pst.lifetime = T_LifeTime;

			if (ParticleMaterial != null && TrailMaterial != null) {
				var _psr = item.GetComponent<ParticleSystemRenderer>();
				_psr.material = ParticleMaterial;
				_psr.trailMaterial = TrailMaterial;
			}
		}
	}
	[Button]
	public void SetFlows2() {
		_MegaFlow.Scale = AirSpeed;
		foreach (var item in SmallFlows) {
			var _psm = item.GetComponent<ParticleSystem>().main;
			_psm.startSize = StartSize;
			_psm.startLifetime = LifeTime;
			_psm.maxParticles = Max;

			var _pse = item.GetComponent<ParticleSystem>().emission;
			_pse.rateOverTime = Rate;

			var _pss = item.GetComponent<ParticleSystem>().shape;
			_pss.scale = new Vector3(XScale, YScale, 1.0f);

			var _pst = item.GetComponent<ParticleSystem>().trails;
			_pst.enabled = Trails;
			_pst.lifetime = T_LifeTime;

			if (ParticleMaterial != null && TrailMaterial != null) {
				var _psr = item.GetComponent<ParticleSystemRenderer>();
				_psr.material = ParticleMaterial;
				_psr.trailMaterial = TrailMaterial;
			}

			var _Child = item.transform.GetChild(0);
			if (_Child) {
				_Child.localScale = new Vector3(XScale*0.1f, 1.0f, YScale*0.1f);
			}
		}
		foreach (var item in BigFlows) {
			var _psm = item.GetComponent<ParticleSystem>().main;
			_psm.startSize = StartSize;
			_psm.startLifetime = LifeTime;
			_psm.maxParticles = Max;
			
			var _pse = item.GetComponent<ParticleSystem>().emission;
			_pse.rateOverTime = Rate;

			var _pst = item.GetComponent<ParticleSystem>().trails;
			_pst.enabled = Trails;
			_pst.lifetime = T_LifeTime;

			if (ParticleMaterial != null && TrailMaterial != null) {
				var _psr = item.GetComponent<ParticleSystemRenderer>();
				_psr.material = ParticleMaterial;
				_psr.trailMaterial = TrailMaterial;
			}
		}
	}
	#endregion
}
