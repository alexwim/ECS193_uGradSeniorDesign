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

	public void UpdateSunRotation() {
		sun.transform.Rotate (0.1f * timeMultiplier,0,0);
		//sun.transform.localRotation = Quaternion.Euler ((currentTime * 360.0f) - 90, 170, 0);
	}
}
