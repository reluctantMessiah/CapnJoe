using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallBoatSource : MonoBehaviour {

	int boat;

	// Use this for initialization
	void Start () {
		float distToBoat1 = Vector3.Distance (this.transform.position, GameObject.Find ("Boat 1").transform.position);
		float distToBoat2 = Vector3.Distance (this.transform.position, GameObject.Find ("Boat 2").transform.position);
		boat = distToBoat1 < distToBoat2 ? 1 : 2;
	}

	public int GetBoat() {
		return boat;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
