using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    public GameObject panelLeft;
    public GameObject panelRight;
    public float transitionSpeed;

    private LoadNextScene loadNextScene;

	// Use this for initialization
	void Start () {
        StartCoroutine(TransitionIn());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void QueueSceneTransition (string sceneToLoad) {
        StartCoroutine(Transition(sceneToLoad));
    }

    IEnumerator Transition(string sceneToLoad) {
        while (panelLeft.transform.localPosition.x < 10f && panelRight.transform.localPosition.x > -10f) {
            panelLeft.transform.localPosition = Vector3.MoveTowards(panelLeft.transform.localPosition, new Vector3(10f, 0f, 0f), Time.deltaTime * transitionSpeed);
            panelRight.transform.localPosition = Vector3.MoveTowards(panelRight.transform.localPosition, new Vector3(-10f, 0f, 0f), Time.deltaTime * transitionSpeed);

            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator TransitionIn() {
        while (panelLeft.transform.localPosition.x > -1950f && panelRight.transform.localPosition.x < 1950f) {
            panelLeft.transform.localPosition = Vector3.MoveTowards(panelLeft.transform.localPosition, new Vector3(-1950f, 0f, 0f), Time.deltaTime * transitionSpeed);
            panelRight.transform.localPosition = Vector3.MoveTowards(panelRight.transform.localPosition, new Vector3(1950f, 0f, 0f), Time.deltaTime * transitionSpeed);

            yield return null;
        }
    }

}
