using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    public GameObject[] players;
    public GameObject myBoat;
    public string teamToSet;
    public int devicesOffset;

    private List<GameObject> spawnPositions = new List<GameObject>();

	// Use this for initialization
	void Start () {
        foreach (Transform child in transform) {
            spawnPositions.Add(child.gameObject);
        }

        for (int i = 0; i < players.Length; ++i) {
            var spawn = Instantiate(players[i], spawnPositions[i].transform.position, Quaternion.identity);

            if (teamToSet == "b") {
                spawn.GetComponent<PlayerInputDevice>().SetPlayerID(i + devicesOffset);
            }
            else {
                spawn.GetComponent<PlayerInputDevice>().SetPlayerID(i);
            }

            spawn.GetComponent<PlayerTeamInfo>().SetTeam(teamToSet);
            spawn.GetComponent<PlayerDamageHandler>().SetRespawnPosition(spawnPositions[i]);

            spawn.SetActive(true);
        }
	}

}
