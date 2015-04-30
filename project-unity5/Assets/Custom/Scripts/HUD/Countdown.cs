using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {
	private Text text;
	private int time;

	public void Awake() {
		text = GetComponent<Text> ();
	}

	public IEnumerator countdown(int startTime) {
		time = startTime;

		while (time > 0) {
			yield return new WaitForSeconds(1);

			text = "Next wave in: " + time.ToString();

			time -= 1;
		}
	}

}
