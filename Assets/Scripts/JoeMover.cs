using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeMover : MonoBehaviour {

    public GameObject[] exclamations;
    public GameObject[] joes;

    public void Shock() {
        foreach(GameObject exclamation in exclamations) {
            exclamation.SetActive(true);
        }
    }

    public void Mutiny() {
        StartCoroutine(MoveJoes());
    }

    IEnumerator MoveJoes() {
        float t = 0f; 

        while(t < 2f) {
            joes[0].transform.position = Vector3.MoveTowards(joes[0].transform.position, joes[0].transform.position + joes[0].transform.forward, Time.deltaTime);
            joes[1].transform.position = Vector3.MoveTowards(joes[1].transform.position, joes[1].transform.position + joes[1].transform.forward, Time.deltaTime);
            joes[2].transform.position = Vector3.MoveTowards(joes[2].transform.position, joes[2].transform.position + joes[2].transform.forward, Time.deltaTime);
            joes[3].transform.position = Vector3.MoveTowards(joes[3].transform.position, joes[3].transform.position + joes[3].transform.forward, Time.deltaTime);
            t += Time.deltaTime;
            yield return null; 
        }
    }

}
