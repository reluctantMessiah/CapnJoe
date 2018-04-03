using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatHealthSlider : MonoBehaviour {

	public GameObject boat;

	Slider slider;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
		slider.minValue = 0;
		slider.maxValue = boat.GetComponent<BoatHealth> ().GetHealth();
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = boat.GetComponent<BoatHealth> ().GetHealth();
	}
}
