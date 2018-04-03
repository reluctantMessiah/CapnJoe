using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanAmbience : MonoBehaviour {

	AudioSource audio;

	public AudioClip oceanSFX;
	public AudioClip seagullSFX;
	public AudioClip music;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		audio.PlayOneShot (oceanSFX);
		audio.PlayOneShot (seagullSFX);
		audio.PlayOneShot (music);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
