using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
  	private Collider playerCollider;
  	private NavMeshAgent navMeshAgent;
	private Animator animator;
	private EnemyHealth enemyHealth;
	private Rigidbody rigidBody;
  	private bool grabbed;
	private bool inAir;
	private Vector3 droppedPosition;

  	private void Awake () {
    	grabbed = false;
		inAir = false;
    	playerCollider = GameObject.FindGameObjectWithTag ("Player").transform.GetComponent<Collider>();
    	navMeshAgent = GetComponent<NavMeshAgent> ();
		enemyHealth = GetComponent<EnemyHealth> ();
		rigidBody = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
  	}

	public void Pinch() {
		grabbed = true;
		navMeshAgent.enabled = false;
		rigidBody.isKinematic = false;
		rigidBody.useGravity = true;
	}

	public void Release() {
		grabbed = false;
		droppedPosition = transform.position;
	}

	public bool isInControl() {
		return navMeshAgent.enabled;
	}

  	private void RegainControl () {
  	  	GetComponent<Rigidbody>().isKinematic = true;
    	GetComponent<Rigidbody>().useGravity = false;
    	navMeshAgent.enabled = true;

		animator.Play ("Move");
  	}

	private void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.name == "Terrain") {
			inAir = false;

			if (!grabbed && !navMeshAgent.enabled) {
				ContactPoint contact = collision.contacts[0];
				Vector3 normal = contact.normal;
				Vector3 relativeVelocity = collision.relativeVelocity;
				
				int damage = (int) Mathf.Abs(Vector3.Dot (normal, relativeVelocity) * GetComponent<Rigidbody>().mass);
				
				enemyHealth.TakeDamage (damage);
				
				droppedPosition.y = 0; // reset after drop

				RegainControl ();
			}
		}
	}

	private void OnCollisionExit (Collision collision) {
		if (collision.gameObject.name == "Terrain") {
			inAir = true;

			animator.Play("Idle");
		}
	}

  	private void Update () {
    	if (navMeshAgent.enabled) {
			navMeshAgent.SetDestination (playerCollider.ClosestPointOnBounds (transform.position));
		} else if (!grabbed && !inAir) {
			RegainControl ();
		}
  	}
}
