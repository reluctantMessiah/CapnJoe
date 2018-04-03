using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;
using UnityEngine.SceneManagement;
public class Scoreboard : MonoBehaviour {

	ScoreTracker scoreTracker;

	enum Category 
	{
		gemsDelivered, rocksDodged, kills
	}

	Dictionary<Category, string> categoryToString = new Dictionary<Category, string>();
	Dictionary<string, Texture2D> joeNameToTexture2D = new Dictionary<string, Texture2D>();
	Dictionary<string, Sprite> joeNameToSprite = new Dictionary<string, Sprite>();
	Dictionary<string, Color> joeBodyColorToTeamColor = new Dictionary<string, Color> ();
	int numPlayers;

	public Color redTeamColor;
	public Color blueTeamColor;

	Text categoryTitle;
	Text continueText;
	Text promptText;
	Text playAgainText;

	List<Category> orderOfCategories = new List<Category>();

	GameObject inControl;

	AudioSource audio;
	public AudioClip titleSFX;
	public AudioClip gunshotSFX;

	public bool allowSkip = true;

	public Texture2D blueJoeSprite;
	public Texture2D greenJoeSprite;
	public Texture2D orangeJoeSprite;
	public Texture2D pinkJoeSprite;
	public List<GameObject> scorePanels = new List<GameObject> ();
	public GameObject scorePanelPrefab;
	public string nextScene = "CharacterSelectionScene";

	void SetCategoryTitles() {
		categoryToString [Category.gemsDelivered] = "Gems Delivered";
		categoryToString [Category.rocksDodged] = "Rocks Dodged";
		categoryToString [Category.kills] = "Kills";
	}

	void SetSpriteMap() {
		joeNameToTexture2D ["BlueJoe"] = blueJoeSprite;
		joeNameToTexture2D ["GreenJoe"] = greenJoeSprite;
		joeNameToTexture2D ["OrangeJoe"] = orangeJoeSprite;
		joeNameToTexture2D ["PinkJoe"] = pinkJoeSprite;

		foreach (var item in joeNameToTexture2D) {
			joeNameToSprite [item.Key] = Texture2DtoSprite (item.Key);
		}
	}

	void SetTeamColorMap() {
		joeBodyColorToTeamColor ["Blue"] = redTeamColor;
		joeBodyColorToTeamColor ["Orange"] = redTeamColor;
		joeBodyColorToTeamColor ["Green"] = blueTeamColor;
		joeBodyColorToTeamColor ["Pink"] = blueTeamColor;
	}

	void SetOrderOfCategories() {
		orderOfCategories.Add (Category.gemsDelivered);
		orderOfCategories.Add (Category.rocksDodged);
		orderOfCategories.Add (Category.kills);
	}

	// Use this for initialization
	void Start () {
		scoreTracker = GameObject.Find ("ScoreTracker").GetComponent<ScoreTracker> ();
		if (scoreTracker == null)
			return;

		inControl = GameObject.Find ("InControl");

		// Set titles for categories
		SetCategoryTitles ();
		categoryTitle = this.transform.Find ("Category").GetComponent<Text> ();
		categoryTitle.enabled = false;

		continueText = this.transform.Find ("Continue Text").GetComponent<Text> ();
		continueText.enabled = false;

		playAgainText = this.transform.Find ("Play Again Text").GetComponent<Text> ();
		playAgainText.enabled = false;

		promptText = this.transform.Find ("Prompt Text").GetComponent<Text> ();
		promptText.enabled = false;

		// Set the number of players based on the score tracker
		numPlayers = scoreTracker.GetNumPlayers();

		// Turn off all score panels
		foreach (var panel in scorePanels) {
			panel.SetActive (false);
		}

		audio = this.GetComponent<AudioSource> ();

		SetSpriteMap ();

		SetTeamColorMap ();

		SetOrderOfCategories ();
        print(orderOfCategories.Count);
		StartCoroutine (PresentScores ());

		if (allowSkip)
			StartCoroutine (WaitForSkip ());
	}

	void PresentTitle(Category category) {
		audio.PlayOneShot (titleSFX);
		UpdateCategoryTitle (category);
		categoryTitle.enabled = true;
	}

	IEnumerator AnimateTitle() {
		audio.PlayOneShot (titleSFX);
		yield return new WaitForSeconds (1f);
		categoryTitle.enabled = true;
	}

	IEnumerator AnimateScorePanels() {
		for (int i = 0; i < numPlayers && i < scorePanels.Count; i++) {
			audio.PlayOneShot (gunshotSFX);
			scorePanels[i].active = true;
			yield return new WaitForSeconds(.5f);
		}
	}

	void LoadNextScene() {
		//Destroy (inControl);
		SceneManager.LoadScene (nextScene);
	}

	IEnumerator WaitForSkip() {
		while (true) {
			while (true) {
				if (InputManager.ActiveDevice.GetControl(InputControlType.Start)) {
					LoadNextScene ();
					yield break;
				}
				yield return null;
			}
		}
			
	}

	IEnumerator WaitForContinue() {
		continueText.enabled = true;
		this.transform.Find ("Continue Text").GetComponent<TextFade> ().Activate (continueText.color);
		while (true) {
			if (InputManager.ActiveDevice.Action4.WasPressed) {
				categoryTitle.enabled = false;
				foreach (var panel in scorePanels) {
					panel.SetActive (false);
				}
				this.transform.Find ("Continue Text").GetComponent<TextFade> ().Deactivate ();
				continueText.enabled = false;
				yield break;
			}
			yield return null;
		}
	}

	IEnumerator WaitForRestart() {
		playAgainText.enabled = true;
		promptText.enabled = true;
		this.transform.Find ("Play Again Text").GetComponent<TextFade> ().Activate (playAgainText.color);

		while (true) {
			if (InputManager.ActiveDevice.GetControl(InputControlType.Start)) {
				this.transform.Find ("Play Again Text").GetComponent<TextFade> ().Deactivate ();
				playAgainText.enabled = false;
				LoadNextScene ();
				yield break;
			}
			yield return null;
		}
	}

	IEnumerator PresentScores() {
		int i = 0;
        print("i=" + i.ToString());
		while (i < orderOfCategories.Count) {
            foreach (var item in orderOfCategories)
            {
                print(item);
            }
            print("i=" + i.ToString());
			Category category = orderOfCategories [i];

			UpdateCategoryTitle (category);
			yield return AnimateTitle ();
			yield return new WaitForSeconds (1.5f);

			PrepareScorePanels (category);
			yield return AnimateScorePanels ();

			yield return WaitForContinue ();

			yield return new WaitForSeconds (.5f);
			i += 1;
			yield return null;
		}

		yield return WaitForRestart ();
	}

	string GetJoeBodyColor(string joeName) {

		string color = joeName.Substring(0, joeName.IndexOf("J"));

		return color;
	}

	Color GetJoeTeamColor(string joeName) {
		return joeBodyColorToTeamColor [GetJoeBodyColor(joeName)];
	}

	Sprite Texture2DtoSprite(string name) {
		return Sprite.Create(joeNameToTexture2D[name], 
			new Rect(0, 0, joeNameToTexture2D[name].width, joeNameToTexture2D[name].height),
			new Vector2(0.5f, 0.5f));
	}

	void UpdateCategoryTitle(Category category) {
		categoryTitle.text = categoryToString [category];
	}

	string GetNormalizedJoeName(string joeName) {
		return joeName.Substring (0, joeName.IndexOf ("_"));
	}

	List<KeyValuePair<string, int>> GetScores(Category category) {
		List<KeyValuePair<string, int>> scores = new List<KeyValuePair<string, int>> ();
		switch (category) {
		case Category.gemsDelivered:
			scores = scoreTracker.GetSortedGemDeliveries ();
			break;
		case Category.rocksDodged:
			scores = scoreTracker.GetSortedRockDodges ();
			break;
		case Category.kills:
			scores = scoreTracker.GetSortedKills ();
			break;
		default:
			break;
		}
		return scores;
	}

	void PrepareScorePanels(Category category) {
		List<KeyValuePair<string, int>> scores = GetScores (category);

		for (int i = 0; i < numPlayers && i < scorePanels.Count; i++) {
			int score = scores [i].Value;

			string joeName = GetNormalizedJoeName(scores [i].Key);

			// Set score text
			scorePanels [i].transform.Find ("Text").GetComponent<Text> ().text = score.ToString();

			// Set sprite
			scorePanels [i].transform.Find ("Icon").GetComponent<Image> ().sprite = 
				joeNameToSprite[joeName];

			// Set team color
			Color teamColor = GetJoeTeamColor (joeName);
			scorePanels [i].GetComponent<Image> ().color = teamColor;
			if (teamColor == Color.red) {
				// FIXME: flip red team icons
			}

//			scorePanels[i].active = true;
		}

		StartCoroutine(DisplayScoreCategory(Category.gemsDelivered));
	}

	IEnumerator DisplayScoreCategory(Category category) {

		while (true) {

			yield return null;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
