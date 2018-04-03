using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class PressStartToSkip : MonoBehaviour {

    public GameObject pressStart;
    public string sceneToLoad;

    private InputDevice input;

    private

    // Use this for initialization
    void Start() {
        pressStart.GetComponent<TextFade>().Activate(Color.white);
    }

    // Update is called once per frame
    void Update() {
        input = InputManager.ActiveDevice;

        if (input.GetControl(InputControlType.Start)) {
            Destroy(GameObject.Find("OST Theme Tutorial"));
            Destroy(GameObject.Find("PressStartToSkipCanvas"));
            GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<SceneTransition>().QueueSceneTransition(sceneToLoad);
            Destroy(gameObject);
        }
    }

}
