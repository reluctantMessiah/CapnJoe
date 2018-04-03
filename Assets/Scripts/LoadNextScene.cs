using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour {

	public string nameOfNextScene;

	public void LoadScene() {
		//SceneManager.LoadScene(nameOfNextScene);
        GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<SceneTransition>().QueueSceneTransition(nameOfNextScene);
    }
}
