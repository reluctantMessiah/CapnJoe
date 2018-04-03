using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderWarning : MonoBehaviour {

    public AudioClip alertSound;

    public GameObject textBox;
    public GameObject text;

    public float timeBeforeWarning;
    public float timeTilWarningDies;

    void Activate() {
        AudioSource.PlayClipAtPoint(alertSound, Camera.main.gameObject.transform.position);
        textBox.SetActive(true);
        text.GetComponent<BoulderWarningText>().Activate();
    }

    void Deactivate() {
        textBox.SetActive(false);
        text.GetComponent<BoulderWarningText>().Deactivate();
    }

    IEnumerator FlashWarning() {
        yield return new WaitForSeconds(timeBeforeWarning);
        Activate();
        yield return new WaitForSeconds(timeTilWarningDies);
        Deactivate();
    }

    public void Warn() {
        StartCoroutine(FlashWarning());
    }

}
