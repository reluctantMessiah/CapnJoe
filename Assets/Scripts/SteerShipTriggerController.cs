using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerShipTriggerController : MonoBehaviour {

	public LoadNextScene loadNextScene;

	public Rigidbody shipRb;
	public GameObject xInidcator;
	public Rigidbody rockRb;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {

			AutomatedPlayerMovement autoPlayerMove = other.GetComponent<AutomatedPlayerMovement>();
			other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			other.transform.parent = shipRb.gameObject.transform;

			rockRb.velocity = Vector3.back * 20f;

			xInidcator.SetActive(true);

			autoPlayerMove.TurnDegrees(false, 90f);
			shipRb.velocity = (new Vector3(1, 0, 1)) * 35f;

			StartCoroutine(WaitAndStrafeLeft());
		}
	}

	IEnumerator WaitAndStrafeLeft() {
		yield return new WaitForSeconds(1.5f);
		shipRb.velocity = (new Vector3(-1, 0, 1)) * 35f;
		StartCoroutine(WaitAndLoadNextScene());
	}

	IEnumerator WaitAndLoadNextScene() { 
		yield return new WaitForSeconds(1f);
		loadNextScene.LoadScene();
	}
}
