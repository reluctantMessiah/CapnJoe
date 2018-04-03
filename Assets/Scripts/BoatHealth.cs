using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatHealth : MonoBehaviour {

	public int health;
	public int damageFromBoulder;

	bool dead = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			dead = true;
		}
	}

	public bool IsDead() {
		return dead;
	}

	public void Kill() {
		health = 0;
	}

	public void OnCollisionEnter(Collision other) {
		if (other.transform.CompareTag ("Boulder") || other.transform.CompareTag("CannonBall")) {
            print(other.gameObject.tag);
			if (health - damageFromBoulder <= 0) {
				dead = true;
			} else {
				health -= damageFromBoulder;
			}
		}
	}

	public int GetHealth() {
		return health;
	}
}
