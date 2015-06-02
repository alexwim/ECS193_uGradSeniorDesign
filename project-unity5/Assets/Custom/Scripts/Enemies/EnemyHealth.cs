using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int health = 100;
	public AudioClip deathClip;

	private AudioSource audioSource;

	private void Awake() {
		audioSource = GetComponent<AudioSource> ();
	}

	public void TakeDamage(int damage) {
		health = (int) Mathf.MoveTowards (health, health - damage, health);

		audioSource.Play ();
		StartCoroutine (Wait (audioSource.clip.length));
		Debug.Log ("damage: " + damage + "; health=" + health);

		if (health <= 0) {
			death();
		}
	}

	private void death() {
		//audioSource.clip = deathClip;
		AudioSource.PlayClipAtPoint (deathClip, GameObject.Find ("Castle").transform.position);
		StartCoroutine (Wait (deathClip.length));
		//audioSource.Play ();

		gameObject.GetComponentInParent<EnemyManager>().enemiesAlive -= 1;
		Destroy (gameObject);
	}

	private IEnumerator Wait(float waitTime) {
		yield return new WaitForSeconds(waitTime);
	}
}