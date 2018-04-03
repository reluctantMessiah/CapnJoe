using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToBoatB4PlankTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			AutomatedPlayerMovement autoPlayerMove = other.GetComponent<AutomatedPlayerMovement>();
			//autoPlayerMove.TranslateTransform(Vector3.forward * 0.5f);
			autoPlayerMove.SetDirection(Vector3.right);
			autoPlayerMove.StartMoveForward();
			autoPlayerMove.TranslateTransform(Vector3.forward * 1f);
			autoPlayerMove.Jump();

		}
	}
}
