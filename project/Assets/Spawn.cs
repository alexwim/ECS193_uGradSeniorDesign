using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public Transform target;
	public GameObject unit;
	
	public float spawnTime = .1f;
	float spawnTimeLeft = .5f;


	// Update is called once per frame
	void Update () {
		if(spawnTimeLeft <= 0) {
			GameObject go = (GameObject)Instantiate(unit, transform.position, transform.rotation);
			go.GetComponent<AstarAI>().target = target;
			spawnTimeLeft = spawnTime;
		}
		else {
			spawnTimeLeft -= Time.deltaTime/10;
		}
	}
}
