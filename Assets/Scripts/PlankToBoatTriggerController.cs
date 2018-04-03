using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankToBoatTriggerController : MonoBehaviour
{

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			AutomatedPlayerMovement autoPlayerMove = other.GetComponent<AutomatedPlayerMovement>();
			BoostPlayerUp(autoPlayerMove);
			this.gameObject.SetActive(false);
		}
	}

	void BoostPlayerUp(AutomatedPlayerMovement autoPlayerMove) {
		autoPlayerMove.Jump();
	}
}