using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene_to_ScoreBoard : MonoBehaviour {

    float timer = 4f;
    bool transition = true;

	// Update is called once per frame
	void Update () {

        if (transition)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                transition = false;
                GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<SceneTransition>().QueueSceneTransition("Scoreboard");
            }
        }
	}
}
