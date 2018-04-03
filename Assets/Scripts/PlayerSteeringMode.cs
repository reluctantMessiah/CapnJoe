using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerSteeringMode : MonoBehaviour {

    private InputDevice inputDevice;
    private Rigidbody rb; 
    private GameObject myBoat;
    private GameObject wheel;
    private bool steeringMode;
    private bool onTrigger;

    private bool grabNextInput = true;

	private BoulderDetector boulderDetector;

    void Start() {
        inputDevice = GetComponent<PlayerInputDevice>().GetPlayerInputDevice();
        rb = GetComponent<Rigidbody>();
        
        StartCoroutine(FindBoulderDetector());

    }

    IEnumerator FindBoulderDetector()
    {
        while (transform.parent == null || boulderDetector == null)
        {
            if (transform.parent != null)
                boulderDetector = transform.parent.GetComponentInChildren<BoulderDetector>();
            yield return null;
        }

        print("here");
        
    }

    void Update() {
        if (onTrigger) {
            if (inputDevice.GetControl(InputControlType.Action2) && grabNextInput) {
                if (!steeringMode && !myBoat.GetComponent<BoatSteering>().GetSteeringMode()) {
                    //put the player in steering mode
                    SetPlayerSteeringMode(false);
                    transform.position = wheel.transform.position + Vector3.up * 1.5f + -wheel.transform.forward.normalized;
                    transform.rotation = wheel.transform.rotation;

                    //myBoat is now in steering mode
                    myBoat.GetComponent<BoatSteering>().SetSteeringMode(true, inputDevice);

                    grabNextInput = false;
                    StartCoroutine(WaitBetweenInputs());
                }
            }

            if (inputDevice.GetControl(InputControlType.Action2) && grabNextInput) {
                if (steeringMode && myBoat.GetComponent<BoatSteering>().GetSteeringMode()) {
                    //take the player out of steering mode
                    SetPlayerSteeringMode(true);

                    //myBoat out of steering mode
                    myBoat.GetComponent<BoatSteering>().SetSteeringMode(false);

                    grabNextInput = false;
                    StartCoroutine(WaitBetweenInputs());
                }
            }
        }

        if (steeringMode) {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
        else {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    public void SetPlayerSteeringMode(bool value) {
        if (!value) {
            steeringMode = true;
        }
        else {
            steeringMode = false;
            myBoat.GetComponent<BoatSteering>().SetSteeringMode(false);
        }
        if (boulderDetector != null)
        {
            boulderDetector.SetBoatIsManned(steeringMode, this.gameObject);
        }
        //StopMovementManual: true disables movement, false enables movement
        GetComponent<PlayerMovement>().StopMovementManual(!value);
        GetComponent<PlayerAttack>().enabled = value;
    }

    public bool GetSteeringMode() {
        return steeringMode;
    }

    public GameObject GetMyBoat() {
        return myBoat;
    }

    public void GetBoatInfo(GameObject myBoat_in, GameObject wheel_in) {
        myBoat = myBoat_in;
        wheel = wheel_in;
    }

    public void SetOnTrigger(bool value) {
        onTrigger = value;
    }

    IEnumerator WaitBetweenInputs() {
        float time = 1f; 

        while (time > 0f) {
            time -= Time.deltaTime;
            yield return null;
        }

        grabNextInput = true;
    }

}
