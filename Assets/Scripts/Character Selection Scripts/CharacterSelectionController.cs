using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionController : MonoBehaviour {

	public GameObject[] characterCyclers;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < characterCyclers.Length; ++i) {
			PlayerInputDevice playerInputDevice = characterCyclers[i].GetComponent<PlayerInputDevice>();
			playerInputDevice.SetPlayerID(i);
 		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
