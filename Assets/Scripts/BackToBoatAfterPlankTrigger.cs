using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToBoatAfterPlankTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			AutomatedPlayerMovement autoPlayerMove = other.GetComponent<AutomatedPlayerMovement>();
			autoPlayerMove.TurnDegrees(true, 20f);
		}
	}
}
