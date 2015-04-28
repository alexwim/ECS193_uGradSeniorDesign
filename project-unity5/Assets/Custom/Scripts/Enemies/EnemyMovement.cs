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
    if (!grabbed && !navMeshAgent.enabled && collision.gameObject.name == "Terrain") {
      navMeshAgent.enabled = true;
      transform.localPosition = new Vector3(transform.localPosition.x, 0.0f, transform.localPosition.z);
    }
  }

  private void Update () {
    if (grabbed) {
      navMeshAgent.enabled = false;
    }

    if (navMeshAgent.enabled) {
      navMeshAgent.SetDestination (player.position);
    }
  }
}
