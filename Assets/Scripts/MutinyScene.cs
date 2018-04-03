using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class MutinyScene : MonoBehaviour {

    public GameObject capnJoe;
    public GameObject capnJoe_worried;

    public GameObject portrait_capnJoe_worried;
    public GameObject portrait_capnJoe;

    public GameObject textBox_capnJoe;

    public List<string> lines_capnJoe = new List<string>();

    public Text buffer;

    public GameObject pressAToContinue;
    public GameObject pressYToSkip;

    private InputDevice input;

    private int currentLine;
    private bool actionPressed;

    // Use this for initialization
    void Start() {
        portrait_capnJoe.SetActive(false);
        textBox_capnJoe.SetActive(false);
        pressAToContinue.GetComponent<TextFade>().Deactivate();
        pressYToSkip.GetComponent<TextFade>().Deactivate();
        StartCoroutine(PrintCapnJoe(lines_capnJoe[0]));
    }

    // Update is called once per frame
    void Update() {
        input = InputManager.ActiveDevice;

        if (input.Action1.IsPressed) {
            actionPressed = true;
        } else {
            actionPressed = false;
        }

        //skip the intro
        if (input.Action4.WasPressed) {
            Destroy(portrait_capnJoe);
            Destroy(portrait_capnJoe_worried);
            Destroy(textBox_capnJoe);
            if (buffer != null) {
                Destroy(buffer.gameObject);
            }
            Destroy(pressAToContinue);
            Destroy(pressYToSkip);

            //load
            GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<SceneTransition>().QueueSceneTransition("James_LAB3");

            //Destroy(gameObject);
        }
    }

    IEnumerator PrintCapnJoe(string line) {
        portrait_capnJoe.SetActive(true);
        textBox_capnJoe.SetActive(true);

        buffer.transform.localPosition = textBox_capnJoe.transform.localPosition;
        buffer.text = "";
        buffer.color = Color.black;

        pressAToContinue.transform.localPosition = new Vector3(0f, -450f, 0f);

        pressYToSkip.transform.localPosition = new Vector3(0f, -150f, 0f);
        pressYToSkip.GetComponent<TextFade>().Activate(Color.black);

        line = line.Replace("@", System.Environment.NewLine);

        int i = 0;
        while (i < line.Length) {
            buffer.text += line[i];
            ++i;

            yield return new WaitForSeconds(0.05f);
        }

        pressAToContinue.GetComponent<TextFade>().Activate(Color.black);

        while (!actionPressed) {
            yield return null;
        }

        pressAToContinue.GetComponent<TextFade>().Deactivate();

        if (++currentLine < lines_capnJoe.Count) {
            if (currentLine == 7) {
                GameObject.Find("JoeMover").GetComponent<JoeMover>().Shock();
            }

            if (currentLine == 8) {
                Instantiate(capnJoe_worried, capnJoe.transform.position, capnJoe.transform.rotation);
                Destroy(capnJoe);

                portrait_capnJoe.SetActive(false);
                portrait_capnJoe_worried.SetActive(true);

                GameObject.Find("JoeMover").GetComponent<JoeMover>().Mutiny();
            }

            StartCoroutine(PrintCapnJoe(lines_capnJoe[currentLine]));
        } 
        else {
            Destroy(portrait_capnJoe);
            Destroy(portrait_capnJoe_worried);
            Destroy(textBox_capnJoe);
            pressAToContinue.GetComponent<TextFade>().Deactivate();
            pressYToSkip.GetComponent<TextFade>().Deactivate();
            Destroy(buffer.gameObject);
            currentLine = 0;

            //load
            GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<SceneTransition>().QueueSceneTransition("James_LAB3");
        }
    }

    IEnumerator ReadyToProceed() {
        float waitTime = Time.time + 2f;

        while (Time.time < waitTime) {
            yield return null;
        }
    }

}
