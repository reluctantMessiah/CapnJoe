using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannonTriggerController : MonoBehaviour {

	public LoadNextScene loadNextScene;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			AutomatedCannonUsage autoCannonUsage = other.GetComponent<AutomatedCannonUsage>();
			AutomatedPlayerMovement autoPlayerMove = other.GetComponent<AutomatedPlayerMovement>();

			autoPlayerMove.StopMove();
			autoCannonUsage.FireCannon();

			StartCoroutine(WaitAndLoadNextScene());
		}
	}

	IEnumerator WaitAndLoadNextScene() {
		yield return new WaitForSeconds(1f);
		loadNextScene.LoadScene();
	}
}
