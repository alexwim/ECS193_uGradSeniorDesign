using UnityEngine;
using System.Collections;
using Leap;

public class GesturePinch : MonoBehaviour {
	private const float PINCH_TRIGGER_DISTANCE = 0.5F;
	private const float GRAB_SPRING_FORCE = 100f;
	private const float GRAB_TRIGGER_DISTANCE = 2f;

	private bool isPinching;
	private Collider grabbedObject;

	// Init
	void Start(){
		isPinching = false;
		grabbedObject = null;
	}

	// Reset
	void Release(){
		isPinching = false;
		grabbedObject = null;
	}

	void OnPinch(Vector3 pinchPosition){
		isPinching = true;

		Collider[] nearbyObjects = Physics.OverlapSphere(pinchPosition, GRAB_TRIGGER_DISTANCE);
		Vector3 triggerDistance = new Vector3(GRAB_TRIGGER_DISTANCE, 0.0f, 0.0f);

		for (int i = 0; i < nearbyObjects.Length; ++i) {
			Vector3 objectDistance = pinchPosition - nearbyObjects[i].transform.position;
			if (nearbyObjects[i].rigidbody != null && objectDistance.magnitude < triggerDistance.magnitude &&
			    !nearbyObjects[i].transform.IsChildOf(transform)) {
				grabbedObject = nearbyObjects[i];
				triggerDistance = objectDistance;
			}
		}
	}
		
	void Update(){
		bool trigger = false;
		HandModel handModel = GetComponent<HandModel>();
		Hand leapHand = handModel.GetLeapHand();
		
		if (leapHand == null)
			return;

		// One of the pinching fingers must be the thumb.
		Vector thumbTip = leapHand.Fingers[0].TipPosition;
		Vector3 pinchPosition;

		for (int i = 1; i < HandModel.NUM_FINGERS && !trigger; ++i) {
			Vector fingerTip = leapHand.Fingers[i].TipPosition;

			if(fingerTip.DistanceTo(thumbTip) < PINCH_TRIGGER_DISTANCE && !isPinching) {
				trigger = true;
				// Pinch position shall be the midpoint between the two pinching fingers.
				pinchPosition = (handModel.fingers[0].GetTipPosition() - handModel.fingers[i].GetTipPosition()) * 0.5f + handModel.fingers[i].GetTipPosition();
				OnPinch(pinchPosition);

				// If our grabbed object exists, move it towards the pinch position
				if (grabbedObject) {
					Vector3 distance = pinchPosition - grabbedObject.transform.position;
					grabbedObject.rigidbody.AddForce(GRAB_SPRING_FORCE * distance);
				}
			}
		}

		// If we were pinching on the last frame and we didn't pinch on this frame, release.
		if (!trigger && isPinching) {
			Release();
		}
	}
}