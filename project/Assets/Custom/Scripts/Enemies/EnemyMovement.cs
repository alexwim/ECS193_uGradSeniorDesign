using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	private Transform player;
	private NavMeshAgent navMeshAgent;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		navMeshAgent = GetComponent<NavMeshAgent> ();
	}

	private void Update() {
    if (navMeshAgent.enabled) {
      navMeshAgent.SetDestination (player.position);
    }
	}
}
