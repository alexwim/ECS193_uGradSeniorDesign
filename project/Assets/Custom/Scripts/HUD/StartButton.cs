using UnityEngine;
using System.Collections;
using LMWidgets;

public class StartButton : ButtonToggleBase {
	public EnemyManager enemyManager;

	public override void ButtonTurnsOn() {
		enemyManager.StartRepeatSpawn();
		Destroy (transform.parent.parent.gameObject);
	}
	
	public override void ButtonTurnsOff() {
	}
	
	protected override void Start() {
		base.Start();
	}
	
	protected override void FixedUpdate() {
		base.FixedUpdate();
	}
}
