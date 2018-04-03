using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoulderWarningText : MonoBehaviour {

    public string[] lines;
    public float lerpSpeed;

    private Text text;
    private Coroutine colorLerpCoroutine;

    void Start() {
        text = GetComponent<Text>();
    }

    IEnumerator ColorLerp() {
        float p = 0f;
        text.color = Color.red;

        while (true) {
            while (p < 1f) {
                text.color = Color.Lerp(Color.red, Color.yellow, p);
                p += Time.deltaTime * lerpSpeed;
                yield return null;
            }

            p = 1f;

            while (p > 0f) {
                text.color = Color.Lerp(Color.red, Color.yellow, p);
                p -= Time.deltaTime * lerpSpeed;
                yield return null;
            }

            p = 0f;
            yield return null;
        }
    }

    public void Activate() {
        text.enabled = true;
        text.text = lines[Random.Range(0, lines.Length)];
        colorLerpCoroutine = StartCoroutine(ColorLerp());
    }

    public void Deactivate() {
        StopCoroutine(colorLerpCoroutine);
        text.enabled = false;
    }

}
