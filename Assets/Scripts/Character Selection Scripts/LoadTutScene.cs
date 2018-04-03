using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTutScene : MonoBehaviour {

	public float delayTillLoad = 5f;
	float timeSinceStart = 0f;
	
	// Update is called once per frame
	void Update () {

		if (timeSinceStart > delayTillLoad) {
			SceneManager.LoadScene ("CharacterSelectionScene");
		} else {
			timeSinceStart += Time.deltaTime;
		}
	}
}
