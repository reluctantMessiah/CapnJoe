using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class OneupScreen : MonoBehaviour {

    public GameObject textBox;
    public Text buffer;
    public List<string> lines = new List<string>();
    public GameObject playerSpawnerA;
    public GameObject playerSpawnerB;
    public GameObject boulderGenerator;
    public GameObject playerInventory;
    public GameObject playerStatus;
    public GameObject playerTexts;

    public GameObject pressAToContinue;
    public GameObject pressYToSkip;

    private InputDevice input;
    private int currentLine;
    private bool actionPressed;

	// Use this for initialization
	void Start () {
        pressYToSkip.GetComponent<TextFade>().Activate(Color.white);
        StartCoroutine(Print(lines[0]));
	}
	
	// Update is called once per frame
	void Update () {
        input = InputManager.ActiveDevice;

        if (input.Action1.IsPressed) {
            actionPressed = true;
        }
        else {
            actionPressed = false;
        }

        //skip the intro
        if (input.Action4.WasPressed) {
            textBox.SetActive(false);
            buffer.gameObject.SetActive(false);

            playerInventory.SetActive(true);
            playerStatus.SetActive(true);
            playerTexts.SetActive(true);

            playerSpawnerA.SetActive(true);
            playerSpawnerB.SetActive(true);
            boulderGenerator.SetActive(true);
            pressAToContinue.SetActive(false);
            pressYToSkip.SetActive(false);

            Destroy(gameObject);
        }
	}

    IEnumerator Print(string line) {
        buffer.text = "";
        line = line.Replace("@", System.Environment.NewLine);
        string blue = "<color=#0000ff>Blue Crew</color>";
        string red = "<color=#ff0000>Red Crew</color>";

        int i = 0;
        while (i < line.Length) {
            if (line[i] == 'B' && line[i + 1] == 'l') {
                buffer.text += blue;
                i += 9;
            }
            else if (line[i] == 'R') {
                buffer.text += red;
                i += 8;
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

        if (++currentLine < lines.Count) {
            StartCoroutine(Print(lines[currentLine]));
        }
        else {
            textBox.SetActive(false);
            buffer.gameObject.SetActive(false);

            playerInventory.SetActive(true);
            playerStatus.SetActive(true);
            playerTexts.SetActive(true);

            playerSpawnerA.SetActive(true);
            playerSpawnerB.SetActive(true);
            boulderGenerator.SetActive(true);
            pressAToContinue.SetActive(false);
            pressYToSkip.SetActive(false);

            Destroy(gameObject);
        }
    }

}
