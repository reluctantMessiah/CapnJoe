using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnGet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //disabled for now, coin will only be destroyed when player picks up a coin (in PlayerPointManager)
    //
    //void OnTriggerEnter(Collider other) {
    //    if (other.CompareTag("Player"))
    //        Destroy(this.gameObject);
    //}

}
