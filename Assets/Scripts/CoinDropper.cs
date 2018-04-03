using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDropper : MonoBehaviour {

	public GameObject coinPrefab;

	int numCoins = 0;
	int pointsPerCoin = 1;

	bool readyToDrop = false;

	int team;
	
	public float throwSpeedMin = 1.25f;
	public float throwSpeedMax = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	public void SetTeam(int teamNum) {
		team = teamNum;
	}
	
	// Update is called once per frame
	void Update () {
		if (readyToDrop)
			DropCoins ();
	}

	public void SetNumberOfPointsToDrop(int points) {
		points = Mathf.Max (points, 0);
		numCoins = Mathf.FloorToInt(points / pointsPerCoin);
//		print ("points " + points.ToString());
		readyToDrop = true;
	}

	void DropCoins() {
		for (int i = 0; i < numCoins; i++) {
			Vector3 dir = this.transform.forward.normalized;
			dir.x -= Random.Range (0f, 180f);
			dir.y += Random.Range (0f, 360f);
			float speed = Random.Range (throwSpeedMin, throwSpeedMax);

			GameObject newCoin = Instantiate (coinPrefab);
			newCoin.transform.position = this.transform.position;
//			newCoin.GetComponent<RespawnGemInChest>().SetTeam(team);

			Rigidbody rb = newCoin.GetComponent<Rigidbody> ();
			rb.AddForce (dir * speed);
			rb.AddForce (Vector3.up * 1.5f);
		}
		Destroy (this.gameObject);
	}
}
