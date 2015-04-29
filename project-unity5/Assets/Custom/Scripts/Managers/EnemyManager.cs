using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
	public GameObject enemy;
	public float spawnTime;
	public Transform[] spawnPoints;

	public void StartRepeatSpawn ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	public void StopRepeatSpawn ()
	{
		CancelInvoke ("Spawn");
	}

	private void Spawn ()
	{
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
	}
}
