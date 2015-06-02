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
	public GameObject mainMenu;
	public GameObject gameOverMenu;
	public PlayerHealth player;
	public DayNightController dayNightController;

	void Start() {
		hud = GameObject.Find ("/LeapOVRPlayerController/OVRCameraRig/CenterEyeAnchor/HUD").GetComponent<HUDManager> ();
		player = GameObject.Find ("/Environment/Castle").GetComponent<PlayerHealth> ();
		mainMenu.SetActive (true);
	}

	public void StartGame() {
		gameIsStarted = true;
		player.Reset ();
		enemyManager.Reset ();
		mainMenu.SetActive (false);

		waveCurrent = 0;
		enemiesSpawnedPreviously = 0;
		waveOngoing = false;
		waitingForTimer = false;

		// This must be the last thing we do
		hud.ToggleWaveInfo ();
		StartWave (waveCurrent);
	}

	public void Restart() {
		gameOverMenu.SetActive (false);
		mainMenu.SetActive (true);
		mainMenu.GetComponentInChildren<StartButton> ().Reset ();
	}

	public void EndGame() {
		StopWave ();
		EndWave ();
		enemyManager.ClearAllEnemies ();

		gameOverMenu.SetActive (true);
		gameOverMenu.GetComponentInChildren<ReturnButton> ().Reset ();
		hud.ToggleWaveInfo ();
		gameIsStarted = false;
	}

	public void StartWave(int waveNumber) {
		Debug.Log ("Starting wave " + waveNumber);
		waveOngoing = true;
		enemyManager.deltaHealth = CurrentBonusHealth();
		enemyManager.deltaDamage = CurrentBonusDamage();
		enemyManager.StartRepeatSpawn ();
		hud.SetWaveNumber (waveNumber);

		dayNightController.UpdateSunRotation ();
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

		if (gameIsStarted && player.IsDead ()) {
			EndGame ();
		}

		// Day Night Cycle
		//GameObject.Find ("Sun").transform.Rotate (0.1f, 0, 0);
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

