using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallTriggerScript : MonoBehaviour {

    public GameObject xbox360_x;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            //set icons
            if (!other.gameObject.GetComponent<PlayerInventory>().HasCannonBall()) {
                xbox360_x.SetActive(true);
            } 
            else {
                xbox360_x.SetActive(false);
            }

            other.gameObject.GetComponent<PlayerInventory>().SetOnTriggerCannonball(true);       
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            //set icons
            xbox360_x.SetActive(false);

            other.gameObject.GetComponent<PlayerInventory>().SetOnTriggerCannonball(false);
        }
    }

}
