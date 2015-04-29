using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int health = 100;
	public Vector3 droppedPosition;

	private void OnCollisionEnter (Collision collision) {
		if (droppedPosition.y > 0) {
			Debug.Log ("I FELL");
			ContactPoint contact = collision.contacts[0];
			Vector3 normal = contact.normal;
			Vector3 relativeVelocity = collision.relativeVelocity;

			float damage = Vector3.Dot (normal, relativeVelocity);

			Debug.Log(damage);
			droppedPosition.y = 0; // reset after drop
			death ();
		}
	}

	private void death() {
		Destroy (gameObject);
	}
}