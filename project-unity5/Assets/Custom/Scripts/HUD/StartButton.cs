using UnityEngine;
using System.Collections;
using VRWidgets;

public class StartButton : ButtonToggleBase {
	public EnemyManager enemyManager;

	public override void ButtonTurnsOn() {
		enemyManager.StartRepeatSpawn();
		Destroy (transform.parent.gameObject);
	}
	
	public override void ButtonTurnsOff() {
	}
}
