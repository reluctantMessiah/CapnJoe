using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMainScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DestroyThings() {
        Destroy(GameObject.Find("OST Theme Tutorial"));
        Destroy(GameObject.Find("PressStartToSkip"));
        Destroy(GameObject.Find("PressStartToSkipCanvas"));
    }

}
