using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawnerController : MonoBehaviour {

	public GameObject ammoGunPrefab;

	Vector3[] spawnPositions;

	int currentSpawnPositionIndex = -1;

	public int numBulletCollectiblesOnScreen = 2;

	// Use this for initialization
	void Awake () {

		var spawnPositionsList = new List<Vector3>();

		foreach(Transform t in GetComponentsInChildren<Transform>()) {
			if(t.name != this.gameObject.name)
				spawnPositionsList.Add (t.position);
		}

		spawnPositions = spawnPositionsList.ToArray ();

		print (this.spawnPositions.Length);
	}

	void Start() {

		for (int i = 0; i < numBulletCollectiblesOnScreen; ++i) {
			Spawn ();
		}
	}

	public void Spawn() {

		int positionIndex = Random.Range (0, spawnPositions.Length);

		if (positionIndex == currentSpawnPositionIndex) {
			positionIndex = (positionIndex + 1) % spawnPositions.Length;
		}
		currentSpawnPositionIndex = positionIndex;

		Vector3 ammoPosition = spawnPositions[positionIndex];
		Instantiate (ammoGunPrefab, ammoPosition, Quaternion.identity);
	}
}
