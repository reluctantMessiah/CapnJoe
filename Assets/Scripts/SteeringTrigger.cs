using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringTrigger : MonoBehaviour {

    public GameObject myBoat;
    public GameObject xbox360_x; 
    public GameObject xbox360_b; 

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player") {
            //set icons
            if (!myBoat.GetComponent<BoatSteering>().GetSteeringMode()) {
                xbox360_x.SetActive(true);
                xbox360_b.SetActive(false);
            }
            else {
                xbox360_x.SetActive(false);
                xbox360_b.SetActive(true);
            }

            other.gameObject.GetComponent<PlayerSteeringMode>().GetBoatInfo(myBoat, gameObject);
            other.gameObject.GetComponent<PlayerSteeringMode>().SetOnTrigger(true);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            //set icons
            xbox360_x.SetActive(false);
            xbox360_b.SetActive(false);

            other.gameObject.GetComponent<PlayerSteeringMode>().GetBoatInfo(null, null);
            other.gameObject.GetComponent<PlayerSteeringMode>().SetOnTrigger(false);
        }
    }

}
