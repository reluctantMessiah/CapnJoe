using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderGenerator : MonoBehaviour {

    public BoulderWarning boulderWarning;

	public GameObject defaultBoulderPrefab;
	public List<GameObject> boulderPrefabs;

	Vector3 spawnValues;
	float minX;
	float maxX;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public float speed = 3f;

	// Use this for initialization
	void Start () {
		spawnValues = this.transform.position;
		minX = spawnValues.x - this.transform.lossyScale.x / 2f;
		maxX = spawnValues.x + this.transform.lossyScale.x / 2f;
		StartCoroutine (SpawnWaves ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);

        //warn here
        if (boulderWarning != null) {
            boulderWarning.Warn();
        }

		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (minX, maxX), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				int boulderIndex = Random.Range (0, boulderPrefabs.Count);
				GameObject boulder = Instantiate (boulderPrefabs[boulderIndex], spawnPosition, spawnRotation);
				Rigidbody rb = boulder.GetComponent<Rigidbody> ();
				rb.velocity = -this.transform.forward * speed;
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (Random.Range(waveWait, waveWait + 10f));

            //Warn here
            if (boulderWarning != null) {
                boulderWarning.Warn();
            }
        }
	}
}
