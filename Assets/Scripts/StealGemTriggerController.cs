using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealGemTriggerController : MonoBehaviour {

	public GameObject backToBoatB4PlankTrigger;
	public GameObject backToBoatAfterPlankTrigger;
	public GameObject mainCamera;
	public Transform secondCameraPosition;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			AutomatedPlayerMovement autoPlayerMove = other.GetComponent<AutomatedPlayerMovement>();
			AutomatedPlayerStealGem autoSealGem = other.GetComponent<AutomatedPlayerStealGem>();
			autoPlayerMove.StopMove();
			autoSealGem.ShowXIndicator();
			autoPlayerMove.SetDirection(Vector3.left);
			autoPlayerMove.TurnDegrees(true, 90f);
			autoSealGem.StealGem();
			autoPlayerMove.TurnDegrees(true, 180f);

			autoPlayerMove.SetDirection(Vector3.right);
			autoPlayerMove.StartMoveForward();

			StartCoroutine(DelayAndTurn(autoPlayerMove));

			this.backToBoatB4PlankTrigger.SetActive(true);
			this.backToBoatAfterPlankTrigger.SetActive(true);

			mainCamera.transform.position = secondCameraPosition.position;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player"))
		{
			AutomatedPlayerStealGem autoPlayerStealGem = other.GetComponent<AutomatedPlayerStealGem>();
			autoPlayerStealGem.HideXIndicator();
            this.gameObject.SetActive(false);
		}
	}

	IEnumerator DelayAndTurn(AutomatedPlayerMovement autoMove) {
		yield return new WaitForSeconds(1.5f);
		autoMove.TurnDegrees(true, 90f);
	}
}
