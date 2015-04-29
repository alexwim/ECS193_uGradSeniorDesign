using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private const string TERRAIN_NAME = "Terrain";
	private float CAPSULE_COLLIDER_RADIUS;

	private GameObject player;
	private Transform playerTransform;
	private PlayerHealth playerHealth;
	private BoxCollider playerCollider;
	private NavMeshAgent navMeshAgent;

	[HideInInspector]
	public Vector3 droppedPosition;
	[HideInInspector]
	public bool grabbed = false;

	private float timer;

	public int health = 100;
	public float rate = 1f;
	public int damage = 5;
	public float range = 1f;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerTransform = player.transform;
		playerHealth = player.GetComponent<PlayerHealth> ();
		playerCollider = player.GetComponent<BoxCollider> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();

		CAPSULE_COLLIDER_RADIUS = transform.GetComponent<CapsuleCollider>().radius;
	}
	
	private void Update() {
		if (grabbed) {
			navMeshAgent.enabled = false;
			return;
		} else if (IsSideways ()) {
			RegainControl ();
		} else if (navMeshAgent.enabled){
			navMeshAgent.SetDestination (playerCollider.ClosestPointOnBounds(transform.position));
			timer += Time.deltaTime;
			
			if(timer >= rate && IsInRange()) {
				Attack();
			}
		}
	}

	private void OnCollisionEnter (Collision collision) {
		if ((!grabbed) && (!navMeshAgent.enabled) && (TERRAIN_NAME == collision.gameObject.name)) {
			if(droppedPosition.y > 1) {
				ContactPoint contact = collision.contacts[0];
				Vector3 normal = contact.normal;
				Vector3 relativeVelocity = collision.relativeVelocity;
				
				double damage = Vector3.Dot (normal, relativeVelocity) * GetComponent<Rigidbody>().mass;
				
				health -= (int) damage;
				
				droppedPosition.y = 0; // reset after drop
				
				if(health <= 0) {
					gameObject.GetComponentInParent<EnemyManager>().enemiesAlive -= 1;
					Destroy (gameObject);
				}
			}

			RegainControl();
		}
	}
	
	private void Attack() {
		timer = 0f;
		
		playerHealth.TakeDamage (damage);
	}

	private void RegainControl () {
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Rigidbody>().useGravity = false;
		navMeshAgent.enabled = true;
	}

	private bool IsInRange() {
		Vector3 closestPosition = playerCollider.ClosestPointOnBounds (transform.position);

		float currentDistance = Vector3.Distance (closestPosition, transform.position) - CAPSULE_COLLIDER_RADIUS;

		return (range >= currentDistance);
	}

	private bool IsSideways() {
		return (transform.position.y < 0.9f);
	}
}

