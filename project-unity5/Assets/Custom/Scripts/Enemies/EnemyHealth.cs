using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int health = 100;

	public void TakeDamage(int damage) {
		health -= damage;

		Debug.Log ("damage: " + damage + "; health=" + health);

		if (health <= 0) {
			death();
		}
	}

	private void death() {
		Destroy (gameObject);
	}
}