using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallSmokeTrail : MonoBehaviour {

    public GameObject cannonBallSmoke;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (rb.velocity.magnitude > 0f) {
            Instantiate(cannonBallSmoke, transform.position, Quaternion.identity);
        }
	}

}
