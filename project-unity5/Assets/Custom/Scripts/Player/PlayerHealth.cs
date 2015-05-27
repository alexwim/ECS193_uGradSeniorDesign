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

	public void Reset() {
		hud.SetHealthTo (maxHealth, maxHealth);
		health = maxHealth;
		isDead = false;
	}

	public void TakeDamage(int amount) {
		if (!isDead) {
			health = (int) Mathf.MoveTowards(health, health - amount, health);
			hud.SetHealthTo(health, maxHealth);

			if(health <= 0) {
				Death();
			}
		}
	}

	public void HealDamage(int amount) {
		if (!isDead) {
			health = (int) Mathf.MoveTowards(health, health + amount, maxHealth - health);
			hud.SetHealthTo(health, maxHealth);
		}
	}

	private void Death() {
		isDead = true;
	}

	public bool IsDead() {
		return isDead;
	}
}
