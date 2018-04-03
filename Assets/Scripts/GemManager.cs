using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class GemManager : MonoBehaviour {

	public int totalGemCount = 8;
	public GameObject team1chest;
	public GameObject team2chest;
	ChestTrigger chest1Manager;
	ChestTrigger chest2Manager;
	bool gameOver = false;
	int winningTeam = 0;
    float timer = 4f;

    public GameObject ship1;
    public GameObject ship2;
    bool startTime = false;
    bool startCor = false;
    bool isRunning = false;

	// Use this for initialization
	void Awake () {
		chest1Manager = team1chest.GetComponent<ChestTrigger>();
		chest2Manager = team2chest.GetComponent<ChestTrigger>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameOver) {
			if (chest1Manager.NumGemsInChest() == totalGemCount) {
                //StartCoroutine(WaitForRestart());
                //ship1.GetComponent<Animation>().Play("WinningShip");
                //ship2.GetComponent<Animation>().Play("LosingShip");
                winningTeam = 1;
                gameOver = true;
            } else if (chest2Manager.NumGemsInChest() == totalGemCount) {
                //StartCoroutine(WaitForRestart());
                //ship1.GetComponent<Animation>().Play("LosingShip");
                //ship2.GetComponent<Animation>().Play("WinningShip");
                winningTeam = 2;
                gameOver = true;
            }
		}
        else if (gameOver)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                if (!isRunning)
                {
                    StartCoroutine(WaitForRestart());
                }
            }
        }
	}
	
	public int GetWinningTeamNum() {
		return winningTeam;
	}

    public string GetWinningTeamName() {
        if (winningTeam == 1) {
            return "Blue";
        }
        else {
            return "Red";
        }
    }

    public int GetGemCountOfTeam(int teamNumber) {
		if (teamNumber == 1) {
			return chest1Manager.NumGemsInChest();
		} else {
			return chest2Manager.NumGemsInChest();
		}
	}
	
	public bool GameOver() {
		return gameOver;
	}
	
	IEnumerator WaitForRestart() {
        isRunning = true;
		while (true) {
			//if (InputManager.ActiveDevice.Action2.WasPressed) {
                //SceneManager.LoadScene ("Main");
                if (winningTeam == 1)
                {
                     GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<SceneTransition>().QueueSceneTransition("Blue_Team_Win");
                }
                else if(winningTeam == 2)
                {
                    GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<SceneTransition>().QueueSceneTransition("Red_Team_Win");
                }
                //GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<SceneTransition>().QueueSceneTransition("Scoreboard");
            //}
			yield return null;
		}
	}
	
//	public void RespawnGemInChest(int team) {
//		if (team == 1) {
//			chest2Manager.RespawnOneGem();
//		} else {
//			chest1Manager.RespawnOneGem();
//		}
//	}
}
