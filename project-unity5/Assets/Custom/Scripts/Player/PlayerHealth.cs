using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	[HideInInspector]
	public int health;
	public int maxHealth = 100;

	private bool isDead;
	
	private HUDManager hud;

	void Start() {
		hud = GameObject.Find ("/LeapOVRPlayerController/OVRCameraRig/CenterEyeAnchor/HUD").GetComponent<HUDManager> ();
		health = maxHealth;
		isDead = false;
	}

	public void TakeDamage(int amount) {
		if (!isDead) {
			hud.ReduceHealth(amount, health, maxHealth);
			health -= amount;

			if(health <= 0) {
				Death();
			}
		}
	}

	public void HealDamage(int amount) {
		if (!isDead) {
			hud.IncreaseHealth(amount, health, maxHealth);
			health = (int) Mathf.MoveTowards(health, health + amount, maxHealth - health);
		}
	}

	private void Death() {
		isDead = true;
	}
}
