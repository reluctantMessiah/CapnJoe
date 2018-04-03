using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour {

    private Text text; 
    private Coroutine fadeCoroutine;

	// Use this for initialization
	void Awake () {
        text = GetComponent<Text>();
	}

    IEnumerator Fade(Color opaque, Color transparent) {
        float p = 0f;

        while (p < 1f) {
            text.color = Color.Lerp(opaque, transparent, p);
            p += Time.deltaTime;
            yield return null;
        }

        p = 0f;
        text.color = transparent;

        while (p < 1f) {
            text.color = Color.Lerp(transparent, opaque, p);
            p += Time.deltaTime;
            yield return null;
        }

        text.color = opaque;
        StartCoroutine(Fade(opaque, transparent));
    }

    public void Activate(Color color) {
        text.enabled = true;

        text.color = color;

        if (fadeCoroutine == null) {
            fadeCoroutine = StartCoroutine(Fade(new Color(text.color.r, text.color.b, text.color.g, 1f), new Color(text.color.r, text.color.b, text.color.g, 0f)));
        }
    }

    public void Deactivate() {
        if (fadeCoroutine != null) {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = null;

        text.enabled = false;
    }

}
