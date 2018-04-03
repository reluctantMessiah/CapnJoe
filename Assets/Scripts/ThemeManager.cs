using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour {

    public AudioClip intro;
    public AudioSource theme;

	// Use this for initialization
	void Start () {
        AudioSource.PlayClipAtPoint(intro, Camera.main.gameObject.transform.position);
        Invoke("PlayTheme", intro.length);
	}
	
    void PlayTheme() {
        theme.Play();
    }

}
