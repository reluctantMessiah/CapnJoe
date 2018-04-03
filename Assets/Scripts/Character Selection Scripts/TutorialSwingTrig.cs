using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSwingTrig : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			var playerAttack = other.gameObject.GetComponent<PlayerAttack>();
			var playerMovement = GetComponent<PlayerMovement>();
			playerMovement.StopMoveForward();
			playerAttack.Swing();
			StartCoroutine(DelayAndMoveAgain(playerMovement));
		}
	}

	IEnumerator DelayAndMoveAgain(PlayerMovement playerMovement) {
		yield return new WaitForSeconds(2f);
		playerMovement.MoveForward();
	}
}
