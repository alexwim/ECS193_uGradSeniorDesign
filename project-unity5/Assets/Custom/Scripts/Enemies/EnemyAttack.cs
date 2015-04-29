using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	public float rate = 1f;
	public int damage = 5;
	public float range = 0.2f;

	private GameObject player;
	private PlayerHealth playerHealth;
	private Collider playerCollider;
	private float timer;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		playerCollider = player.GetComponent<Collider> ();
	}

	private bool IsInRange() {
		float currentDistance = Vector3.Distance (playerCollider.ClosestPointOnBounds (transform.position), transform.position) - + transform.GetComponent<CapsuleCollider> ().radius;
		return range > currentDistance;
	}

	private void Update() {
		timer += Time.deltaTime;

		if(timer >= rate && IsInRange()) {
			Attack();
		}
	}

	private void Attack() {
		timer = 0f;

		playerHealth.TakeDamage (damage);
	}
}
