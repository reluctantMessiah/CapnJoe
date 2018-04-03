using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour {

	public int teamNumber;
	int pointsPerGrab = 1;
	PointManager pointManager;
	GemManager gemManager;

	int gemsRemaining;

	// Use this for initialization
	void Start () {
		pointManager = GameObject.Find ("Point Manager").GetComponent<PointManager> ();
		gemManager = GameObject.Find ("Gem Manager").GetComponent<GemManager> ();
		
		gemsRemaining = gemManager.totalGemCount / 2;
	}
	
	public int NumGemsInChest() {
		return gemsRemaining;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void RemoveGems(int n) {
		gemsRemaining = Mathf.Max(gemsRemaining - n, 0);
	}
	
	public void RespawnOneGem() {
		gemsRemaining += 1;
	}

	void OnTriggerEnter(Collider other) {
//		print (other.tag);
		if (other.CompareTag ("Player")) {
			PlayerTeamInfo teamInfo = other.gameObject.GetComponent<PlayerTeamInfo> ();
			int playerTeamNumber;
			if (teamInfo.GetTeam () == "a") {
				playerTeamNumber = 1;
			} else {
				playerTeamNumber = 2;
			}
			if (playerTeamNumber == teamNumber) {
                // give points to team
                // FIXME: add points that the player holds


                //if (other.gameObject.GetComponent<PlayerPointManager> ().GetPlayerPoints() > 0) {
                //	gemsRemaining += other.gameObject.GetComponent<PlayerPointManager> ().GetPlayerPoints();
                //	other.gameObject.GetComponent<PlayerPointManager> ().RemovePlayerPoints ();
                //}

                if (other.gameObject.GetComponent<PlayerInventory>().HasGem()) {
                    gemsRemaining += 1;
                    other.gameObject.GetComponent<PlayerInventory>().SetGem(false);
                }

            } else {
                // FIXME: give points to player inventory, i.e., player is at enemy chest
                //if (other.gameObject.GetComponent<PlayerPointManager>().GetPlayerPoints() == 0) {
                //    if (gemsRemaining > 0) {
                //        gemsRemaining -= 1;
                //        other.gameObject.GetComponent<PlayerPointManager>().GivePlayerPoints(pointsPerGrab);
                //    }
                //}

                if (!other.gameObject.GetComponent<PlayerInventory>().HasGem()) {
                    if (gemsRemaining > 0) {
                        gemsRemaining -= 1;
                        other.gameObject.GetComponent<PlayerInventory>().SetGem(true);
                    }
                }
            }
		}
	}

}
