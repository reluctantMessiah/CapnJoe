using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracksPlayers : MonoBehaviour {

    public float zoomMultiplier;

    private GameObject[] players = new GameObject[0];
    private bool calculateAxisOnce;
    private Vector3 axis;
    private Vector3 offset;

    // Use this for initialization
    void Start () {
        offset = transform.position;
        Debug.Log(players.Length);
	}
	
	// Update is called once per frame
	void Update () {
        if (players.Length == 0) {
            players = GameObject.FindGameObjectsWithTag("Player");
        }

        if (players.Length > 0) {
            Vector3 averagePositions = Vector3.zero;

            foreach (GameObject player in players) {
                averagePositions += player.transform.position;
            }

            averagePositions /= players.Length;

            if (!calculateAxisOnce) {
                axis = (averagePositions - transform.position).normalized;
                axis *= -1f;
                calculateAxisOnce = true;
            }
           
            Vector3 zoomDistance = axis * 1f;

            print(MaxDistance());
            print("Y: " + axis.y + " " + "Z: " + axis.z);

            transform.position = new Vector3(averagePositions.x, offset.y, offset.z);

            print(players.Length);
        }
	}

    float MaxDistance() {
        float min = Mathf.Infinity;
        float max = -Mathf.Infinity; 

        foreach(GameObject player in players) {
            if (player.transform.position.x < min) {
                min = player.transform.position.x;

            }
            
            if (player.transform.position.x > max) {
                max = player.transform.position.x;
            }
        }

        print("Max: "  + max);
        print("Min: " + min);

        return Mathf.Abs(max - min);
    }

}
