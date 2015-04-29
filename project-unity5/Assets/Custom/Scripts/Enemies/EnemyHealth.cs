using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int health = 100;
	public Vector3 droppedPosition;

	private void OnCollisionEnter (Collision collision) {
		if (droppedPosition.y > 0 && collision.gameObject.name == "Terrain") {
			droppedPosition.y = 0; // reset after drop
			death ();
		}
	}

	private void death() {
		Destroy (gameObject);
	}
}