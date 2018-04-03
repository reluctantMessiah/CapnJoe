using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMovement : MonoBehaviour {

	public float moveSpeed = 3f;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	public void StartMove() {
		rb.velocity = transform.forward * moveSpeed;
	}

	public void StopMove() {
		rb.velocity = Vector3.zero;
	}
}
