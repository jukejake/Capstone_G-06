/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//BUSI 4995U Capstone
////*/

using UnityEngine;
using Sirenix.OdinInspector;


public class FlowData : SerializedMonoBehaviour {

	#region General Variables
	//General Variables
	[BoxGroup("General")]
	[HorizontalGroup("General/Start"), Range(0.5f, 20.0f), LabelWidth(60)]
	public float LifeTime = 5.0f; //Life time of particales.
	[HorizontalGroup("General/Start"), Range(0.01f, 0.2f), LabelWidth(70)]
	public float StartSize = 0.1f; //Start size of particales.
	[HorizontalGroup("General/Rate", Width = 0.15f), LabelWidth(30)]
	public int MaxP = 500; //Max amount of particales.
	[HorizontalGroup("General/Rate"), Range(0, 200), LabelWidth(40)]
	public int Rate = 5; //Flow rate of particales.
	[HorizontalGroup("General/Speed", Width = 0.25f), LabelWidth(100)]
	public bool BasedOnSpeed = false; //Turn on speed based on a UI slider.
	[HorizontalGroup("General/Speed"), Range(0.1f, 20.0f), LabelWidth(70)]
	public float AirSpeed = 1.0f; //Speed based on a UI slider.
	[HorizontalGroup("General/Speed", Width = 0.15f), LabelWidth(40)]
	public float MaxS = 1.0f; //Speed based on a UI slider.
	[BoxGroup("General")]
	public MegaFlow _MegaFlow;
	[BoxGroup("General")]
	public bool BigFlowsOn = true;
	[BoxGroup("General")]
	public GameObject[] BigFlows;

	private WheelController WC;

	[BoxGroup("Small Flows"), Range(0.3f, 5.0f)]
	public float XScale = 0.3f; //X Size of particale emiter.
	[BoxGroup("Small Flows"), Range(0.3f, 5.0f)]
	public float YScale = 0.3f; //Y Size of particale emiter.
	[BoxGroup("Small Flows")]
	public bool SmallFlowsOn = true;
	[BoxGroup("Small Flows")]
	public GameObject[] SmallFlows;
	#endregion

	#region Trail Variables
	//General Variables
	[ToggleGroup("Trails")]
	public bool Trails = true;
	private float T_LifeTime = 0.05f; //Life time of Trails.
	private Material ParticleMaterial; //Material for the Particle.
	private Material TrailMaterial; //Material for the Trail.

	//Wind Variables
	[HorizontalGroup("Trails/2", Width = 0.2f), LabelWidth(50)]
	public float Wind_L = 0.05f;
	[HorizontalGroup("Trails/2"), LabelWidth(60)]
	public Material Wind_PM;
	[HorizontalGroup("Trails/2"), LabelWidth(60)]
	public Material Wind_TM;
	//Rain Variables
	[HorizontalGroup("Trails/3", Width = 0.2f), LabelWidth(50)]
	public float Rain_L = 0.05f;
	[HorizontalGroup("Trails/3"), LabelWidth(60)]
	public Material Rain_PM;
	[HorizontalGroup("Trails/3"), LabelWidth(60)]
	public Material Rain_TM;
	//Snow Variables
	[HorizontalGroup("Trails/4", Width = 0.2f), LabelWidth(50)]
	public float Snow_L = 0.05f;
	[HorizontalGroup("Trails/4"), LabelWidth(60)]
	public Material Snow_PM;
	[HorizontalGroup("Trails/4"), LabelWidth(60)]
	public Material Snow_TM;
	#endregion

	#region Set trail data
	private void Start() {
		//Find the WheelController to get the speed.
		WC = FindObjectOfType<WheelController>();
	}

	private bool SwitchOn = false;
	private void FixedUpdate() {
		if (AirSpeed <= 0.1f && !SwitchOn) {
			if (SmallFlowsOn) { foreach (var item in SmallFlows) { item.SetActive(SwitchOn); } }
			if (BigFlowsOn) { foreach (var item in BigFlows) { item.SetActive(SwitchOn); } }
			SwitchOn = true;
		}
		else if (AirSpeed > 0.1f && SwitchOn) {
			if (SmallFlowsOn) { foreach (var item in SmallFlows) { item.SetActive(SwitchOn); } }
			if (BigFlowsOn) { foreach (var item in BigFlows) { item.SetActive(SwitchOn); } }
			SwitchOn = false;
		}


		if (BasedOnSpeed) {
			_MegaFlow.Scale = AirSpeed = ((float)WC.Speed).Map(250, MaxS);
		}
	}

	[ToggleGroup("Trails"), Button]
	//Function to remap the Materials to the desired weather
	public void SetMaterials(int _m = 0) {
		switch (_m) {
			case 0: //Wind
				T_LifeTime = Wind_L;
				if (Wind_PM != null) { ParticleMaterial = Wind_PM; }
				if (Wind_TM != null) { TrailMaterial = Wind_TM; }
				break;
			case 1: //Rain
				T_LifeTime = Rain_L;
				if (Rain_PM != null) { ParticleMaterial = Rain_PM;}
				if (Rain_TM != null) { TrailMaterial = Rain_TM; }
				break;
			case 2: //Snow
				T_LifeTime = Snow_L;
				if (Snow_PM != null) { ParticleMaterial = Snow_PM;}
				if (Snow_TM != null) { TrailMaterial = Snow_TM; }
				break;
			default: //Do nothing
				break;
		}
	}
	
	[Button]
	//One option to accsess the ParticleSystem that doesn't always work.
	public void SetFlows1() {
		_MegaFlow.Scale = AirSpeed;
		//Set all the small flows
		foreach (var item in SmallFlows) {
			var _psm = item.GetComponent<ParticleSystem>().main;
			_psm.startSize = StartSize;
			_psm.startLifetime = LifeTime;
			_psm.maxParticles = MaxP;

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
		//Set all the big flows
		foreach (var item in BigFlows) {
			var _psm = item.GetComponent<ParticleSystem>().main;
			_psm.startSize = StartSize;
			_psm.startLifetime = LifeTime;
			_psm.maxParticles = MaxP;

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
	//One option to access the ParticleSystem that doesn't always work.
	public void SetFlows2() {
		_MegaFlow.Scale = AirSpeed;
		//Set all the small flows
		foreach (var item in SmallFlows) {
			var _psm = item.GetComponent<ParticleSystem>().main;
			_psm.startSize = StartSize;
			_psm.startLifetime = LifeTime;
			_psm.maxParticles = MaxP;

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
		//Set all the big flows
		foreach (var item in BigFlows) {
			var _psm = item.GetComponent<ParticleSystem>().main;
			_psm.startSize = StartSize;
			_psm.startLifetime = LifeTime;
			_psm.maxParticles = MaxP;
			
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
