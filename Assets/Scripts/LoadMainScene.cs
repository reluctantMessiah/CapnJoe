using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour {

	public float delayTillLoad = 5f;
	float timeSinceStart = 0f;
	
	// Update is called once per frame
	void Update () {

		if (timeSinceStart > delayTillLoad) {
			SceneManager.LoadScene ("Main");
		} else {
			timeSinceStart += Time.deltaTime;
		}
	}
}
