using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialTimer : MonoBehaviour {

    public string sceneToLoad;
    public float timeTilNextScene;

    private Text timer;

    void Start() {
        timer = GetComponent<Text>();
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene() {
        while (timeTilNextScene > 0f) {
            timeTilNextScene -= Time.deltaTime;
            if (timeTilNextScene <= 9.5f) {
                timer.text = "00:" + "0" + timeTilNextScene.ToString("F0");
            }
            else {
                timer.text = "00:" + timeTilNextScene.ToString("F0");
            }
            
            yield return null;
        }

        StartCoroutine(DisableAllControllersFor(2.5f));
        yield return new WaitForSeconds(3f);

        if (sceneToLoad == "MutinyScene") {
            GameObject.Find("ToMainScene").GetComponent<ToMainScene>().DestroyThings();
        }

        GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<SceneTransition>().QueueSceneTransition(sceneToLoad);
    }

    IEnumerator DisableAllControllersFor(float time) {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players) {
            player.GetComponent<PlayerMovement>().StopMovementManual(true);
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().isKinematic = true;
        }

        var inControl = GameObject.Find("InControl");
        inControl.SetActive(false);

        while (time > 0f) {
            time -= Time.deltaTime;
            yield return null;
        }

        inControl.SetActive(true);
    }

}
