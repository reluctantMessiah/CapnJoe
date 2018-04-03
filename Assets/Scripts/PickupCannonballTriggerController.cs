using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCannonballTriggerController : MonoBehaviour {

	public TextMesh cannonInstructionText;
	public string secondInstructionText;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			AutomatedPlayerMovement autoPlayerMove = other.GetComponent<AutomatedPlayerMovement>();
			AutomatedCannonUsage autoCannonUsage = other.GetComponent<AutomatedCannonUsage>();
			autoPlayerMove.StopMove();
			autoCannonUsage.PickupCannonball();
			autoPlayerMove.TurnDegrees(false, 45f);
			autoPlayerMove.StartMoveForward();

			cannonInstructionText.text = secondInstructionText;
		}
	}
}
