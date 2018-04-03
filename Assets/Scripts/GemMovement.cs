using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMovement : MonoBehaviour {

    public float rotationSpeed;
    public float floatTime;
    public float floatSpeed;

	// Use this for initialization
	void Start () {
        StartCoroutine(Float());
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
	}

    IEnumerator Float() {
        float t = Time.time + floatTime;

        while(Time.time < t) {
            transform.Translate(Vector3.forward * floatSpeed * Time.deltaTime);
            yield return null;
        }

        t = Time.time + floatTime;

        while (Time.time < t) {
            transform.Translate(Vector3.back * floatSpeed * Time.deltaTime);
            yield return null;
        }

        StartCoroutine(Float());
    }

}
