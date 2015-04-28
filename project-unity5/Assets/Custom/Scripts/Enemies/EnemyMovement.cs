using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	private Transform player;
	private NavMeshAgent navMeshAgent;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag ("Castle").transform;
		navMeshAgent = GetComponent<NavMeshAgent> ();
		Debug.Log (navMeshAgent.enabled);
	}

	private void Update() {
	    if (navMeshAgent.enabled) {
	      navMeshAgent.SetDestination (player.position);
	    }
	}
}
