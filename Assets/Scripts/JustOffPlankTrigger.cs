using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustOffPlankTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			AutomatedPlayerMovement autoPlayerMove = other.GetComponent<AutomatedPlayerMovement>();
			autoPlayerMove.TurnDegrees(false, 13f);
			autoPlayerMove.StartMoveForward();

			this.gameObject.SetActive(false);
		}
	}
}
