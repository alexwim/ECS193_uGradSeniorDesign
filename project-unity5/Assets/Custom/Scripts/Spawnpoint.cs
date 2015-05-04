using UnityEngine;
using System.Collections;

public class Spawnpoint : MonoBehaviour {

	private GameObject enemy;

	void Start () {
		enemy = GetComponentInParent<EnemyManager> ().enemy;
	}

	public void Spawn (int deltaHealth, int deltaDamage) {
		GameObject newenemy = Instantiate (enemy, GetPosition(), transform.rotation) as GameObject;
		newenemy.transform.parent = gameObject.transform.parent;
		newenemy.GetComponent<EnemyHealth> ().health += deltaHealth;
		newenemy.GetComponent<EnemyAttack> ().damage += deltaDamage;
	}

	private Vector3 GetPosition() {
		return transform.position;
	}
}
