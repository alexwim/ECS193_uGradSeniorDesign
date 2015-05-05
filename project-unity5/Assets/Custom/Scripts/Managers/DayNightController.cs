using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour {
	private const float SECONDS_PER_DAY = 120.0f;

	public Light sun;

	private float baseIntensity;
	private float currentTime = 0.25f;
	private float timeMultiplier = 2f;

	private void Awake() {
		baseIntensity = sun.intensity;
	}

	// Update is called once per frame
	private void Update () {
		UpdateSunRotation ();
		UpdateSunIntensity ();

		currentTime += (Time.deltaTime / SECONDS_PER_DAY) * timeMultiplier;

		if (currentTime >= 1) {
			currentTime = 0;
		}
	}

	private void UpdateSunRotation() {
		sun.transform.localRotation = Quaternion.Euler ((currentTime * 360.0f) - 90, 170, 0);
	}

	private void UpdateSunIntensity() {
		float intensityMultiplier = 1.0f;

		if( (currentTime <= 0.23f) || (currentTime >= 0.75f) ) {
			intensityMultiplier = 0;
		}
		else if(currentTime <= 0.25f) {
			intensityMultiplier = Mathf.Clamp01((currentTime - 0.23f) * (1 / 0.02f));
		}
		else if(currentTime >= 0.73f) {
			intensityMultiplier = Mathf.Clamp01(1 - ((currentTime - 0.73f) * (1 / 0.02f)));
		}

		sun.intensity = baseIntensity * intensityMultiplier;
	}
}
