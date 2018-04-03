using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class PointManager : MonoBehaviour {

	public int team1points = 0;
	public int team2points = 0;

//	public int pointsToWin = 100;

	public float secondsLeft = 300f;

	public GameObject boat1;
	public GameObject boat2;

	bool gameOver = false;

	ShipDestroy shipDestroy1;
	ShipDestroy shipDestroy2;

	bool shouldDisplayWinText = false;

	// Use this for initialization
	void Start () {
		team1points = 0;
		team2points = 0;

//		shipDestroy1 = boat1.GetComponent<ShipDestroy> ();
//		shipDestroy2 = boat2.GetComponent<ShipDestroy> ();

//		StartCoroutine (CheckIfGameOver ());
	}

	public float SecondsLeft() {
		return secondsLeft;
	}

	IEnumerator CheckIfGameOver() {
		while (true) {
			if (!gameOver) {
				if (secondsLeft <= 0 || BoatDied (1) || BoatDied (2)) {
					gameOver = true;

				}
			} else {
				if (!BoatDied (1) && !BoatDied (2)) {
					shouldDisplayWinText = true;
					PrepareRestart ();
				} else {
					if (shipDestroy1.FinishedDeath () || shipDestroy2.FinishedDeath ()) {
						shouldDisplayWinText = true;
						PrepareRestart ();
					}
				}
			}
			yield return null;
		}
	}

	void PrepareRestart() {
		if (InputManager.ActiveDevice.Action2.WasPressed) {
			SceneManager.LoadScene ("Main");
		}
	}

	public int GetWinningTeamNumber() {
		if (!BoatDied (1) && !BoatDied (2)) {
			if (team1points == team2points) {
				return 0;
			}
			if (team1points > team2points) {
				return 1;
			} else {
				return 2;
			}
		} else {
			if (BoatDied (1)) {
				return 2;
			} else {
				return 1;
			}
		}
	}

	bool BoatDied(int boatNum) {
		if (boatNum == 1) {
			return boat1.GetComponent<BoatHealth> ().IsDead ();
		} else {
			return boat2.GetComponent<BoatHealth> ().IsDead ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		secondsLeft -= Time.deltaTime;
	}

	public bool ShouldDisplayWinText() {
		return shouldDisplayWinText;
	}

	public void AddPoints(int teamNumber, int points) {
		if (teamNumber == 1) {
			team1points += points;
		} else {
			team2points += points;
		}
	}

	public int GetPointsOfTeam(int teamNumber) {
		if (teamNumber == 1) {
			return team1points;
		} else {
			return team2points;
		}
	}

	public bool GameOver() {
		return gameOver;
	}
}
