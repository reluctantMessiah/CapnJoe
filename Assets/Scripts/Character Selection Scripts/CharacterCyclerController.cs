using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class CharacterCyclerController : MonoBehaviour {

	public PlayerInputDevice playerInputDevice;
	public Transform leftAnalogTrans;
	public float flickDist = 1f;

	InputDevice inputDevice;
	bool stickIsReset = true;

	void Start () {
		inputDevice = playerInputDevice.GetPlayerInputDevice();
	}

	void Update () {
		if (inputDevice.LeftStickX.HasChanged) {
			Vector3 dir;
			if (!stickIsReset) {
				if (inputDevice.LeftStickX.Value < 0.01f && inputDevice.LeftStickX.Value > -0.01f) {
					stickIsReset = true;
				}
			}
			else
			{
				if (inputDevice.LeftStickX.Value < 0f)
				{
					// Flick left
					dir = Vector3.left;
				}
				else 
				{
					// Flick right
					dir = Vector3.right;
				}
				leftAnalogTrans.Translate(dir * flickDist);
				StartCoroutine(FlickBackAfterDelay(-dir));
			}
		}
	}

	IEnumerator FlickBackAfterDelay(Vector3 dir) {
		yield return new WaitForSeconds(1f);
		leftAnalogTrans.Translate(dir * flickDist);
	}
}
