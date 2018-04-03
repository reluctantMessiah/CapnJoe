using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedPlayerMovement : MonoBehaviour {

	public float moveSpeed = 5f;
	public float jumpSpeed = 5f;
	public float gravAccel = 9f;

	Rigidbody rb;

	void Awake() {
		rb = GetComponent<Rigidbody>();
	}

	void Start() {
		StartMoveForward();
	}

	void Update() {
		Vector3 newVel = rb.velocity;
		newVel.y -= gravAccel * Time.deltaTime;
		rb.velocity = newVel;
	}

	public void TurnDegrees(bool turnRight, float degrees) {
		float sign = 1;
		if (turnRight)
			sign = -1;
		transform.Rotate(-transform.up, sign * degrees);
	}

	public void StartMoveForward() {
		print("Start move forward");
		rb.velocity = transform.forward * moveSpeed;
	}

	public void StopMove() {
		rb.velocity = Vector3.zero;
	}

	public void Jump() {
		Vector3 newVel = rb.velocity;
		newVel.y = jumpSpeed;
		rb.velocity = newVel;
	}

	public void FreezeRotation() {
		rb.constraints = RigidbodyConstraints.FreezeRotation;
	}

	public void SetDirection(Vector3 newDirection) {
		this.transform.rotation = Quaternion.LookRotation(newDirection);
	}

	public void TranslateTransform(Vector3 shiftBy) {
		transform.position += shiftBy;
	}
}
