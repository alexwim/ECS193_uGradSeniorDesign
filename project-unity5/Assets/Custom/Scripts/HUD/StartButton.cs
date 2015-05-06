using UnityEngine;
using System.Collections;
using VRWidgets;

public class StartButton : ButtonBase {
	private GameManager gameManager;
	
	private Vector3 defaultPosition;
	private Quaternion defaultRotation;

	void Start() {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		defaultPosition = transform.localPosition;
		defaultRotation = transform.localRotation;
	}

	public void Reset() {
		Rigidbody body = GetComponent<Rigidbody> ();
		body.velocity = Vector3.zero;
		body.angularVelocity = Vector3.zero;
		body.inertiaTensorRotation = Quaternion.identity;
		
		transform.localPosition = defaultPosition;
		transform.localRotation = defaultRotation;
	}

	public override void ButtonPressed() {
		Debug.Log("Start button pressed");
		gameManager.StartGame ();
	}

	public override void ButtonReleased() {
	}
}
