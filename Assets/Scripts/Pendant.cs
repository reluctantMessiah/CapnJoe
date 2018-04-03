using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendant : MonoBehaviour {

    public Transform startPosition;
    public Transform endPosition;
    public GameObject pendant;

    private Coroutine movePendant;
    private bool alreadyStarted;

    public void Activate() {
        pendant.SetActive(true);

        if (!alreadyStarted) {
            alreadyStarted = true;
            movePendant = StartCoroutine(MovePendant());
        }
    }

    public void Deactivate() {
        if (movePendant != null) {
            StopCoroutine(MovePendant());
        }

        pendant.SetActive(false);

        alreadyStarted = false;
    }

    IEnumerator MovePendant() {
        pendant.transform.position = startPosition.position;

        while (pendant.transform.position.y < endPosition.position.y) {
            pendant.transform.position = Vector3.MoveTowards(pendant.transform.position, endPosition.position, Time.deltaTime * 4f);
            yield return null;
        }

        pendant.transform.position = endPosition.position;
    }

}
