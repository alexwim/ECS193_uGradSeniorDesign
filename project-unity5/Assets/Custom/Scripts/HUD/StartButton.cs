using UnityEngine;
using System.Collections;
using VRWidgets;

public class StartButton : ButtonBase {
	public EnemyManager enemyManager;

	public override void ButtonPressed() {
		enemyManager.StartRepeatSpawn();
		Destroy (transform.parent.gameObject);
		Debug.Log("Pressed");
	}

	public override void ButtonReleased() {
		Debug.Log("Released");
	}
}
