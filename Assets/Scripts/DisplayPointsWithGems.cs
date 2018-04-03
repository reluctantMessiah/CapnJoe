using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPointsWithGems : MonoBehaviour {

    public GameObject gemDisappearSmoke;
    public GameObject gemAppearSparkle;
    public int teamNumber;

    private PointManager pointManager;
    private GemManager gemManager;
    private List<GameObject> gems = new List<GameObject>();
    private GameObject chestSparkle;

    // Use this for initialization
    void Start() {
        pointManager = GameObject.Find("Point Manager").GetComponent<PointManager>();
        gemManager = GameObject.Find("Gem Manager").GetComponent<GemManager>();

        foreach (Transform child in transform) {
            if (child.gameObject.name =="ChestSparkle") {
                chestSparkle = child.gameObject;
            }

            if (child.gameObject.name == "GemImage") {
                gems.Add(child.gameObject);
            }
        }

        //initialize gem counts
        int numGems = gemManager.GetGemCountOfTeam(teamNumber);

        int i = 0;
        for ( ; i < numGems; ++i) {
            gems[i].SetActive(true);
        }

        for ( ; i < gems.Count; ++i) {
            gems[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        int numGems = gemManager.GetGemCountOfTeam(teamNumber);

        int i = 0;
        for ( ; i < numGems; ++i) {
            if (!gems[i].activeSelf) {
                Instantiate(gemAppearSparkle, gems[i].transform.position, Quaternion.identity);
            }

            gems[i].SetActive(true);
        }

        for ( ; i < gems.Count; ++i) {
            if (gems[i].activeSelf) {
                Instantiate(gemDisappearSmoke, gems[i].transform.position, Quaternion.identity);
            }

            gems[i].SetActive(false);
        }

        if (numGems >= 6) {
            chestSparkle.transform.localScale = new Vector3(8f, 8f, 8f);
        }
        else {
            chestSparkle.transform.localScale = new Vector3(5f, 5f, 5f);
        }

        if (numGems == 0) {
            chestSparkle.SetActive(false);
        }
        else {
            chestSparkle.SetActive(true);
        }
    }

}
