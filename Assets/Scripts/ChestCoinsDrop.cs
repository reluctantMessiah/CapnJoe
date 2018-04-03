using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCoinsDrop : MonoBehaviour {

	Rigidbody boatRb;
	public float minShakeForceToDropCoins = 700f;
	ChestManager chestManager;
	ChestTrigger chestTrigger;
	
	float timeOfDrop = 0f;
	
	public GameObject coinDropperPrefab;
	
	float throwSpeedMin = .25f;
	float throwSpeedMax = 3f;
	
	Vector3 lastVelocity;

	// Use this for initialization
	void Start () {
		chestManager = GetComponent<ChestManager>();
		if (chestManager == null) {
			chestTrigger = GetComponent<ChestTrigger>();
		}

		boatRb = GetComponentInParent<Rigidbody>();
		lastVelocity = boatRb.velocity;
	}
	
	float GetAcceleration() {
		float acceleration = (boatRb.velocity - lastVelocity).magnitude / Time.fixedDeltaTime;
		lastVelocity = boatRb.velocity;
		return acceleration;
	}
	
	// Update is called once per frame
	void Update () {
//		print (GetAcceleration ());
//		if (GetAcceleration() >= minShakeForceToDropCoins) {
//			if (Mathf.Abs(timeOfDrop - Time.time) >= 3f) {
//				DropGems();
//				timeOfDrop = Time.time;
//			}
//		}
	}

	public void DropGems(int numGemsToLose) {
		int numGems;
		if (chestManager == null) {
			numGems = chestTrigger.NumGemsInChest ();
			if (numGems <= 0)
				return;
		} else {
			numGems = chestManager.NumGemsInChest ();
			if (numGems <= 0)
				return;
		}
		GameObject coinDropper = Instantiate(coinDropperPrefab);
		Vector3 position = this.transform.position;
		position.y += 2f;
		coinDropper.transform.position = position;
		CoinDropper coinDropperScript = coinDropper.GetComponent<CoinDropper> ();
		coinDropperScript.throwSpeedMin = throwSpeedMin;
		coinDropperScript.throwSpeedMax = throwSpeedMax;
		int numPointsToLose = Mathf.Min (numGemsToLose, numGems);
		coinDropperScript.SetNumberOfPointsToDrop (numPointsToLose);

		if (chestManager == null) {
			chestTrigger.RemoveGems(numPointsToLose);
		} else {
			chestManager.RemoveGems(numPointsToLose);
		}
	}
}
