using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
	public GameObject enemy;
	public float spawnTime;
	public Transform[] spawnPoints;

	[HideInInspector]
	public int enemiesSpawned = 0;
	[HideInInspector]
	public int enemiesAlive = 0;
	[HideInInspector]
	public bool isSpawning = false;

	[HideInInspector]
	public int deltaHealth = 0;
	[HideInInspector]
	public int deltaDamage = 0;

	public void StartRepeatSpawn (){
		isSpawning = true;
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	public void StopRepeatSpawn (){
		isSpawning = false;
		CancelInvoke ("Spawn");
	}

	private void Spawn (){
		// If the player has no health left...
		// if (playerHealth.currentHealth <= 0f){
		//	... exit the function.
		//return;
		//}

		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		++enemiesSpawned;
		++enemiesAlive;

		GameObject newenemy = Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation) as GameObject;
		newenemy.transform.parent = gameObject.transform;
		newenemy.GetComponent<EnemyHealth> ().health += deltaHealth;
		newenemy.GetComponent<EnemyAttack> ().damage += deltaDamage;
	}
}
