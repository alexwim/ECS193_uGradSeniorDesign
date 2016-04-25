using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int health = 100;

	public AudioClip deathClip;
	private AudioSource audioSource;

	private Animator animator;
	private float deathTime = 1.5f;

	[HideInInspector]
	public bool isDying = false;

	private void Awake() {
		animator = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
	}

	public void TakeDamage(int damage) {
		if (damage == int.MaxValue) {
			kill ();
			return;
		}

		health = (int) Mathf.MoveTowards (health, health - damage, health);

		audioSource.Play ();
		Debug.Log ("damage: " + damage + "; health=" + health);

		if (health <= 0) {
			death();
		}
	}

	private void death() {
		AudioSource.PlayClipAtPoint (deathClip, GameObject.Find ("Castle").transform.position);
		isDying = true;
		animator.Play ("Death");
		StartCoroutine (WaitThenDie (deathTime));
	}

	private IEnumerator WaitThenDie(float waitTime) {
		yield return new WaitForSeconds(waitTime);

		kill ();
	}

	private void kill() {
		gameObject.GetComponentInParent<EnemyManager>().enemiesAlive -= 1;
		Destroy (gameObject);
	}
}