using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PMarkersGroupController : MonoBehaviour {

	public GameObject[] pMarkers;
	public string nextSceneToLoad;

	PMarkerController[] pMarkerControllers;
	public GameObject[] characterPrefabsInOrder;

	// Use this for initialization
	void Start () {
		int index = 0;
		pMarkerControllers = new PMarkerController[pMarkers.Length];
		foreach (var pMarker in pMarkers) {
			pMarker.GetComponent<PlayerInputDevice>().SetPlayerID(index);
			pMarker.SetActive(true);

			pMarkerControllers[index] = pMarker.GetComponent<PMarkerController>();
			++index;
		}
	}

	public bool IsEveryoneReady() {

		foreach (var pMarkerController in pMarkerControllers) {
			if (!pMarkerController.GetIsReady())
				return false;
		}

		return true;
	}

	public void LoadNextScene() {

		GameObject[] selectedPrefabs = new GameObject[pMarkers.Length];
		for (int i = 0; i < selectedPrefabs.Length; ++i) {
			selectedPrefabs[i] = characterPrefabsInOrder[pMarkerControllers[i].GetSelectionPos()];
		}

		// We can access this static array of gameobject prefabs to load in the correct 
		//   player prefab by loading "CharacterSelectionInfo.characterPrefabSelctions[playerId]"
		CharacterSelectionInfo.characterPrefabSelctions = selectedPrefabs;

        //SceneManager.LoadScene(nextSceneToLoad);
        GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<SceneTransition>().QueueSceneTransition(nextSceneToLoad);
    }
}
