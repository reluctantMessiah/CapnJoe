using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWinText : MonoBehaviour {

    public GameObject textBox;

	GameObject pointManager;
	GameObject gemManager;
	Text text;

	// Use this for initialization
	void Start () {
		pointManager = GameObject.Find ("Point Manager");
		gemManager = GameObject.Find ("Gem Manager");
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
//		if (pointManager.GetComponent<PointManager>().GameOver () &&
//			pointManager.GetComponent<PointManager>().ShouldDisplayWinText()) {
//			int winningTeamNumber = pointManager.GetComponent<PointManager> ().GetWinningTeamNumber ();
//			string winText;
//			string suffix = "\nPress B to Play Again";
//			if (winningTeamNumber == 0) {
//				winText = "Draw!" + suffix;
//			} else {
//				winText = "Team ";
//				winText += winningTeamNumber.ToString ();
//				winText += " Wins!" + suffix;
//			}
//			text.text = winText;
//
//			Color color = text.color;
//			color.a = 255;
//			text.color = color;
//		}
//	}
		if (gemManager.GetComponent<GemManager>().GameOver ()) {
			string winText;
			//string suffix = "\nPress B to Play Again";

            string textColor;
            if (gemManager.GetComponent<GemManager>().GetWinningTeamName() == "Blue") {
                textColor = "<color=blue>";
            }
            else {
                textColor = "<color=red>";
            }

            textBox.SetActive(true);

			winText = textColor + gemManager.GetComponent<GemManager>().GetWinningTeamName();
            winText += "Crew</color> ";
            winText += " Wins!";
			text.text = winText;

			Color color = text.color;
			color.a = 255;
			text.color = color;
		}
	}

}
