using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoamMove : MonoBehaviour {

    public float zSpeed;
    public Vector3 foamScale;

    private float boatSpeed;

    public bool whaleFoam = false;

    void Start() {
        transform.localScale = Vector3.zero;
        StartCoroutine(FoamEnlarge());
    }

    void Update() {

        if (whaleFoam)
        {
            boatSpeed = transform.root.gameObject.GetComponent<Rigidbody>().velocity.x;
            GetComponent<Rigidbody>().velocity = new Vector3(boatSpeed * (-1) + zSpeed, 0f, 0f);
        }
        else
        {
            boatSpeed = transform.root.gameObject.GetComponent<Rigidbody>().velocity.z;
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, boatSpeed + zSpeed);
        }        
    }

    IEnumerator FoamEnlarge() {
        float p = 0f; 

        while (p < 1f) {
            transform.localScale = Vector3.Lerp(Vector3.zero, foamScale, p);
            p += Time.deltaTime;
            yield return null;
        }
    }

}
