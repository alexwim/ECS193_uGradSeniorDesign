using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int health;
	public int maxHealth = 100;

	private bool isDead;

	private void Awake() {
		health = maxHealth;
		isDead = false;
	}

	public void TakeDamage(int amount) {
		health -= amount;
	}

	private void Death() {
		isDead = true;
	}
}
