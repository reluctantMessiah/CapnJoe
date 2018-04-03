using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedCannonUsage : MonoBehaviour {

	public GameObject cannonballPrefab;
	public Transform cannonballPosition;
	public Transform fireStartPos;
	public Vector3 fireVel;

	GameObject heldCannonball;

	public void PickupCannonball() {

		heldCannonball = Instantiate(cannonballPrefab, cannonballPosition.position, Quaternion.identity, this.transform);
		heldCannonball.GetComponent<Rigidbody>().useGravity = false;
		heldCannonball.GetComponent<Rigidbody>().isKinematic = true;

	}

	public void FireCannon() {
		Destroy(heldCannonball);
		var firedCannonball = Instantiate(cannonballPrefab, fireStartPos.position, Quaternion.identity);
		firedCannonball.GetComponent<Rigidbody>().velocity = fireVel;
	}
}
