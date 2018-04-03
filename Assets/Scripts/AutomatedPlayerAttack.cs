using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedPlayerAttack : MonoBehaviour {

	public GameObject swordPrefab;
	public Transform swordSpawnPosition;
	public GameObject rightTrigger;
	public Rigidbody enemyRb;

	public void Swing() {

		var spawnedSword = Instantiate(swordPrefab, swordSpawnPosition.position, Quaternion.identity, this.transform);
		rightTrigger.SetActive(true);

		enemyRb.velocity = (new Vector3(1f, 1f, 0f)) * 3f;

		StartCoroutine(WaitAndHideIndicator());
	}

	IEnumerator WaitAndHideIndicator() {
		yield return new WaitForSeconds(1f);
		rightTrigger.SetActive(false);
		enemyRb.velocity = Vector3.zero;
	}
}
