using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
  private Transform player;
  private NavMeshAgent navMeshAgent;

  [HideInInspector]
  public bool grabbed;

  private void Awake () {
    grabbed = false;
    player = GameObject.FindGameObjectWithTag ("Player").transform;
    navMeshAgent = GetComponent<NavMeshAgent> ();
  }

  private void OnCollisionEnter (Collision collision) {
    // This re-enables the navmesh once an enemy hits the ground after a long toss.
    if (!grabbed && !navMeshAgent.enabled && collision.gameObject.name == "Terrain") {
      navMeshAgent.enabled = true;
    }
  }

  private void Update () {
    if (grabbed) {
      navMeshAgent.enabled = false;
      return;
    } else if (transform.position.y < 0.9f) { // In case the enemies can't collide against the terrain, ie they're laying the ground.
      navMeshAgent.enabled = true;
    } else if (navMeshAgent.enabled) {
      navMeshAgent.SetDestination (player.position);
    }
  }
}
