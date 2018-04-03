using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleSystem : MonoBehaviour {

	ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ps = this.GetComponent<ParticleSystem> ();
		Destroy (this.gameObject, ps.main.duration);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
