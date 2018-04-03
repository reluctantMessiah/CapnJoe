using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMarkerPositionsController : MonoBehaviour {

	public float markerYIncDist = 1.5f;

	List<GameObject> pMarkers = new List<GameObject>();
	public GameObject[] startingPMarkers;
	public ReadyController readyController;
	public LeftStickIndicatorController leftStickController;
	public bool isMiddle = false;

	bool hasReadyPlayer = false;

	void Start() {
		foreach (GameObject pMarker in startingPMarkers) {
			pMarkers.Add(pMarker);
		}

		RepositionPMarkers();
	}

	// Returns index of new pMarker in list
	public void AddPMarker(GameObject pMarkerObj) {
		pMarkers.Add(pMarkerObj);

		RepositionPMarkers();
		if(!isMiddle && !hasReadyPlayer)
			MaybeChangeReadyController();
	}

	public void RemoveMarker(GameObject markerToRemove) {

		pMarkers.Remove(markerToRemove);

		RepositionPMarkers();
		if(!isMiddle && !hasReadyPlayer)
			MaybeChangeReadyController();
	}

	public bool ReadyUpMarker(GameObject markerToReady) {
		if (hasReadyPlayer)
			return false;

		// Move to front
		pMarkers.Remove(markerToReady);
		pMarkers.Insert(0, markerToReady);

		RepositionPMarkers();

		leftStickController.RemoveIndicator();

		hasReadyPlayer = true;

		return true;
	}

	public void UnreadyUpMarker(GameObject markerToUnready) {

		// Make sure this is the same player that readied
		if (markerToUnready != pMarkers[0])
			return;

		hasReadyPlayer = false;

		leftStickController.DisplayIndicator();
	}

	void RepositionPMarkers() {
		int index = 0;
		foreach (GameObject pMarker in pMarkers) {

			Vector3 newPos = this.transform.position;
			newPos.y -= index * markerYIncDist;
			pMarker.transform.position = newPos;

			++index;
		}
	}

	void MaybeChangeReadyController() {
		if (pMarkers.Count == 0)
		{
			readyController.RemoveA();
			leftStickController.RemoveIndicator();
		}
		else { 
			readyController.DisplayA();
			leftStickController.DisplayIndicator();
		}
	}
}
