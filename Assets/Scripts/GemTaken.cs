using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemTaken : MonoBehaviour {

    private string firstToTake = "";

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (firstToTake == "") {
                firstToTake = other.gameObject.GetComponent<PlayerStats>().GetPlayerName();
            }
        }
    }

    public string GetTaken() {
        return firstToTake;
    }

}
