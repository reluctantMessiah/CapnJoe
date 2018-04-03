using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyController: MonoBehaviour {

	public GameObject readyObj;
	public GameObject aButtObj;

	bool isReady = false;

	public void ToggleReady() {
		if (!isReady)
		{
			isReady = true;
			readyObj.SetActive(true);
			aButtObj.SetActive(false);
		}
		else {
			isReady = false;
			readyObj.SetActive(false);
			aButtObj.SetActive(true);
		}
	}

	public void DisplayA() {
		aButtObj.SetActive(true);
	}

	public void RemoveA() { 
		aButtObj.SetActive(false);
	}
}
