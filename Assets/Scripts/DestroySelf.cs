using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour {

    public float timeTilDestroy;

	// Use this for initialization
	void Start () {
        StartCoroutine(Destroyer());
	}

    IEnumerator Destroyer() {
        yield return new WaitForSeconds(timeTilDestroy);
       // print("SELF DESTRUCT");
        Destroy(gameObject);
    }

}
