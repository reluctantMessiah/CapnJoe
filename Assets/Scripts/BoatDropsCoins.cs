using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatDropsCoins : MonoBehaviour {

	public int gemsLostIfBoulder = 4;
	public int gemsLostIfBoat = 1;
	public int gemsLostIfCannonBall = 2;

	GameObject chest;
	ChestCoinsDrop dropper;


	string boat = "";
	// Use this for initialization
	void Start () {
		chest = this.transform.Find ("Treasure Chest").gameObject;
		dropper = chest.GetComponent<ChestCoinsDrop> ();

		char boatChar = this.name [this.name.Length - 1];
		boat += boatChar;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other) {
		int numGemsToLose = 0;
		if (other.gameObject.CompareTag ("Boulder")) {
			numGemsToLose = gemsLostIfBoulder;
		} else if (other.gameObject.CompareTag ("Boat1") || other.gameObject.CompareTag ("Boat2")) {
			numGemsToLose = gemsLostIfBoat;
		} else if (other.gameObject.CompareTag ("CannonBall")) {
			int boatNum = other.gameObject.GetComponent<CannonBallBoatSource> ().GetBoat ();
			print ("boat hit: " + boat);
			print ("from boat: " + boatNum.ToString ());
			if (boat != boatNum.ToString())
				numGemsToLose = gemsLostIfCannonBall;
		}

		dropper.DropGems (numGemsToLose);
	}
}
