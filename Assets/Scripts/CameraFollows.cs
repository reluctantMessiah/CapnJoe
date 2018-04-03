using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollows : MonoBehaviour {
	public Transform player1;
	public Transform player2;

	private const float DISTANCE_MARGIN = 15.0f;

	private Vector3 middlePoint;
	private float distanceFromMiddlePoint;
	private float distanceBetweenPlayers;
	private float cameraDistance;
	private float aspectRatio;
	private float fov;
	private float tanFov;


	void Start() {

		aspectRatio = Screen.width / Screen.height;
		tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);

	}

	void FixedUpdate () {
		// Position the camera in the center.
		Vector3 newCameraPos = Camera.main.transform.position;
		newCameraPos.x = middlePoint.x;
		Camera.main.transform.position = newCameraPos;

		// Find the middle point between players.
		Vector3 vectorBetweenPlayers = player2.position - player1.position;
		middlePoint = player1.position + 0.5f * vectorBetweenPlayers;

		// Calculate the new distance.
		distanceBetweenPlayers = vectorBetweenPlayers.magnitude;
		cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;
		middlePoint.z -= 5;
		// Set camera to new position.
		Vector3 dir = (Camera.main.transform.position - middlePoint).normalized;
		Vector3 dest = middlePoint + dir * (cameraDistance + DISTANCE_MARGIN);
		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, dest, Time.fixedDeltaTime);
		Camera.main.transform.LookAt (middlePoint);
	}


}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//// This amazing code is from:
//// https://stackoverflow.com/questions/22015697/how-to-keep-2-objects-in-view-at-all-time-by-scaling-the-field-of-view-or-zy
//public class CameraFollows : MonoBehaviour {
//
//	// Min and max in the sense that, beyond these distances, the camera will not update
//	float minDist = 0f;
//	float maxDist = 200f;
//
//	float angleWhenClosest = 75f;
//	float angleWhenFarthest = 33f;
//
//	GameObject center1;
//	GameObject center2;
//
//	GameObject obj1;
//	GameObject obj2;
//
//	Camera camera;
//
//	Vector3 panDir;
//
//	void Start() {
//		camera = this.GetComponent<Camera> ();
//		obj1 = GameObject.Find ("Boat 1");
//		obj2 = GameObject.Find ("Boat 2");
//		center1 = GameObject.Find ("Boat 1").transform.Find("Chest").gameObject;
//		center2 = GameObject.Find ("Boat 2").transform.Find("Chest").gameObject;
//
//		panDir = (GetMidPoint () - transform.position).normalized;
//
//		StartCoroutine (UpdateAngle ());
//		StartCoroutine (UpdatePosition ());
//	}
//
//	void Update() {
//		
//	}
//
//	Vector3 GetMidPoint() {
//		Vector3 vectorBetweenObjects = obj2.transform.position - obj1.transform.position;
//		Vector3 midPoint = obj1.transform.position + 0.5f * vectorBetweenObjects;
//		return midPoint;
//	}
//
//	bool ObjectInView(GameObject obj) {
//		Vector3 screenPoint = camera.WorldToViewportPoint(obj.transform.position);
//		bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
//		return onScreen;
//	}
//
//	IEnumerator UpdatePosition() {
//		float prevTime = Time.time;
//		while (true) {
//			Vector3 midpoint = GetMidPoint ();
//			Vector3 pos = this.transform.position;
//			if (!(ObjectInView (obj1) && ObjectInView (obj2))) {
//
//				transform.Translate  (-transform.forward * (Time.time - prevTime) * 10f, Space.World);
//				print ("Here");
//
//				prevTime = Time.time;
//
//			} else {
////				pos.y -= 1;
////				pos.z += 1;
//			}
//			this.transform.position = pos;
//			yield return null;
//		}
//	}
//
//	IEnumerator UpdateAngle() {
//		while (true) {
//			float proportionOfDistances = GetProportionOfDistances ();
//
//			float angle = GetProportionateAngle (proportionOfDistances);
//
//			Vector3 rotation = this.transform.rotation.eulerAngles;
//			rotation.x = angle;
//			this.transform.eulerAngles = rotation;
//
//			yield return null;
//		}
//	}
//
//	float GetProportionOfDistances() {
//		float distance = Mathf.Min(Vector3.Distance (center1.transform.position, center2.transform.position), maxDist);
//		float proportion = (distance - minDist) / (maxDist - minDist);
//		return proportion;
//	}
//
//	float GetProportionateAngle(float proportion) {
//		float angle = angleWhenClosest - (proportion * (angleWhenClosest - angleWhenFarthest));
//		return angle;
//	}
//
////	public string nameOfGameObject1 = "Boat 1";
////	public string nameOfGameObject2 = "Boat 2";
////	public float cameraMovementDampTime = .15f;
////	Transform player1;
////	Transform player2;
////
////	private const float DISTANCE_MARGIN = 8.0f;
////
////	private Vector3 middlePoint;
////	private float distanceFromMiddlePoint;
////	private float distanceBetweenPlayers;
////	private float cameraDistance;
////	private float aspectRatio;
////	private float fov;
////	private float tanFov;
////
////	Vector3 cameraOffset;
////
////	void Start() {
////
////		player1 = GameObject.Find(nameOfGameObject1).transform.Find ("Chest");
////		player2 = GameObject.Find(nameOfGameObject2).transform.Find ("Chest");
////
////		Vector3 midPoint = GetMidPoint ();
////		cameraOffset = Camera.main.transform.position - midPoint;
////
////		aspectRatio = Screen.width / Screen.height;
////		tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);
////	}
////
////	Vector3 GetMidPoint() {
////		Vector3 vectorBetweenPlayers = player2.position - player1.position;
////		Vector3 midPoint = player1.position + 0.5f * vectorBetweenPlayers;
////		return midPoint;
////	}
////
////	void Update () {
////		Vector3 velocity = Vector3.zero;
////		Vector3 midPoint = GetMidPoint ();
////		Vector3 destination = midPoint + cameraOffset;
////
////		Vector3 vectorBetweenPlayers = player2.position - player1.position;
////		distanceBetweenPlayers = Mathf.Max(vectorBetweenPlayers.magnitude, 0f);
////		cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;
////		destination.y = cameraDistance;
////		midPoint.z -= 8f;
////		Camera.main.transform.LookAt (midPoint);
////		Camera.main.transform.position = destination;
////	}
//}
