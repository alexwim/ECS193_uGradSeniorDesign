using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private Transform target;
	public Transform myTransform;
	public int moveSpeed = 100;
	public int maxDistance = 5;

	// Use this for initialization
	void Awake () {
			myTransform = transform;
	}

	void Start () {
		target = GameObject.Find ("Castle").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (target.position, myTransform.position) > maxDistance) {
			transform.LookAt (target.position);
			transform.Translate (Vector3.forward * Time.deltaTime * moveSpeed);
		}
	}
}
