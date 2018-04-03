using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    public float swingSpeed;

    private GameObject myPlayer;

	// Use this for initialization
	void Start () {
        StartCoroutine(Swing());
        myPlayer = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (myPlayer.GetComponent<PlayerStats>().GetHealth() <= 0 || myPlayer.transform.position.y < -10f) {
            Destroy(gameObject);
        }
	}

    IEnumerator Swing() {
        float p = 0f; 
        float fullSwingX = transform.rotation.eulerAngles.x + 90f;

        while (p < 1f) {
            Quaternion fullSwing = Quaternion.Euler(fullSwingX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            transform.rotation = Quaternion.Lerp(transform.rotation, fullSwing, p); 
            p += Time.fixedDeltaTime * swingSpeed; 

            yield return null;
        }

        transform.rotation = Quaternion.Euler(fullSwingX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    public Vector3 GetPlayerForward() {
        return myPlayer.transform.forward;
    }

}
