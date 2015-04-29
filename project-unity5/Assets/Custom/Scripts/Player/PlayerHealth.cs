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
			health -= amount;
			healthBar.fillAmount = Mathf.MoveTowards(health, health-amount, health)/maxHealth;

			if(health <= 0) {
				Death();
			}
		}
	}

	private void Death() {
		isDead = true;
	}
}
