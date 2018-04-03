using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckShot : MonoBehaviour {

    private string playerTeam;

    public void SetPlayerTeam(string playerTeam_in) {
        playerTeam = playerTeam_in;
    }

    public string GetPlayerTeam() {
        return playerTeam;
    }

}
