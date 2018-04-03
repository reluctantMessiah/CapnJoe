using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class BoatSteering : MonoBehaviour {

    public GameObject rudder;
    public Transform originRotation;
    public Transform leftRotation;
    public Transform rightRotation;

    public ParticleSystem foamParticlesRight;
    public ParticleSystem foamParticlesLeft;
    public float steerSpeed = 600000f;

    private InputDevice inputDevice;
    private Rigidbody rb;
    private Coroutine boatSteer;
    private bool steeringMode;
    
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }

    public void SetSteeringMode(bool value, InputDevice inputDevice_in = null) {
        steeringMode = value;
        inputDevice = inputDevice_in;

        if (boatSteer == null) {
            boatSteer = StartCoroutine(BoatSteer());
        }
        
    }

    public bool GetSteeringMode() {
        return steeringMode;
    }

    void Update() {
        var emissionRight = foamParticlesRight.emission;
        var emissionLeft = foamParticlesLeft.emission;

        if (rb.velocity.z > 0f) {
            emissionRight.rateOverTime = 15f;
            emissionLeft.rateOverTime = 15f;
        } 
        else {
            emissionRight.rateOverTime = 0f;
            emissionLeft.rateOverTime = 0f;
        }
    }

    IEnumerator BoatSteer() {
        while (true) {
            if (!steeringMode) {
                boatSteer = null;
                yield break;
            }

            float x = inputDevice.LeftStickX;
            float z = inputDevice.LeftStickY;

			if (x == 0 && z == 0) {
				
			}

            if (x > 0f) {
                rudder.transform.rotation = Quaternion.Lerp(originRotation.rotation, leftRotation.rotation, Mathf.Abs(x));
            }

            if (x < 0f) {
                rudder.transform.rotation = Quaternion.Lerp(originRotation.rotation, rightRotation.rotation, Mathf.Abs(x));
            }

            rb.AddForce(new Vector3(x, 0f, z).normalized * steerSpeed);
        
            yield return null;
        }
    }

}
