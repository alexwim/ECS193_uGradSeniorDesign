using UnityEngine;
using System.Collections;

public class Spawnpoint : MonoBehaviour {

	public bool isCircularArea = false;
	public float CircularRadius = 0.0f;
	
	public bool isRectangularArea = false;
	public float RectangularXDist = 0.0f;
	public float RectangularZDist = 0.0f;

	private GameObject enemy;

	void Start () {
		enemy = GetComponentInParent<EnemyManager> ().enemy;
	}

	public void Spawn (int deltaHealth, int deltaDamage) {
		Vector3 spawnPos = GetSpawnLocation ();

		GameObject newenemy = Instantiate (enemy, spawnPos, GetRotationFrom(spawnPos)) as GameObject;
		newenemy.transform.parent = gameObject.transform.parent;
		newenemy.GetComponent<EnemyHealth> ().health += deltaHealth;
		newenemy.GetComponent<EnemyAttack> ().damage += deltaDamage;
	}

	private Vector3 GetSpawnLocation() {
		if (isCircularArea) {
			float r = Random.Range (0.0f, CircularRadius);
			float theta = Random.Range (0.0f, 360.0f);
			return new Vector3(Mathf.Cos(theta) * r, 0, Mathf.Sin(theta) * r);
		} else if (isRectangularArea) {
			return new Vector3(Random.Range(0.0f, RectangularXDist) - RectangularXDist/2, 0, Random.Range(0.0f, RectangularZDist) - RectangularZDist/2);
		} else {
			return transform.position;
		}
	}

	private Quaternion GetRotationFrom(Vector3 fromPos) {
		return Quaternion.FromToRotation (fromPos, new Vector3 (0, 0, 0));
	}
}
