using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class XboxControllerSelector : MonoBehaviour {

	public CharacterSelectorGroup charSelectorGroup;

	public Transform charPositionRight;
	public Transform charPositionLeft;

	public float leftStickLeeway = 0.05f;
	public float selectSpeed = 5f;
	public float shiftSpeed = 3f;
	public float fastShiftSpeed = 14f;
	public float yDownThreshold = 8f;

	GameObject playableChar;

	// Set initially
	float yUpThreshold;

	Vector3 initialPosition;

	public bool isOnLeftEdge = false;
	public bool isOnRightEdge = false;

	PlayerInputDevice playerInputDevice;
	InputDevice inputDevice;

	public int selectorIndex;
	bool isSelected = false;

	bool movingDown = false;
	bool movingUp = false;
	bool isShifting = false;
	bool isShiftingFast = false;
	bool isGoingRight = true;

	public bool IsSelected() {
		return isSelected;
	}

	public int GetDeviceID() {
		return playerInputDevice.devicesIndex;
	}

	public int GetSelectorIndex() {
		return selectorIndex;
	}

	void Select() {
		if (!movingUp && !movingDown && !charSelectorGroup.IsCharSelectedAt(selectorIndex))
		{
			initialPosition = this.transform.position;

			// Let other players immediately know this spot is taken
			charSelectorGroup.SelectCharAt(selectorIndex);

			movingDown = true;
		}
	}

	void Deselect() {
		if (isSelected) {
			charSelectorGroup.DeselectCharAt(selectorIndex);
			ResetDummyChar();
			isSelected = false;
		}
	}

	void ShiftToNextPlayer(bool isGoingRightIn) {
		if (isShifting)
			return;
		if (isOnLeftEdge && !isGoingRightIn)
			return;
		if (isOnRightEdge && isGoingRightIn)
			return;

		charSelectorGroup.OnRemoveController(selectorIndex);

		if (isGoingRightIn && selectorIndex == 1)
		{
			isShiftingFast = true;
		}
		else if (!isGoingRightIn && selectorIndex == 2) {
			isShiftingFast = true;
		}

		if (isGoingRightIn)
			++selectorIndex;
		else
			--selectorIndex;
			
		
		isGoingRight = isGoingRightIn;
		isShifting = true;


	}

	void Awake() {
		playerInputDevice = GetComponent<PlayerInputDevice>();
	}

	void Start() {
		yUpThreshold = this.transform.position.y;
		initialPosition = this.transform.position;
		inputDevice = playerInputDevice.GetPlayerInputDevice();
	}

	void Update() {

		HandleInput();

		if (movingDown)
		{
			if (this.transform.position.y < yDownThreshold)
			{
				movingDown = false;

				isSelected = true;

				InstantiatePlayableChar();

				charSelectorGroup.CheckAllReady();
			}
			else {
            	this.transform.Translate(Vector3.down * selectSpeed * Time.deltaTime);
			}
			return;
		}
		//if (movingUp) {
		//	if (this.transform.position.y > yUpThreshold)
		//	{
		//		movingUp = false;

		//		isSelected = false;

		//		ResetDummyChar();
		//	}
		//	else {
		//		this.transform.Translate(Vector3.up * selectSpeed * Time.deltaTime);
		//	}
		//	return;
		//}
		if (!isShifting)
			return;

		if (isGoingRight)
		{
			if (this.transform.position.x >= charPositionRight.position.x)
			{
				isShifting = false;
				isShiftingFast = false;
				charSelectorGroup.OnAddController(selectorIndex);
				SetNewShiftBarrriers();
			}
			else {

				if(isShiftingFast)
                    this.transform.Translate(Vector3.right * fastShiftSpeed * Time.deltaTime);
				else
					this.transform.Translate(Vector3.right * shiftSpeed * Time.deltaTime);
			}
		}
		else {
			if (this.transform.position.x <= charPositionLeft.position.x)
			{
				isShifting = false;
				isShiftingFast = false;
				charSelectorGroup.OnAddController(selectorIndex);
				SetNewShiftBarrriers();
			}
			else
			{
				if(isShiftingFast)
                    this.transform.Translate(Vector3.left * fastShiftSpeed * Time.deltaTime);
				else
					this.transform.Translate(Vector3.left * shiftSpeed * Time.deltaTime);
			}
		}
	}

	void HandleInput() { 
		// A button press
		if (inputDevice.Action2.WasPressed && isSelected) { // B
			Deselect();
		}

		if (isSelected)
			return;

		if (inputDevice.Action1.WasPressed) // A
		{
            Select();
		}

		if (inputDevice.LeftStick.X < -leftStickLeeway)
		{
				ShiftToNextPlayer(false);
		}
		else if(inputDevice.LeftStick.X > leftStickLeeway) {
				ShiftToNextPlayer(true);
		}
	}

	void SetNewShiftBarrriers() {
		if (selectorIndex != 0)
			isOnLeftEdge = false;
		else
			isOnLeftEdge = true;
		if (selectorIndex != 3)
			isOnRightEdge = false;
		else
			isOnRightEdge = true;

		charPositionRight = charSelectorGroup.GetBarrierRight(selectorIndex);
		charPositionLeft = charSelectorGroup.GetBarrierLeft(selectorIndex);
	}

	void InstantiatePlayableChar() {

		GameObject prefab = charSelectorGroup.GetCharPlayable(selectorIndex);
		Transform instantiationPos = charSelectorGroup.GetPlayableInstantiationPosition(selectorIndex);

		charSelectorGroup.DeactivateDummyChar(selectorIndex);



		playableChar = Instantiate(prefab, instantiationPos.position, Quaternion.LookRotation(Vector3.back));

		Vector3 newControllerPos = playableChar.transform.position;
		newControllerPos += playableChar.transform.forward * 1f;
		this.transform.position = newControllerPos;
		this.transform.parent = playableChar.transform;

		playableChar.GetComponent<PlayerInputDevice>().animate = true;
		playableChar.GetComponent<PlayerInputDevice>().devicesIndex = playerInputDevice.devicesIndex;

		playableChar.SetActive(true);
		playableChar.GetComponent<PlayerStats>().enabled = false;
		playableChar.GetComponent<PlayerDamageHandler>().enabled = false;
		playableChar.GetComponent<PlayerInventory>().enabled = false;
		playableChar.GetComponent<PlayerAttack>().enabled = false;
	}

	void ResetDummyChar() {

		this.transform.parent = null;

		this.transform.position = initialPosition;

		Destroy(playableChar);
		charSelectorGroup.ReactivateDummyChar(selectorIndex);
	}
}
