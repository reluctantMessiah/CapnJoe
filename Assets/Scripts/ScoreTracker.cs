using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class ScoreTracker : MonoBehaviour {

	public bool testMode = true;
	int numPlayers = 0;
	public bool skipToScoreboard = false;
	GameObject[] players;

	Dictionary<string, int> gemDeliveries = new Dictionary<string, int> ();
	Dictionary<string, int> rocksDodged = new Dictionary<string, int> ();
	Dictionary<string, int> kills = new Dictionary<string, int> ();

	HashSet<string> names = new HashSet<string> ();

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	IEnumerator SkipToScoreboard() {
		while (true) {
			if (testMode && skipToScoreboard) {
				skipToScoreboard = false;
				SceneManager.LoadScene ("Scoreboard");
			}
			yield return null;
		}
	}

	IEnumerator DestroyOnCharacterSelection() {
		while (true) {
			if (SceneManager.GetActiveScene().name == "CharacterSelectionScene") {
				Destroy (this.gameObject);
			}
			yield return null;
		}
	}

	public int GetNumPlayers() {
		return numPlayers;
	}

	// Use this for initialization
	void Start () {

		if (GameObject.Find ("ScoreTracker") != null && GameObject.Find ("ScoreTracker") != this.gameObject)
			Destroy (this.gameObject);

		if (!testMode) {
			StartCoroutine (FindPlayers ());
		}
		else {
			names.Add ("BlueJoe_redTeam(clone)");
			names.Add ("GreenJoe_blueTeam(clone)");
			names.Add ("OrangeJoe_redTeam(clone)");
			names.Add ("PinkJoe_blueTeam(clone)");

			foreach (var name in names) {
				gemDeliveries[name] = Random.Range(0, 20);
				rocksDodged[name] = Random.Range(0, 40);
				kills[name] = Random.Range(0, 15);
			}
		}
		StartCoroutine (SkipToScoreboard ());
		StartCoroutine (DestroyOnCharacterSelection ());
	}

	IEnumerator FindPlayers() {
		while (numPlayers < 4) {
			players = GameObject.FindGameObjectsWithTag("Player");
			foreach (var player in players) {
				if (!names.Contains (player.name)) {
					numPlayers += 1;
					names.Add (player.name);
					gemDeliveries[player.name] = 0;
					rocksDodged[player.name] = 0;
					kills[player.name] = 0;
				}
			}
			yield return null;
		}
//		while (names.Count < numPlayers) {
//			players = GameObject.FindGameObjectsWithTag("Player");
//			foreach (var player in players) {
//				if (!names.Contains (player.name)) {
//					names.Add (player.name);
//					gemDeliveries[player.name] = 0;
//					rocksDodged[player.name] = 0;
//					kills[player.name] = 0;
//				}
//			}
//			yield return null;
//		}
	}

	public void RecordGemDelivery(GameObject player) {
		gemDeliveries [player.name] += 1;
	}

	public void RecordRockDodged(GameObject player) {
		rocksDodged [player.name] += 1;
	}

	public void RecordKill(GameObject player) {
		kills [player.name] += 1;
	}

	// Update is called once per frame
	void Update () {
		foreach (var name in names) {
//			print (name);
//			print ("Gems:" + gemDeliveries [name]);
//			print ("Kills:" + kills [name]);
//			print ("Rocks:" + rocksDodged [name]);
		}
	}

	List<KeyValuePair<string, int>> GetSorted(Dictionary<string, int> dict) {
		List<KeyValuePair<string, int>> list = dict.ToList();
		list.Sort(
			delegate(KeyValuePair<string, int> pair1,
				KeyValuePair<string, int> pair2)
			{
				return pair1.Value.CompareTo(pair2.Value);
			}
		);
		list.Reverse ();
		return list;
	}

	public List<KeyValuePair<string, int>> GetSortedGemDeliveries() {
		return GetSorted (gemDeliveries);
	}

	public List<KeyValuePair<string, int>> GetSortedRockDodges() {
		return GetSorted (rocksDodged);
	}

	public List<KeyValuePair<string, int>> GetSortedKills() {
		return GetSorted (kills);
	}
}
