using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public GameObject enemy;
	public float spawnTime;
	public Transform[] spawnPoints;

	private void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	private void Spawn() {
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
	}
}
