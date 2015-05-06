using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int health = 100;

	public void TakeDamage(int damage) {
		health = (int) Mathf.MoveTowards (health, health - damage, health);

		Debug.Log ("damage: " + damage + "; health=" + health);

		if (health <= 0) {
			death();
		}
	}

	private void death() {
		gameObject.GetComponentInParent<EnemyManager>().enemiesAlive -= 1;
		Destroy (gameObject);
	}
}