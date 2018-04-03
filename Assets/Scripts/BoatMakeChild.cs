using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMakeChild : MonoBehaviour {

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "AmmoGun") {
            other.transform.parent = transform.parent;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.transform.parent = null;
        }
    }

}
