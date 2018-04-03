using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoamSpawner : MonoBehaviour {

    public GameObject[] foams;
    public float timeTilSpawn;
    public bool isLeft;

	// Use this for initialization
	void Start () {
        StartCoroutine(FoamSpawn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FoamSpawn() {
        GameObject spawn;

        if (isLeft) {
            spawn = Instantiate(foams[0], transform.position, Quaternion.Euler(90f, 0f, -180f));
        } 
        else {
            spawn = Instantiate(foams[0], transform.position, Quaternion.Euler(-90f, 0f, 180f));
        }

        spawn.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(timeTilSpawn);
        StartCoroutine(FoamSpawn());
    }

}
