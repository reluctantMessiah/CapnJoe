using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectorGroup : MonoBehaviour {

	public float controllerOffset = 0.5f;
	public float waitForSecondsTillTransition = 3f;

	public LoadNextScene loadNextScene;

	public GameObject[] characterPrefabs;
	public GameObject[] dummyCharacters;
	bool[] charSelected;
	public Transform[] characterInstantiationPosition;
	public Transform[] controllerShiftBarriersLeft;
	public Transform[] controllerShiftBarriersRight;

	public XboxControllerSelector[] controllerSelectors;

	void Start() {

		charSelected = new bool[4];
		for (int i = 0; i < 4; ++i) {
			charSelected[i] = false;
		}

		foreach (var controllerSelector in GetComponentsInChildren<XboxControllerSelector>()) {
			controllerSelectors[controllerSelector.GetDeviceID()] = controllerSelector;
		}
	}

	public void CheckAllReady() {

		foreach(var selector in controllerSelectors) {
			if (!selector.IsSelected()) {
				return; // Don't load next scene
			}
		}

		SetupCharacterSelectInfo();
		StartCoroutine(LoadNextScene());
	}

	public void DeactivateDummyChar(int selectorIndex) {
		dummyCharacters[selectorIndex].SetActive(false);
	}

	public void ReactivateDummyChar(int selectorIndex) {
		dummyCharacters[selectorIndex].SetActive(true);	
	}

	public void SelectCharAt(int selectorIndex) {
		charSelected[selectorIndex] = true;
	}

	public void DeselectCharAt(int selectorIndex) {
		charSelected[selectorIndex] = false;
	}

	public bool IsCharSelectedAt(int selectorIndex) {
		return charSelected[selectorIndex];
	}

	public Transform GetBarrierLeft(int selectorIndex) {
		return controllerShiftBarriersLeft[selectorIndex];
	}

	public Transform GetBarrierRight(int selectorIndex)
	{
		return controllerShiftBarriersRight[selectorIndex];	
	}

	public GameObject GetCharPlayable(int selectorIndex) {
		return characterPrefabs[selectorIndex];
	}

	public Transform GetPlayableInstantiationPosition(int selectorIndex) {
		return characterInstantiationPosition[selectorIndex];
	}

	public void OnAddController(int selectorIndex) {
		if (selectorIndex != 0) { 
			Vector3 newPos = controllerShiftBarriersRight[selectorIndex - 1].position;
			newPos.x += controllerOffset;
			controllerShiftBarriersRight[selectorIndex - 1].position = newPos;
		}
		if (selectorIndex != 3) { 
			Vector3 newPos = controllerShiftBarriersLeft[selectorIndex + 1].position;
			newPos.x += controllerOffset;
			controllerShiftBarriersLeft[selectorIndex + 1].position = newPos;
		}


	}
	public void OnRemoveController(int selectorIndex) { 
		if (selectorIndex != 0) { 
			Vector3 newPos = controllerShiftBarriersRight[selectorIndex - 1].position;
			newPos.x -= controllerOffset;
			controllerShiftBarriersRight[selectorIndex - 1].position = newPos;
		}
		if (selectorIndex != 3) { 
			Vector3 newPos = controllerShiftBarriersLeft[selectorIndex + 1].position;
			newPos.x -= controllerOffset;
			controllerShiftBarriersLeft[selectorIndex + 1].position = newPos;
		}
	}

	IEnumerator LoadNextScene() {

		yield return new WaitForSeconds(waitForSecondsTillTransition);

		loadNextScene.LoadScene();
	}

	void SetupCharacterSelectInfo() {

		CharacterSelectionInfo.characterPrefabSelctions = new GameObject[4];

		foreach (var selector in controllerSelectors) {
			CharacterSelectionInfo.characterPrefabSelctions[selector.GetDeviceID()] = characterPrefabs[selector.GetSelectorIndex()];
		}
	}
}
