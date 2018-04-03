using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerCannon : MonoBehaviour {

    private InputDevice inputDevice;
    private CannonShootTrigger cannon;
    private PlayerInventory inventory;
    private bool onTrigger;

	// Use this for initialization
	void Start () {
        inputDevice = GetComponent<PlayerInputDevice>().GetPlayerInputDevice();
        inventory = GetComponent<PlayerInventory>();
	}
	
	// Update is called once per frame
	void Update () {
		if (onTrigger) {
            if (inputDevice.Action2.WasPressed && inventory.HasCannonBall() && cannon != null) {
                cannon.Shoot(gameObject);
            }
        }
	}

    public void SetOnTrigger(bool value) {
        onTrigger = value;
    }

    public void SetCannon(CannonShootTrigger cannon_in) {
        cannon = cannon_in;
    }
    public bool GetOnTrigger()
    {
        return onTrigger;
    }
}
