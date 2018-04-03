using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ChestTrigger : MonoBehaviour {

	int teamNumber;

	GameObject button;
	InputDevice inputDevice;
	Animator animator;

	Coroutine closeChestAfterOpened;

	GameObject particles;
	GameObject items;

	enum ChestState
	{
		opening, opened, closed
	}

	ChestState state = ChestState.closed;

	public int gemsRemaining;

    bool onTrigger;
    GameObject playerOnTrigger;

	ScoreTracker scoreTracker;

	// Use this for initialization
	void Start () {
        GameObject scoreTrackerObj = GameObject.Find("ScoreTracker");
        if (scoreTrackerObj != null)
          scoreTracker = scoreTrackerObj.GetComponent<ScoreTracker> ();

		button = transform.Find ("Xbox360_b").gameObject;
		button.SetActive (false);
		animator = this.GetComponent<Animator> ();

		GameObject boat1 = GameObject.Find ("Boat 1");
		GameObject boat2 = GameObject.Find ("Boat 2");
		if (boat1 != null && boat2 == null) {
			teamNumber = 1;
		} else if (boat1 == null && boat2 != null) {
			teamNumber = 2;
		} else if (boat1 == null && boat2 == null) {
			teamNumber = 1;
		} else {
			float distToBoat1 = Vector3.Distance (this.transform.position, boat1.transform.position);
			float distToBoat2 = Vector3.Distance (this.transform.position, boat2.transform.position);
			teamNumber = distToBoat1 < distToBoat2 ? 1 : 2;
		}
			

		GameObject gemManager = GameObject.Find ("Gem Manager");
		if (gemManager == null) {
			gemsRemaining = 1;
		} else {
			gemsRemaining = gemManager.GetComponent<GemManager>().totalGemCount / 2;
		}

		particles = transform.Find ("Particles").gameObject;
		items = transform.Find ("Box").transform.Find("Items").gameObject;
		StartCoroutine (UpdateContents ());
	}

	IEnumerator UpdateContents() {
		while (true) {
			if (gemsRemaining > 0) {
				particles.SetActive (true);
				items.SetActive (true);
			} else {
				particles.SetActive (false);
				items.SetActive (false);
			}
			yield return null;
		}
	}

	// Update is called once per frame
	void Update () {
        if (onTrigger) {
            if (state == ChestState.closed) {
                button.SetActive(true);
                if (inputDevice.Action2.WasPressed) {
                    animator.SetTrigger("Open");
                    state = ChestState.opening;
                    Invoke("SetStateToOpened", 1.5f);
                    button.SetActive(false);
                }
            } 
            else if (state == ChestState.opened) {
                button.SetActive(true);
                if (inputDevice.Action2.WasPressed) {
                    // give gem
                    TransferGems(playerOnTrigger);
                    button.SetActive(false);
                }
            }
        }
	}

	void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            print(other.gameObject.GetComponent<PlayerTeamInfo>().GetTeam());
            print("chest team:" + teamNumber.ToString());
        }
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Player") {
			//set icons
			SetPlayerInputDevice (other.gameObject);

			if (closeChestAfterOpened != null)
				StopCoroutine (closeChestAfterOpened);

            playerOnTrigger = other.gameObject;
            onTrigger = true;
		}
	}

	public int NumGemsInChest() {
		return gemsRemaining;
	}

	public void RemoveGems(int n) {
		gemsRemaining = Mathf.Max(gemsRemaining - n, 0);
	}

	void TransferGems(GameObject player) {
		int playerTeamNumber = player.GetComponent<PlayerTeamInfo> ().GetTeam () == "a" ? 1 : 2;

        if (playerTeamNumber == teamNumber) {
            //if (player.GetComponent<PlayerPointManager> ().GetPlayerPoints() > 0) {
            //	gemsRemaining += player.GetComponent<PlayerPointManager> ().GetPlayerPoints();
            //	player.GetComponent<PlayerPointManager> ().RemovePlayerPoints ();
            //}

            if (player.GetComponent<PlayerInventory>().HasGem()) {
                gemsRemaining += 1;
                player.GetComponent<PlayerInventory>().SetGem(false);

				if (scoreTracker != null) {
					scoreTracker.RecordGemDelivery(player);
				}
            }
        } 
        else {
            //// FIXME: give points to player inventory, i.e., player is at enemy chest
            //if (player.GetComponent<PlayerPointManager>().GetPlayerPoints() == 0) {
            //	if (gemsRemaining > 0) {
            //		gemsRemaining -= 1;
            //		player.GetComponent<PlayerPointManager>().GivePlayerPoints(1);
            //	}
            //}

            if (!player.GetComponent<PlayerInventory>().HasGem()) {
                if (gemsRemaining > 0) {
                    gemsRemaining -= 1;
                    player.GetComponent<PlayerInventory>().SetGem(true);
                }
            }
        }
	}

	void SetStateToOpened() {
		state = ChestState.opened;
	}

	void SetPlayerInputDevice(GameObject player) {
		inputDevice = player.GetComponent<PlayerInputDevice>().GetPlayerInputDevice();
	}

	IEnumerator CloseChestAfterOpened() {
		while (true) {
			print ("running");
			if (state == ChestState.opened) {
				animator.SetTrigger ("Close");
				state = ChestState.closed;
				closeChestAfterOpened = null;
				break;
			}
			yield return null;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			//set icons

			inputDevice = null;
			if (state == ChestState.opened) {
				animator.SetTrigger ("Close");
				state = ChestState.closed;
			} else if (state == ChestState.opening) {
				closeChestAfterOpened = StartCoroutine (CloseChestAfterOpened ());
			}
			button.SetActive (false);

            onTrigger = false;
            playerOnTrigger = null;
        }
	}
}
