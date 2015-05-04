using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	public float rate = 1.0f;
	public int damage = 5;
	
	private PlayerHealth playerHealth;
	private float timer;
	private bool playerInRange = false;

	private void Awake() {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			playerInRange = true;
		}
	}

	private void OnTriggerExit(Collider other) {
		if(other.gameObject.CompareTag("Player")) {
			playerInRange = false;
		}
	}

	private void Update() {
		timer += Time.deltaTime;

		if(timer >= rate && playerInRange) {
			Attack();
		}
	}

	private void Attack() {
		timer = 0f;
		playerHealth.TakeDamage (damage);
	}
}
