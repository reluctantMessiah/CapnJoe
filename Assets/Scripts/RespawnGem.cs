using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGem : MonoBehaviour {

	float fallLimit;
	int team;
//	GemManager gemManager;

	// Use this for initialization
	void Start () {
//		gemManager = GameObject.Find("Gem Manager").GetComponent<GemManager>();
		fallLimit = -10f;
	}
	
	public void SetTeam(int teamNum) {
		team = teamNum;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= fallLimit) {
			GameObject boat1 = GameObject.Find("Boat 1");
			GameObject boat2 = GameObject.Find("Boat 2");
			float distToBoat1 = Vector3.Distance(boat1.transform.position, transform.position);
			float distToBoat2 = Vector3.Distance(boat2.transform.position, transform.position);
			
			Vector3 newPos;
			if (distToBoat1 < distToBoat2) {
				newPos = boat1.transform.position;
				newPos.y += 10f;
			} else {
				newPos = boat2.transform.position;
				newPos.y += 10f;
			}
            //print("respawn");

            transform.parent = null;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
			this.transform.position = newPos;
		}
	}
}
