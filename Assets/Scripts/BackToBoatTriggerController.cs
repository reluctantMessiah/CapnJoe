using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToBoatTriggerController : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			AutomatedPlayerMovement autoPlayerMove = other.GetComponent<AutomatedPlayerMovement>();
			autoPlayerMove.Jump();
		}
	}
}
