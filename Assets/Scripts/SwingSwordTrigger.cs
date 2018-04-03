using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingSwordTrigger : MonoBehaviour {

	public LoadNextScene loadNextScene;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			AutomatedPlayerMovement autoPlayerMove = other.GetComponent<AutomatedPlayerMovement>();
			autoPlayerMove.StopMove();
			AutomatedPlayerAttack autoPlayerAttack = other.GetComponent<AutomatedPlayerAttack>();

			autoPlayerAttack.Swing();

			StartCoroutine(WaitAndLoadNextScene());
		}
	}

	IEnumerator WaitAndLoadNextScene() {
		yield return new WaitForSeconds(1.5f);
        loadNextScene.LoadScene();
	}
}
