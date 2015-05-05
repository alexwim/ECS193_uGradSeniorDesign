using UnityEngine;
using System.Collections;
using VRWidgets;

public class StartButton : ButtonBase {
	public GameManager gameManager;

	private GameObject mainMenu;

	void Start() {
		mainMenu = GameObject.Find ("MainMenu");
	}

	public override void ButtonPressed() {
		Debug.Log("Start button pressed");
		gameManager.StartGame ();
		mainMenu.SetActive(false);
	}

	public override void ButtonReleased() {
	}
}
