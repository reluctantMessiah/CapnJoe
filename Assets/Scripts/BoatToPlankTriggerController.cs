using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatToPlankTriggerController : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			AutomatedPlayerMovement autoPlayerMovement = other.GetComponent<AutomatedPlayerMovement>();
			TurnPlayerLeft(autoPlayerMovement);
			CorrectMoveDirection(autoPlayerMovement);
			autoPlayerMovement.Jump();

			this.gameObject.SetActive(false);
		}
	}

	void TurnPlayerLeft(AutomatedPlayerMovement autoPlayerMovement) {
		autoPlayerMovement.TurnDegrees(false, 90f);
	}

	void CorrectMoveDirection(AutomatedPlayerMovement autoPlayerMovement) {
		autoPlayerMovement.StartMoveForward();
	}
}
