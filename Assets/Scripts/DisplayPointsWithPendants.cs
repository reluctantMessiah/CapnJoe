using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPointsWithPendants : MonoBehaviour {

    public int teamNumber;

    private GemManager gemManager;
    private List<GameObject> pendants = new List<GameObject>();

    // Use this for initialization
    void Start() {
        gemManager = GameObject.Find("Gem Manager").GetComponent<GemManager>();

        foreach (Transform child in transform) {
            pendants.Add(child.gameObject);
        }

        //initialize gem counts
        int numGems = gemManager.GetGemCountOfTeam(teamNumber);

        int i = 0;
        for (; i < numGems; ++i) {
            pendants[i].GetComponent<Pendant>().Activate();
        }

        for (; i < pendants.Count; ++i) {
            pendants[i].GetComponent<Pendant>().Deactivate();
        }
    }

    // Update is called once per frame
    void Update() {
        int numGems = gemManager.GetGemCountOfTeam(teamNumber);

        int i = 0;
        for (; i < numGems; ++i) {
            pendants[i].GetComponent<Pendant>().Activate();
        }

        for (; i < pendants.Count; ++i) {
            pendants[i].GetComponent<Pendant>().Deactivate();
        }
    }

}
