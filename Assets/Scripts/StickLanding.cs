using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickLanding : MonoBehaviour {

    public LayerMask layerMask;
    public float rayLength;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
	}
	
	void OnCollisionEnter(Collision other) {
        if (other.collider.gameObject.CompareTag("Boat1") ||
            other.collider.gameObject.CompareTag("Boat2")) {

            RaycastHit hitInfo;

            if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, rayLength, layerMask)) {
                if (hitInfo.collider.gameObject.tag == "Boat1" || hitInfo.collider.gameObject.tag == "Boat2") {
                    rb.velocity = Vector3.zero;
                    transform.parent = other.transform;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                }
            }
        }
	}

}
