using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

	Text text;
	GameObject pointManager;

	// Use this for initialization
	void Start () {
		text = this.GetComponent<Text> ();
		pointManager = GameObject.Find ("Point Manager");
	}
	
	// Update is called once per frame
	void Update () {
		int secondsLeft = Mathf.RoundToInt(pointManager.GetComponent<PointManager> ().SecondsLeft ());
		if (secondsLeft <= 0) {
			text.text = "0";
		} else {
			text.text = secondsLeft.ToString ();
		}
	}
}
