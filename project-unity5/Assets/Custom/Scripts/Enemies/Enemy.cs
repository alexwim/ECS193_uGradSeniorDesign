using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private const string TERRAIN_NAME = "Terrain";
	private float CAPSULE_COLLIDER_RADIUS;
	private const float range = 2.0f;

	private GameObject player;
	private Transform playerTransform;
	private PlayerHealth playerHealth;
	private BoxCollider playerCollider;
	private NavMeshAgent navMeshAgent;

	//private Animator animator;

	[HideInInspector]
	public Vector3 droppedPosition;
	[HideInInspector]
	public bool grabbed = false;

	private float timer;

	public int health = 100;
	public float rate = 1f;
	public int damage = 5;

	private bool inRange;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerTransform = player.transform;
		playerHealth = player.GetComponent<PlayerHealth> ();
		playerCollider = player.GetComponent<BoxCollider> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		//animator = GetComponent<Animator> ();
		//animator.speed = 0.5f;

		CAPSULE_COLLIDER_RADIUS = transform.GetComponent<CapsuleCollider>().radius;

		//Debug.Log ("AWAKE(range): " + range);
	}
	
	private void Update() {
		if (grabbed) {
			navMeshAgent.enabled = false;
			//animator.Play("Idle");
			return;
		} 
		else if (navMeshAgent.enabled){
			navMeshAgent.SetDestination (playerCollider.ClosestPointOnBounds(transform.position));

			timer += Time.deltaTime;

			//Debug.Log ("Nav Mesh Enabled");

			if(IsInRange ()) {
				//Debug.Log ("In Range");
				if(timer >= rate) {
					Attack ();
				}
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			inRange = true;
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			inRange = false;
		}
	}


	private void OnCollisionEnter (Collision collision) {
		if ((!grabbed) && (!navMeshAgent.enabled) && (TERRAIN_NAME == collision.gameObject.name)) {
			if(droppedPosition.y > 1) {
				ContactPoint contact = collision.contacts[0];
				Vector3 normal = contact.normal;
				Vector3 relativeVelocity = collision.relativeVelocity;
				
				float damage = Mathf.Abs(Vector3.Dot (normal, relativeVelocity) * GetComponent<Rigidbody>().mass);
				Debug.Log ("Damage taken: " + damage);

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
		//animator.Play ("Move");
	}

	private bool IsInRange() {
		Vector3 closestPosition = playerCollider.ClosestPointOnBounds (transform.position);

		float currentDistance = Vector3.Distance (closestPosition, transform.position) - CAPSULE_COLLIDER_RADIUS;
		//Debug.Log ("Current Distance : " + currentDistance + "; range : " + range + "; bool : " + (range >= currentDistance));
		return (range >= currentDistance);
	}

	private bool IsSideways() {
		return (transform.position.y < 0.9f);
	}
}

