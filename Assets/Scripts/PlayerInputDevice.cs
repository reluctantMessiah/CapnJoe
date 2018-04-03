using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerInputDevice : MonoBehaviour {

    public bool animate;
    public int devicesIndex;

    private InputDevice inputDevice; 
    private int playerID; 

	void Awake () {
        Debug.Log("Number of connected controllers: " + InputManager.Devices.Count);
        Debug.Log("My playerID " + playerID);

        if (animate)
        {
            inputDevice = InputManager.Devices[devicesIndex];
            return;
        }

        //note: for now, we'll be accessing devices array manually to get the input devices for each player
        if (InputManager.Devices.Count > 0) {
            inputDevice = InputManager.Devices[playerID];
        }
        else {
            inputDevice = null;
        }
	}
	
    public InputDevice GetPlayerInputDevice() {
        return inputDevice;
    }

    public void SetPlayerID(int playerID_in) {
        playerID = playerID_in;
    }

}
