using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationPanel : MonoBehaviour {

    public GameObject[] objects;

    public void Activate() {
        foreach (GameObject thing in objects) {
            thing.SetActive(true);
        }
    }

    public void Deactivate() {
        foreach (GameObject thing in objects) {
            Destroy(thing);
        }
    }

}
