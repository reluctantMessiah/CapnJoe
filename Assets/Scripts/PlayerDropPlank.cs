using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerDropPlank : MonoBehaviour {

    private InputDevice inputDevice;
    private GameObject plank;
    private bool onTrigger;

	// Use this for initialization
	void Start () {
        inputDevice = GetComponent<PlayerInputDevice>().GetPlayerInputDevice();
	}
	
	// Update is called once per frame
	void Update () {
        if (onTrigger) {
            if (inputDevice.Action2.WasPressed) {
                if (plank != null) {
                    plank.GetComponent<DropPlank>().Drop();
                }
            }
        }
	}

    public void SetOnTrigger(bool value) {
        onTrigger = value;
    }

    public void SetPlank(GameObject plank_in) {
        plank = plank_in;
    }
}
