using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour {
	[HideInInspector]
	public int health;
	public int maxHealth = 100;
	public Image healthBar;

	private bool isDead;

	private void Awake() {
		health = maxHealth;
		isDead = false;
	}

	public void TakeDamage(int amount) {
		if (!isDead) {
			healthBar.fillAmount = Mathf.MoveTowards(health, health-amount, health)/maxHealth;
			health -= amount;

			if(health <= 0) {
				Death();
			}
		}
	}

	public void HealDamage(int amount) {
		if (!isDead) {
			healthBar.fillAmount = Mathf.MoveTowards(health, health+amount, maxHealth-health)/maxHealth;
			health += amount;
		}
	}

	private void Death() {
		isDead = true;
	}
}
