using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class PMarkerController : MonoBehaviour {

	public int deviceId = 0;
	public PMarkerPositionsController[] positionControllers;
	public PMarkerPositionsController middlePositionController;
	public ReadyController[] readyControllers;
	public Material readyMat;
	public Material regMat;

	bool inMiddle = true;
	bool isReady = false;
	int selectionPosition = -1;
	InputDevice inputDevice;
	bool flickReset = true;
	MeshRenderer meshRenderer;

	PMarkersGroupController pMarkersGroupController;

	void Start() { 
		PlayerInputDevice playerInputDevice = GetComponent<PlayerInputDevice>();
		playerInputDevice.SetPlayerID(deviceId);

		inputDevice = playerInputDevice.GetPlayerInputDevice();
		meshRenderer = GetComponent<MeshRenderer>();

		pMarkersGroupController = GetComponentInParent<PMarkersGroupController>();
	}

	
	// Update is called once per frame
	void Update () {

		if (!isReady && inputDevice.LeftStickX.HasChanged)
		{

			if (!flickReset)
			{
				if (inputDevice.LeftStickX < 0.001f && inputDevice.LeftStickX > -0.001f)
				{
					flickReset = true;
				}
			}
			else
			{
				flickReset = false;
				if (inputDevice.LeftStickX.Value < 0)
				{
					MoveLeft();
				}
				else
				{
					MoveRight();
				}
			}
		}
		if (AButtPressed()) {
			if (inMiddle)
				return;

            readyControllers[selectionPosition].ToggleReady();

            isReady = !isReady;

			if (isReady && positionControllers[selectionPosition].ReadyUpMarker(this.gameObject))
			{
				
			}
			else {
				isReady = false;
                readyControllers[selectionPosition].ToggleReady();
            }

			if (isReady)
			{

				meshRenderer.material = readyMat;

				if (pMarkersGroupController.IsEveryoneReady())
				{
					// Time to load next scene

					// Change later maybe
					pMarkersGroupController.LoadNextScene();
				}
			}
			else { 
				positionControllers[selectionPosition].UnreadyUpMarker(this.gameObject);

				meshRenderer.material = regMat;
			}
		}
	}

	public bool GetIsReady() {
		return isReady;
	}

	public int GetSelectionPos() {
		return selectionPosition;
	}

	bool AButtPressed() {
		return inputDevice.Action1.HasChanged && inputDevice.Action1.IsPressed;
	}

	void MoveLeft() {
		int lastSelectionPosition = selectionPosition;
		if (inMiddle)
		{
			inMiddle = false;
			selectionPosition = 1;
		}
		else {
			if (selectionPosition == 2)
			{
				inMiddle = true;
				selectionPosition = -1;
			}
			else { 
				selectionPosition = (selectionPosition - 1);
				if (selectionPosition < 0)
					selectionPosition = 3;
			}
		}
        UpdateMarkerPosition(lastSelectionPosition);
	}

	void MoveRight()
	{
		int lastSelectionPosition = selectionPosition;
		if (inMiddle)
		{
			inMiddle = false;
			selectionPosition = 2;
		}
		else
		{
			if (selectionPosition == 1)
			{
				inMiddle = true;
				selectionPosition = -1;
			}
			else {
				selectionPosition = (selectionPosition + 1) % 4;
			}
		}
		UpdateMarkerPosition(lastSelectionPosition);
	}

	void UpdateMarkerPosition(int lastSelectionPos) {

		if (inMiddle)
		{
			positionControllers[lastSelectionPos].RemoveMarker(this.gameObject);
			middlePositionController.AddPMarker(this.gameObject);
		}
		else {
			if (lastSelectionPos == -1)
			{
				middlePositionController.RemoveMarker(this.gameObject);
			}
			else { 
				positionControllers[lastSelectionPos].RemoveMarker(this.gameObject);
			}
			positionControllers[selectionPosition].AddPMarker(this.gameObject);
		}
	}
}
