using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public EnemyManager enemyManager;

	public void StartGame() {
		enemyManager.StartRepeatSpawn ();
	}
}

