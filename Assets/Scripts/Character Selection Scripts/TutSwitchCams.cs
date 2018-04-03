using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutSwitchCams : MonoBehaviour {

	public Camera otherCam;

	// Use this for initialization
	void Start () {
		StartCoroutine(WaitAndSwitch());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator WaitAndSwitch() { 
		yield return new WaitForSeconds(3f);
		this.GetComponent<Camera>().enabled = false;
		otherCam.enabled = true;
	}
}
