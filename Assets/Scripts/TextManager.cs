using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class TextManager : MonoBehaviour {

    public GameObject timerTextBox;
    public GameObject timerText;

    public GameObject playerInventory;
    public GameObject playerStatus;
    public GameObject playerTexts;

    public GameObject playerSpawner_a;
    public GameObject playerSpawner_b;

    public GameObject boulderGen;

    public GameObject portrait_capnJoe;
    public GameObject textBox_capnJoe;
    public List<string> lines_capnJoe = new List<string>();

    public GameObject textBox_narration;
    public List<string> lines_narration = new List<string>();

    public bool printNarrationWhole;
    public GameObject[] narrationPanels;

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
        textBox_narration.SetActive(false);
        pressAToContinue.GetComponent<TextFade>().Deactivate();
        pressYToSkip.GetComponent<TextFade>().Deactivate();
        StartCoroutine(PrintCapnJoe(lines_capnJoe[0]));
    }

    // Update is called once per frame
    void Update() {
        input = InputManager.ActiveDevice;

        if (input.Action1.IsPressed) {
            actionPressed = true;
        } 
        else {
            actionPressed = false;
        }

        //skip the intro
        if (input.Action4.WasPressed) {
            Destroy(portrait_capnJoe);
            Destroy(textBox_capnJoe);
            Destroy(textBox_narration);
            if (buffer != null) {
                Destroy(buffer.gameObject);
            }
            Destroy(pressAToContinue);
            Destroy(pressYToSkip);

            foreach (GameObject panel in narrationPanels) {
                if (panel != null) {
                    panel.GetComponent<NarrationPanel>().Deactivate();
                }
                Destroy(panel);
            }

            Destroy(gameObject);

            //if this is steering scene
            if (boulderGen != null) {
                boulderGen.SetActive(true);
            }

            playerInventory.SetActive(true);
            playerStatus.SetActive(true);
            playerTexts.SetActive(true);

            //spawn players in
            playerSpawner_a.SetActive(true);
            playerSpawner_b.SetActive(true);

            ActivateTimer();
        }
    }

    IEnumerator PrintCapnJoe(string line) {
        portrait_capnJoe.SetActive(true);
        textBox_capnJoe.SetActive(true);

        buffer.transform.localPosition = textBox_capnJoe.transform.localPosition;
        buffer.text = "";
        buffer.color = Color.black;
        
        pressAToContinue.transform.localPosition = new Vector3(0f, 150f, 0f);

        pressYToSkip.transform.localPosition = new Vector3(0f, 450f, 0f);
        pressYToSkip.GetComponent<TextFade>().Activate(Color.black);

        line = line.Replace("@", System.Environment.NewLine);
        string blue = "<color=#0000ff>Blue</color>";
        string red = "<color=#ff0000>Red</color>";

        int i = 0;
        while (i < line.Length) {
            if (line[i] == 'B' && line[i + 1] == 'l') {
                buffer.text += blue;
                i += 4;
            } 
            else if (line[i] == 'R') {
                buffer.text += red;
                i += 3;
            } 
            else {
                buffer.text += line[i];
                ++i;
            }

            yield return new WaitForSeconds(0.05f);
        }

        pressAToContinue.GetComponent<TextFade>().Activate(Color.black);

        while (!actionPressed) {
            yield return null;
        }

        pressAToContinue.GetComponent<TextFade>().Deactivate();

        if (++currentLine < lines_capnJoe.Count) {
            StartCoroutine(PrintCapnJoe(lines_capnJoe[currentLine]));
        } 
        else {
            if (printNarrationWhole) {
                Destroy(portrait_capnJoe);
                Destroy(textBox_capnJoe);
                pressAToContinue.GetComponent<TextFade>().Deactivate();
                pressYToSkip.GetComponent<TextFade>().Deactivate();
                currentLine = 0;
                Destroy(buffer.gameObject);
                buffer = null;
                StartCoroutine(PrintNarrationWhole(0));
            } 
            else {
                Destroy(portrait_capnJoe);
                Destroy(textBox_capnJoe);
                pressAToContinue.GetComponent<TextFade>().Deactivate();
                pressYToSkip.GetComponent<TextFade>().Deactivate();
                currentLine = 0;
                StartCoroutine(PrintNarration(lines_narration[0]));
            }
        }
    }

    IEnumerator PrintNarration(string line) {
        textBox_narration.SetActive(true);

        buffer.transform.localPosition = textBox_narration.transform.localPosition;
        buffer.text = "";
        buffer.color = Color.white;

        pressAToContinue.transform.localPosition = new Vector3(0f, -315f, 0f);

        pressYToSkip.transform.localPosition = new Vector3(0f, 315f, 0f);
        pressYToSkip.GetComponent<TextFade>().Activate(Color.white);

        line = line.Replace("@", System.Environment.NewLine);
        string blue = "<color=#0000ff>Blue</color>";
        string red = "<color=#ff0000>Red</color>";

        int i = 0;
        while (i < line.Length) {
            if (line[i] == 'B' && line[i + 1] == 'l') {
                buffer.text += blue;
                i += 4;
            } 
            else if (line[i] == 'R') {
                buffer.text += red;
                i += 3;
            } 
            else {
                buffer.text += line[i];
                ++i;
            }

            yield return new WaitForSeconds(0.05f);
        }

        pressAToContinue.GetComponent<TextFade>().Activate(Color.white);

        while (!actionPressed) {
            yield return null;
        }

        pressAToContinue.GetComponent<TextFade>().Deactivate();

        if (++currentLine < lines_narration.Count) {
            StartCoroutine(PrintNarration(lines_narration[currentLine]));
        } 
        else {
            Destroy(textBox_narration);
            Destroy(buffer.gameObject);
            Destroy(pressAToContinue);
            Destroy(pressYToSkip);
            Destroy(gameObject);

            //spawn players in
        }
    }

    IEnumerator PrintNarrationWhole(int i) {
        textBox_narration.SetActive(true);
        narrationPanels[i].SetActive(true);

        pressAToContinue.transform.localPosition = new Vector3(0f, -315f, 0f);

        pressYToSkip.transform.localPosition = new Vector3(0f, 315f, 0f);
        pressYToSkip.GetComponent<TextFade>().Activate(Color.white);

        Debug.Log(i);

        narrationPanels[i].GetComponent<NarrationPanel>().Activate();

        yield return StartCoroutine(ReadyToProceed());

        pressAToContinue.GetComponent<TextFade>().Activate(Color.white);

        while (!actionPressed) {
            yield return null;
        }

        narrationPanels[i].GetComponent<NarrationPanel>().Deactivate();
        Destroy(narrationPanels[i]);
        narrationPanels[i] = null;

        pressAToContinue.GetComponent<TextFade>().Deactivate();

        if (++i < narrationPanels.Length) {
            StartCoroutine(PrintNarrationWhole(i));
        }
        else {
            Destroy(textBox_narration);
            Destroy(pressAToContinue);
            Destroy(pressYToSkip);
            Destroy(gameObject);
            
            //if this is steering scene
            if (boulderGen != null) {
                boulderGen.SetActive(true);
            }

            playerInventory.SetActive(true);
            playerStatus.SetActive(true);
            playerTexts.SetActive(true);

            //spawn players in
            playerSpawner_a.SetActive(true);
            playerSpawner_b.SetActive(true);

            ActivateTimer();
        }
    }


    IEnumerator ReadyToProceed() {
        float waitTime = Time.time + 2f;

        while (Time.time < waitTime) {
            yield return null;
        }
    }

    void ActivateTimer() {
        timerTextBox.SetActive(true);
        timerText.SetActive(true);
    }

}
