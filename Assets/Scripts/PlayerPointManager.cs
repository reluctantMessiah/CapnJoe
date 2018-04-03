using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPointManager : MonoBehaviour {

    public GameObject coinDropperPrefab;
    public int points;

    private int team;

	// Use this for initialization
	void Start () {	
		if (GetComponent<PlayerTeamInfo> ().GetTeam() == "a") {
			team = 1;
		} 
        else {
			team = 2;
		}
	}

	public void GivePlayerPoints(int points) {
		this.points += points;
	}

	public int GetPlayerPoints() {
		return points;
	}

	public void RemovePlayerPoints() {
		points = 0;
	}

	public void ResetPoints() {
		RemovePlayerPoints ();
	}

	public void DropGold(bool willDie = false) {
		//if (points <= 0)
		//	return;
		GameObject coinDropper = Instantiate(coinDropperPrefab);
		Vector3 position = this.transform.position;
		position.y += 5f;
		coinDropper.transform.position = position;
		CoinDropper coinDropperScript = coinDropper.GetComponent<CoinDropper> ();
		coinDropperScript.SetTeam(team);
		int numPointsToLose = 1;
		//if (willDie) {
		//	// drop all points
		//	numPointsToLose = this.points;
		//} else {
		//	numPointsToLose = Mathf.Min (1, this.points);
		//}
		coinDropperScript.SetNumberOfPointsToDrop (numPointsToLose);
		//points = Mathf.Max (0, points - numPointsToLose);
	}

    //moved to player inventory script
	//void OnTriggerEnter(Collider other) {
	//	if (other.CompareTag ("Gem")) {
 //           //players coin only hold 1 coin at a time
 //           if (points == 0 && other.gameObject.GetComponent<GemTaken>().GetTaken() == GetComponent<PlayerStats>().GetPlayerName()) {
 //               GivePlayerPoints(1);
 //               Destroy(other.transform.parent.gameObject);
 //           }
	//	}
	//}

}
