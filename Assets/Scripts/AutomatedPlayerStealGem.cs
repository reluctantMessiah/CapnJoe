using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedPlayerStealGem : MonoBehaviour {

	public GameObject gemPrefab;
	public Transform gemSpawnPosition;
	public GameObject xInidicator;
	public ReturnGemTrigger returnGemTrigger;

	public void StealGem() {
		var spawnedGem = Instantiate(gemPrefab, gemSpawnPosition.position, Quaternion.identity, this.gameObject.transform);
		spawnedGem.GetComponent<Rigidbody>().isKinematic = true;
		spawnedGem.GetComponent<Rigidbody>().useGravity = false;
		spawnedGem.GetComponent<BoxCollider>().enabled = false;
		returnGemTrigger.SetHeldGem(spawnedGem);
	}

	public void ShowXIndicator() {
		xInidicator.SetActive(true);
	}

	public void HideXIndicator() {
		xInidicator.SetActive(false);
	}
}
