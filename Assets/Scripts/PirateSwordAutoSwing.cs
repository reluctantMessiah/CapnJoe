using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSwordAutoSwing : MonoBehaviour {

	float swingSpeed = 8f;

	void Start() {
		transform.rotation = Quaternion.Euler(-90f, 90f, 0);

		StartCoroutine(Swing());
	}

	IEnumerator Swing()
	{
		float p = 0f;
		float fullSwingX = transform.rotation.eulerAngles.x + 90f;

		while (p < 1f)
		{
			Quaternion fullSwing = Quaternion.Euler(fullSwingX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

			transform.rotation = Quaternion.Lerp(transform.rotation, fullSwing, p);
			p += Time.fixedDeltaTime * swingSpeed;

			yield return null;
		}

		transform.rotation = Quaternion.Euler(fullSwingX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); 
	}
}
