using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
  private Transform player;
  private NavMeshAgent navMeshAgent;
  public bool inControl;

  private void Awake () {
    inControl = true;
    player = GameObject.FindGameObjectWithTag ("Player").transform;
    navMeshAgent = GetComponent<NavMeshAgent> ();
  }

  private void Update () {
    if (!inControl) {
      navMeshAgent.enabled = false;
    } else if (!navMeshAgent.enabled && inControl) {
      navMeshAgent.enabled = true;
    }

    if (navMeshAgent.enabled) {
      navMeshAgent.SetDestination (player.position);
    }
  }
}
