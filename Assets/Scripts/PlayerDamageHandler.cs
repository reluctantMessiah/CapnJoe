using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageHandler : MonoBehaviour {

    public AudioClip[] pirateDeathSounds;

    public GameObject deathSmoke;
    public GameObject respawnSmoke;

    private Rigidbody rb;
    private PlayerStats playerStats;
	private PlayerPointManager pointManager;
    private GameObject respawnPosition;
	private GameObject gameState;
    private bool isDead;
	private ScoreTracker scoreTracker;
    private GameObject deathIcon;
    private Text respawnTimer;

	public GameObject attacker;
	public float timeOfAttack;

	// Use this for initialization
	void Start () {
        GameObject scoreTrackerObj = GameObject.Find("ScoreTracker");
        if (scoreTrackerObj != null)
          scoreTracker = scoreTrackerObj.GetComponent<ScoreTracker> ();

        rb = GetComponent<Rigidbody>();
        playerStats = GetComponent<PlayerStats>();
		pointManager = GetComponent<PlayerPointManager> ();
		gameState = GameObject.Find ("Point Manager");

        deathIcon = GameObject.Find(GetComponent<PlayerStats>().GetPlayerName() + "Status").transform.GetChild(0).gameObject;
        respawnTimer = GameObject.Find(GetComponent<PlayerStats>().GetPlayerName() + "Text").transform.GetChild(0).gameObject.GetComponent<Text>();

        deathIcon.SetActive(false);
        respawnTimer.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (playerStats.GetHealth() <= 0 || transform.position.y < -8f) {
			if (gameState == null || !gameState.GetComponent<PointManager>().GameOver()) {
                if (!isDead) {
                    isDead = true;

                    if (playerStats.GetHealth() <= 0) {
                        Instantiate(deathSmoke, transform.position + Vector3.up, Quaternion.identity);
                        scoreTracker.RecordKill(attacker);

                    }

                    if (GetComponent<PlayerSteeringMode>().GetSteeringMode()) {
                        GetComponent<PlayerMovement>().GiveUpShipOnHit(Vector3.zero);
                    }

                    if (GetComponent<PlayerInventory>().HasCannonBall()) {
                        GetComponent<PlayerInventory>().SetCannonBall(false);
                        Destroy(GetComponent<CarryCannonBall>().GetCannonBall().gameObject);

                        if (Random.Range(0, 2) == 0) {
                            GameObject.Find("CannonballSpawner_boat1").GetComponent<CannonballSpawner>().SpawnCannonball();
                        }
                        else {
                            GameObject.Find("CannonballSpawner_boat2").GetComponent<CannonballSpawner>().SpawnCannonball();
                        }
                    }

                    if (GetComponent<PlayerInventory>().HasGem()) {
                        pointManager.DropGold (true);
                        GetComponent<PlayerInventory>().SetGem(false);
                    }

                    StartCoroutine(RespawnOnDeath());
                }
			}
        }
	}

    public void SetRespawnPosition(GameObject respawnPosition_in) {
        respawnPosition = respawnPosition_in;
    }

    IEnumerator RespawnOnDeath() {
        AudioSource.PlayClipAtPoint(pirateDeathSounds[Random.Range(0, pirateDeathSounds.Length)], Camera.main.gameObject.transform.position);

        deathIcon.SetActive(true);
        respawnTimer.gameObject.SetActive(true);

        transform.position += Vector3.down * 1000f;

        StartCoroutine(RespawnTimerCountdown(3f));
        yield return new WaitForSeconds(3f);

        rb.velocity = Vector3.zero;
        transform.position = respawnPosition.transform.position;

        if (playerStats.GetHealth() <= 0) {
            playerStats.ResetHealth(5);
        }

        isDead = false;

        Instantiate(respawnSmoke, transform.position + Vector3.down + Vector3.back * 1.5f, Quaternion.identity);

        deathIcon.SetActive(false);
        respawnTimer.gameObject.SetActive(false);
    }

    IEnumerator RespawnTimerCountdown(float respawnTime) {
        respawnTimer.text = "";
        while (respawnTime > 0f) {
            respawnTime -= Time.deltaTime;
            respawnTimer.text = respawnTime.ToString("F0");
            yield return null;
        }
    }

}
