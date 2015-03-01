using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	public float rate = 0.5f;
	public int damage = 10;

	private GameObject player;
	private PlayerHealth playerHealth;
	private bool playerInRange;
	private float timer;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject == player) {
			playerInRange = true;
		}
	}

	private void OnTriggerExit(Collider other) {
		if(other.gameObject == player) {
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
