using UnityEngine;
using System.Collections;
using VRWidgets;

public class StartButton : ButtonBase {
	public GameManager gameManager;

	public override void ButtonPressed() {
		Debug.Log("Start button pressed");
		gameManager.StartGame ();
	}

	public override void ButtonReleased() {
	}
}
