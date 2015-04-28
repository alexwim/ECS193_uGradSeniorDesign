using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour {
	public int health;
	public int maxHealth = 100;
	public Slider healthSlider;

	private bool isDead;

	private void Awake() {
		health = maxHealth;
		isDead = false;

		if (healthSlider != null) {
			healthSlider.value = health;
			healthSlider.maxValue = maxHealth;
		}
	}

	public void TakeDamage(int amount) {
		if (!isDead) {
			health -= amount;

			if (healthSlider != null) {
				healthSlider.value = health;
			}

			if(health <= 0) {
				Death();
			}
		}
	}

	private void Death() {
		isDead = true;
	}
}
