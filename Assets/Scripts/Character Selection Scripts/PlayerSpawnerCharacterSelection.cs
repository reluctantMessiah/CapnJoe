using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerCharacterSelection : MonoBehaviour {

    public string teamToSet;
    public int totalNumberOfPlayers;

    private List<GameObject> spawnPositions = new List<GameObject>();

    // Use this for initialization
    void Start() {
        for (int i = 0; i < CharacterSelectionInfo.characterPrefabSelctions.Length; ++i) {
            Debug.Log("This: " + CharacterSelectionInfo.characterPrefabSelctions[i].name);
        }

        foreach (Transform child in transform) {
            spawnPositions.Add(child.gameObject);
        }

        int spawnPositions_index = 0;
        for (int i = 0; i < CharacterSelectionInfo.characterPrefabSelctions.Length; ++i) {
            GameObject spawn = null;
            
            if (teamToSet == "a") {
                if (CharacterSelectionInfo.characterPrefabSelctions[i].name == "GreenJoe_blueTeam" ||
                    CharacterSelectionInfo.characterPrefabSelctions[i].name == "PinkJoe_blueTeam") {
                    spawn = Instantiate(CharacterSelectionInfo.characterPrefabSelctions[i], spawnPositions[spawnPositions_index].transform.position, Quaternion.identity);
                    spawn.GetComponent<PlayerInputDevice>().SetPlayerID(i);
                    spawn.GetComponent<PlayerTeamInfo>().SetTeam(teamToSet);
                    spawn.GetComponent<PlayerDamageHandler>().SetRespawnPosition(spawnPositions[spawnPositions_index]);
                    spawn.SetActive(true);
                    ++spawnPositions_index;
                }
            }
            else {
                if (CharacterSelectionInfo.characterPrefabSelctions[i].name == "BlueJoe_redTeam" ||
                    CharacterSelectionInfo.characterPrefabSelctions[i].name == "OrangeJoe_redTeam") {
                    spawn = Instantiate(CharacterSelectionInfo.characterPrefabSelctions[i], spawnPositions[spawnPositions_index].transform.position, Quaternion.identity);
                    spawn.GetComponent<PlayerInputDevice>().SetPlayerID(i);
                    spawn.GetComponent<PlayerTeamInfo>().SetTeam(teamToSet);
                    spawn.GetComponent<PlayerDamageHandler>().SetRespawnPosition(spawnPositions[spawnPositions_index]);
                    spawn.SetActive(true);
                    ++spawnPositions_index;
                }
            }
        }
    }

}
