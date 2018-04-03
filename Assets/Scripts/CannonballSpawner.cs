using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballSpawner : MonoBehaviour {

    public GameObject cannonball;

    private bool spawningCannonballMany;

	// Use this for initialization
	void Start () {
        spawningCannonballMany = true;
        StartCoroutine(SpawnCannonballMany(4));
	}
	
	// Update is called once per frame
	void Update () {
        if (!spawningCannonballMany && GetNumberOfCannonballs() <= 0) {
            spawningCannonballMany = true;
            StartCoroutine(SpawnCannonballMany(Random.Range(1, 5)));
        }
    }

    public void SpawnCannonball() {
        Vector3 pos = new Vector3(transform.position.x + Random.Range(-7, 3), 4f, transform.position.z + Random.Range(-2, 3));
        Instantiate(cannonball, pos, Quaternion.identity);
    }

    IEnumerator SpawnCannonballMany(int numToSpawn) {
        int i = 0;

        while (i < numToSpawn) {
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-7, 3), 4f, transform.position.z + Random.Range(-2, 3));
            Instantiate(cannonball, pos, Quaternion.identity);
            ++i;
            yield return new WaitForSeconds(1f);
        }

        spawningCannonballMany = false;
    }

    int GetNumberOfCannonballs() {
        int count = 0;

        foreach (Transform child in transform.root) {
            if (child.gameObject.tag == "CannonballPickup") {
                ++count;
            }
        }

        return count;
    }

}
