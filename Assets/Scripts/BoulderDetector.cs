using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderDetector : MonoBehaviour {

	ScoreTracker scoreTracker;

	GameObject[] players;

	bool boatIsManned = false;
	GameObject joeWhoIsManning;

	// Use this for initialization
	void Start () {
		scoreTracker = GameObject.Find ("ScoreTracker").GetComponent<ScoreTracker> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetBoatIsManned(bool manning, GameObject player) {
		boatIsManned = manning;
		joeWhoIsManning = player;
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("Boulder")) {
            print("dodged");
			if (boatIsManned) {
				scoreTracker.RecordRockDodged (joeWhoIsManning);
			}
		}
	}
}
