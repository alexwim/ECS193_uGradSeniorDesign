using UnityEngine;
using System.Collections;
using VRWidgets;

public class StartButton : ButtonBase {
	public GameManager gameManager;

	public override void ButtonPressed() {
		gameManager.StartGame ();
		Destroy (transform.parent.gameObject);
		Debug.Log("Start button pressed");
	}

	public override void ButtonReleased() {
	}
}
