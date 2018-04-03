using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnGemTrigger : MonoBehaviour {

	GameObject heldGem;
	public LoadNextScene loadNextScene;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			AutomatedPlayerMovement autoPlayerMove = other.GetComponent<AutomatedPlayerMovement>();
			autoPlayerMove.StopMove();
			autoPlayerMove.SetDirection(Vector3.forward);
			ReturnGem();

			StartCoroutine(WaitAndLoadNextScene());
		}
	}

	public void SetHeldGem(GameObject gemIn) {
		heldGem = gemIn;
	}

	void ReturnGem() {
		heldGem.SetActive(false);
	}

	IEnumerator WaitAndLoadNextScene() {
		yield return new WaitForSeconds(1f);
		loadNextScene.LoadScene();
	}
}
