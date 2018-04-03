using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPoints : MonoBehaviour {

	public int teamNumber;

	GemManager gemManager;
	Text scoreText;

	// Use this for initialization
	void Start () {
        gemManager = GameObject.Find("Gem Manager").GetComponent<GemManager>();
        scoreText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		int numGems = gemManager.GetGemCountOfTeam (teamNumber);
        scoreText.text = numGems.ToString() + "/6";
	}
}
