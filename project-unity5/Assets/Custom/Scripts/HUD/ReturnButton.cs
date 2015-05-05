using UnityEngine;
using System.Collections;
using VRWidgets;

public class ReturnButton : ButtonBase {
	private GameManager gameManager;

	void Start() {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}
	
	public void Reset() {
		Rigidbody body = GetComponent<Rigidbody> ();
		body.velocity = Vector3.zero;
		body.angularVelocity = Vector3.zero;
		body.inertiaTensorRotation = Quaternion.identity;
		
		transform.localPosition = new Vector3 (0, -1, 0);
		transform.localRotation = new Quaternion(0.0f,0,0,1.0f);
	}

	public override void ButtonPressed() {
		Debug.Log("Return button pressed");
		gameManager.Restart ();
	}
	
	public override void ButtonReleased() {
	}
}
