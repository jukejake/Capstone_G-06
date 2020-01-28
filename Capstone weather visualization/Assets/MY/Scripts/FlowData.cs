/*////
//Written by Jacob Rosengren
//Date: 2018~2019
//Updated: January 2020
//BUSI 4995U Capstone
////*/

using UnityEngine;
using Sirenix.OdinInspector;


public class FlowData : SerializedMonoBehaviour {

    #region General Variables
    public static FlowData instance = null;

    //General Variables
    [BoxGroup("General")]
	[HorizontalGroup("General/Start"), Range(0.5f, 20.0f), LabelWidth(60)]
	public float LifeTime = 5.0f; //Life time of particles.
	[HorizontalGroup("General/Start"), Range(0.01f, 0.2f), LabelWidth(70)]
	public float StartSize = 0.1f; //Start size of particles.
    [HorizontalGroup("General/Rate", Width = 0.15f), LabelWidth(30)]
	public int MaxP = 500; //Max amount of particles.
    [HorizontalGroup("General/Rate"), Range(0, 200), LabelWidth(40)]
	public int Rate = 5; //Flow rate of particles.
    [HorizontalGroup("General/Speed", Width = 0.25f), LabelWidth(100)]
	public bool BasedOnSpeed = false; //Turn on speed based on a UI slider.
	[HorizontalGroup("General/Speed"), Range(0.1f, 20.0f), LabelWidth(70)]
	public float AirSpeed = 0.0f; //Speed based on a UI slider.
	[HorizontalGroup("General/Speed", Width = 0.15f), LabelWidth(40)]
	public float MaxS = 1.0f; //Speed based on a UI slider.
	[BoxGroup("General")]
	public MegaFlow _MegaFlow;
	[BoxGroup("General")]
	public int ActiveFlow; //0 = Smoke, 1 = Rain, 2 = Snow

	[BoxGroup("BigFlows")]
	public bool BigFlowsOn = true;
	[BoxGroup("BigFlows")]
	public GameObject BigSmokeFlow;
	[BoxGroup("BigFlows")]
	public GameObject BigRainFlow;
	[BoxGroup("BigFlows")]
	public GameObject BigSnowFlow;
	

	[BoxGroup("Small Flows"), Range(0.1f, 5.0f)]
	public float XScale = 0.3f; //X Size of particle emitter.
    [BoxGroup("Small Flows"), Range(0.1f, 5.0f)]
	public float YScale = 0.3f; //Y Size of particle emitter.
    [BoxGroup("Small Flows")]
	public bool SmallFlowsOn = true;
	[BoxGroup("Small Flows")]
	public GameObject[] SmallSmokeFlows;
	[BoxGroup("Small Flows")]
	public GameObject[] SmallRainFlows;
	[BoxGroup("Small Flows")]
	public GameObject[] SmallSnowFlows;
	#endregion

	#region Functions

	private bool SwitchOn = true;

    private void Awake() {
        instance = this;
    }

	private void Start() {
		//AirSpeed = 0;
		//SwitchOn = true;
		//SetSpeed(0);
	}

	private void SwitchWeather(bool state) {
		if (SmallFlowsOn) {
				 if (ActiveFlow == 0) { foreach (var item in SmallSmokeFlows){ item.SetActive(state); } }
			else if (ActiveFlow == 1) { foreach (var item in SmallRainFlows) { item.SetActive(state); } }
			else if (ActiveFlow == 2) { foreach (var item in SmallSnowFlows) { item.SetActive(state); } }
		}
		if (BigFlowsOn) {
				 if (ActiveFlow == 0) { BigSmokeFlow.SetActive(state);}
			else if (ActiveFlow == 1) { BigRainFlow.SetActive(state); }
			else if (ActiveFlow == 2) { BigSnowFlow.SetActive(state); }
		}
	}

	public void SetSpeed(float value) {
		
		if (BasedOnSpeed) {
			_MegaFlow.Scale = AirSpeed = (value).Map(250, MaxS);
		}
		//Set the desired weather when air speeds are high enough.
		if (AirSpeed <= 0.1f && !SwitchOn) {
			SwitchWeather(SwitchOn);
			SwitchOn = true;
		}
		//Switch off the weather when air speeds are not high enough.
		else if (AirSpeed > 0.1f && SwitchOn) {
			SwitchWeather(SwitchOn);
			SwitchOn = false;
		}
	}
	
	[Button]
	public void SwitchSize() {
		if (SmallFlowsOn) {
			SmallFlowsOn = false;
			BigFlowsOn = true;
				 if (ActiveFlow == 0) { foreach (var item in SmallSmokeFlows){ item.SetActive(false); } BigSmokeFlow.SetActive(true); }
			else if (ActiveFlow == 1) { foreach (var item in SmallRainFlows) { item.SetActive(false); }  BigRainFlow.SetActive(true); }
			else if (ActiveFlow == 2) { foreach (var item in SmallSnowFlows) { item.SetActive(false); }  BigSnowFlow.SetActive(true); }
		}
		else if (BigFlowsOn) {
			BigFlowsOn = false;
			SmallFlowsOn = true;
				 if (ActiveFlow == 0) { BigSmokeFlow.SetActive(false); foreach (var item in SmallSmokeFlows){ item.SetActive(true); } }
			else if (ActiveFlow == 1) {  BigRainFlow.SetActive(false); foreach (var item in SmallRainFlows) { item.SetActive(true); } }
			else if (ActiveFlow == 2) {  BigSnowFlow.SetActive(false); foreach (var item in SmallSnowFlows) { item.SetActive(true); } }
		}
	}
	[Button]
	public void ClearWeather() {
		SwitchWeather(false);
	}
	[Button]
	//Function to set which particle system should be active
	public void SetWeather(int _m = 0) {
		switch (_m) {
			case 0: //Wind
                SwitchWeather(false);
                if (ActiveFlow != 0) {
					ActiveFlow = 0;
					SmallFlowsOn = true;
					BigFlowsOn = false;
                    if (AirSpeed <= 0.1f)  { SwitchWeather(false); }
                    else { SwitchWeather(true); }
                }
				else { ActiveFlow = -1; }
				break;
			case 1: //Rain
                SwitchWeather(false);
                if (ActiveFlow != 1) {
					ActiveFlow = 1;
					SmallFlowsOn = false;
					BigFlowsOn = true;
                    if (AirSpeed <= 0.1f) { SwitchWeather(false); }
                    else { SwitchWeather(true); }
                }
				else { ActiveFlow = -1; }
				break;
			case 2: //Snow
                SwitchWeather(false);
                if (ActiveFlow != 2) {
					ActiveFlow = 2;
					SmallFlowsOn = false;
					BigFlowsOn = true;
                    if (AirSpeed <= 0.1f) { SwitchWeather(false); }
                    else { SwitchWeather(true); }
                }
				else { ActiveFlow = -1; }
				break;
			default: //Do nothing
				break;
		}
	}



	public void SetEmissionRate(float rate) {
		_MegaFlow.Scale = AirSpeed;
		//Set all the small flows
		foreach (var item in SmallSmokeFlows) {
			var _pse = item.GetComponent<ParticleSystem>().emission;
			_pse.rateOverTime = rate;
		}
		foreach (var item in SmallRainFlows) {
			var _pse = item.GetComponent<ParticleSystem>().emission;
			_pse.rateOverTime = rate;
		}
		foreach (var item in SmallSnowFlows) {
			var _pse = item.GetComponent<ParticleSystem>().emission;
			_pse.rateOverTime = rate;
		}
		var B_pse1 = BigSmokeFlow.GetComponent<ParticleSystem>().emission;
		B_pse1.rateOverTime = (rate * 2);
		var B_pse2 = BigRainFlow.GetComponent<ParticleSystem>().emission;
		B_pse2.rateOverTime = (rate * 2);
		var B_pse3 = BigSnowFlow.GetComponent<ParticleSystem>().emission;
		B_pse3.rateOverTime = (rate * 2);
	}
    public void SetLifeTime(float rate) {
		_MegaFlow.Scale = AirSpeed;
		//Set all the small flows
		foreach (var item in SmallSmokeFlows) {
			var _psm = item.GetComponent<ParticleSystem>().main;
            _psm.startLifetime = rate;
		}
		foreach (var item in SmallRainFlows) {
			var _psm = item.GetComponent<ParticleSystem>().main;
            _psm.startLifetime = rate;
		}
		foreach (var item in SmallSnowFlows) {
			var _psm = item.GetComponent<ParticleSystem>().main;
            _psm.startLifetime = rate;
        }
        var B_psm1 = BigSmokeFlow.GetComponent<ParticleSystem>().main;
        B_psm1.startLifetime = rate;
        var B_psm2 = BigSmokeFlow.GetComponent<ParticleSystem>().main;
        B_psm2.startLifetime = rate;
        var B_psm3 = BigSmokeFlow.GetComponent<ParticleSystem>().main;
        B_psm3.startLifetime = rate;
	}
	[Button]
	public void SetSmallSmokeFlows() {
		_MegaFlow.Scale = AirSpeed;
		foreach (var item in SmallSmokeFlows)  {
			var _psm = item.GetComponent<ParticleSystem>().main;
			_psm.startSize = StartSize;
			_psm.startLifetime = LifeTime;
			_psm.maxParticles = MaxP;

			var _pse = item.GetComponent<ParticleSystem>().emission;
			_pse.rateOverTime = Rate;

			var _pss = item.GetComponent<ParticleSystem>().shape;
			_pss.scale = new Vector3(XScale, YScale, 1.0f);

			var _Child = item.transform.GetChild(0);
			if (_Child) {
				_Child.localScale = new Vector3(XScale*0.1f, 1.0f, YScale*0.1f);
			}
		}
	}
	[Button]
	public void SetSmallRainFlows() {
		_MegaFlow.Scale = AirSpeed;
		foreach (var item in SmallRainFlows)  {
			var _psm = item.GetComponent<ParticleSystem>().main;
			_psm.startSize = StartSize;
			_psm.startLifetime = LifeTime;
			_psm.maxParticles = MaxP;

			var _pse = item.GetComponent<ParticleSystem>().emission;
			_pse.rateOverTime = Rate;

			var _pss = item.GetComponent<ParticleSystem>().shape;
			_pss.scale = new Vector3(XScale, YScale, 1.0f);

			var _Child = item.transform.GetChild(0);
			if (_Child) {
				_Child.localScale = new Vector3(XScale*0.1f, 1.0f, YScale*0.1f);
			}
		}
	}
	[Button]
	public void SetSmallSnowFlows() {
		_MegaFlow.Scale = AirSpeed;
		foreach (var item in SmallSnowFlows)  {
			var _psm = item.GetComponent<ParticleSystem>().main;
			_psm.startSize = StartSize;
			_psm.startLifetime = LifeTime;
			_psm.maxParticles = MaxP;

			var _pse = item.GetComponent<ParticleSystem>().emission;
			_pse.rateOverTime = Rate;

			var _pss = item.GetComponent<ParticleSystem>().shape;
			_pss.scale = new Vector3(XScale, YScale, 1.0f);

			var _Child = item.transform.GetChild(0);
			if (_Child) {
				_Child.localScale = new Vector3(XScale*0.1f, 1.0f, YScale*0.1f);
			}
		}
	}
	[Button]
	public void SetBigSmokeFlow() {
		_MegaFlow.Scale = AirSpeed;
		var _psm = BigSmokeFlow.GetComponent<ParticleSystem>().main;
		_psm.startSize = StartSize;
		_psm.startLifetime = LifeTime;
		_psm.maxParticles = MaxP;

		var _pse = BigSmokeFlow.GetComponent<ParticleSystem>().emission;
		_pse.rateOverTime = Rate;
	}
	[Button]
	public void SetBigRainFlow() {
		_MegaFlow.Scale = AirSpeed;
		var _psm = BigRainFlow.GetComponent<ParticleSystem>().main;
		_psm.startSize = StartSize;
		_psm.startLifetime = LifeTime;
		_psm.maxParticles = MaxP;

		var _pse = BigRainFlow.GetComponent<ParticleSystem>().emission;
		_pse.rateOverTime = Rate;
	}
	[Button]
	public void SetBigSnowFlow() {
		_MegaFlow.Scale = AirSpeed;
		//Set all the small flows
		var _psm = BigSnowFlow.GetComponent<ParticleSystem>().main;
		_psm.startSize = StartSize;
		_psm.startLifetime = LifeTime;
		_psm.maxParticles = MaxP;

		var _pse = BigSnowFlow.GetComponent<ParticleSystem>().emission;
		_pse.rateOverTime = Rate;
	}
	#endregion
}
