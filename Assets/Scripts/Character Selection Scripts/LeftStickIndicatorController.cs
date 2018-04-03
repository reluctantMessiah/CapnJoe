using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftStickIndicatorController : MonoBehaviour {

	public GameObject indicator;

	public void DisplayIndicator() {
		indicator.SetActive(true);
	}

	public void RemoveIndicator() {
		indicator.SetActive(false);
	}
}
