using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeamInfo : MonoBehaviour {

    public string myTeam; 

	public void SetTeam(string team) {
        myTeam = team;
    }

    public string GetTeam() {
        return myTeam;
    }
}
