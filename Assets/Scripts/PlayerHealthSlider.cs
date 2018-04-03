using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSlider : MonoBehaviour {

    public GameObject myPlayer;
    public Image fill;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Slider>().value = myPlayer.GetComponent<PlayerStats>().GetHealth();

        if (myPlayer.GetComponent<PlayerStats>().GetHealth() > 3) {
            fill.color = Color.green;
        }
        else if (myPlayer.GetComponent<PlayerStats>().GetHealth() <= 3 && myPlayer.GetComponent<PlayerStats>().GetHealth() > 1) {
            fill.color = Color.yellow;
        }
        else {
            fill.color = Color.red;
        }
    }

}
