using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDManager : MonoBehaviour {

	[SerializeField]
	private Image healthBar;
	[SerializeField]
	private Text countdownTimer;

	private float timeLeftOnTimer = 0.0f;
	private bool isTimerRunning = false;

	// Update is called once per frame
	void Update () {
		if (IsTimerRunning()) {
			float newTimeLeft = Mathf.MoveTowards (timeLeftOnTimer, timeLeftOnTimer - Time.deltaTime, timeLeftOnTimer);
			countdownTimer.text = newTimeLeft.ToString ("F1");
			timeLeftOnTimer = newTimeLeft;
		}

		if (timeLeftOnTimer <= 0.0f) {
			countdownTimer.text = "";
			isTimerRunning = false;
		}
	}

	public void ReduceHealth(int amount, int health, int maxHealth) {
		healthBar.fillAmount = Mathf.MoveTowards(health, health-amount, health)/maxHealth;
	}

	public void IncreaseHealth(int amount, int health, int maxHealth) {
		healthBar.fillAmount = Mathf.MoveTowards(health, health+amount, maxHealth-health)/maxHealth;
	}

	public void StartCountdown(float startTime) {
		if (timeLeftOnTimer <= 0.0f) {
			timeLeftOnTimer = startTime;
			isTimerRunning = true;
		}
	}

	public bool IsTimerRunning() {
		return isTimerRunning;
	}
}
