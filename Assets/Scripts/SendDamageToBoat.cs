using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDamageToBoat : MonoBehaviour {

	BoatHealth boatHealth;

	// Use this for initialization
	void Start () {
		boatHealth = GetComponentInParent<BoatHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionStay(Collision other) {
        boatHealth.OnCollisionEnter(other);
    }
}
