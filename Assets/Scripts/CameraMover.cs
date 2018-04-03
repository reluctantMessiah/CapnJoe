using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

	Transform highPos;
	Transform lowPos;

	GameObject boatCenter1;
	GameObject boatCenter2;

	Transform centerPos;

	public float cameraSpeed = 20.0f;

	// Use this for initialization
	void Start () {
		highPos = GameObject.Find ("Camera Position (High)").transform;
		lowPos = GameObject.Find ("Camera Position (Low)").transform;

		boatCenter1 = GameObject.Find ("Boat 1").transform.Find("Wheel").gameObject;
		boatCenter2 = GameObject.Find ("Boat 2").transform.Find ("Wheel").gameObject;

		centerPos = GameObject.Find ("Center Position").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float step = cameraSpeed * Time.fixedDeltaTime;
		if (Vector3.Distance (boatCenter1.transform.position, centerPos.position) >= 43f ||
		    Vector3.Distance (boatCenter2.transform.position, centerPos.position) >= 43f) {
//			this.transform.position = highPos.position;
//			this.transform.rotation = highPos.rotation;

			transform.position = Vector3.MoveTowards(transform.position, highPos.position, step);
			transform.LookAt (centerPos);
		} else {
//			this.transform.position = lowPos.position;
//			this.transform.rotation = lowPos.rotation;

			transform.position = Vector3.MoveTowards(transform.position, lowPos.position, step);
			transform.LookAt (centerPos);
		}
	}
}
