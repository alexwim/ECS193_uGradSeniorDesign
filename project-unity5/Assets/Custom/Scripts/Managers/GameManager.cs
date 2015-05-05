using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public EnemyManager enemyManager;
	public int enemiesPerWave = 1;
	public float timeBetweenWaves = 3.0f;

	public int perWaveDeltaHealth = 0;
	public int perWaveDeltaDamage = 0;
	public int perWaveDeltaEnemyCount = 0;

	private int waveCurrent = 0;
	private int enemiesSpawnedPreviously = 0;
	private bool waveOngoing = false;

	private bool gameIsStarted = false;
	private bool waitingForTimer = false;

	private HUDManager hud;

	void Start() {
		hud = GameObject.Find ("/LeapOVRPlayerController/OVRCameraRig/CenterEyeAnchor/HUD").GetComponent<HUDManager> ();
	}

	public void StartGame() {
		gameIsStarted = true;
		StartWave (waveCurrent);
	}

	public void StartWave(int waveNumber) {
		Debug.Log ("Starting wave " + waveNumber);
		waveOngoing = true;
		enemyManager.deltaHealth = CurrentBonusHealth();
		enemyManager.deltaDamage = CurrentBonusDamage();
		enemyManager.StartRepeatSpawn ();
	}

	public void StopWave() {
		Debug.Log ("Stopping wave " + waveCurrent);
		enemyManager.StopRepeatSpawn ();
	}

	public void EndWave() {
		waveOngoing = false;
		++waveCurrent;
		enemiesSpawnedPreviously = enemyManager.enemiesSpawned;
	}

	public void Update() {
		if (((enemyManager.enemiesSpawned - enemiesSpawnedPreviously) >= CurrentWaveEnemyCount()) && enemyManager.isSpawning) {
			StopWave ();
		} else if (!enemyManager.isSpawning && enemyManager.enemiesAlive == 0 && waveOngoing) {
			EndWave ();
		} else if (waitingForTimer && !hud.IsTimerRunning()) {
			StartWave (waveCurrent);
			waitingForTimer = false;
		} else if(gameIsStarted && !waveOngoing && !hud.IsTimerRunning()) {
			hud.StartCountdown(timeBetweenWaves);
			waitingForTimer = true;
		}

		// Day Night Cycle
		GameObject.Find ("Sun").transform.Rotate (0.1f, 0, 0);
	}

	private int CurrentBonusHealth(){
		return waveCurrent * perWaveDeltaHealth;
	}

	private int CurrentBonusDamage(){
		return waveCurrent * perWaveDeltaDamage;
	}

	private int CurrentWaveEnemyCount(){
		return waveCurrent * perWaveDeltaEnemyCount + enemiesPerWave;
	}
}

