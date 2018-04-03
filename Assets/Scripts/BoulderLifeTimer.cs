using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderLifeTimer : MonoBehaviour {

	public float secondsAlive;
	public GameObject explosionPrefab;

	MeshRenderer mesh;
	BoxCollider col;

	public List<AudioClip> explosionSFX = new List<AudioClip> ();

	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = this.GetComponent<AudioSource> ();
		mesh = this.GetComponent<MeshRenderer> ();
		col = this.GetComponent<BoxCollider> ();
		Destroy (this.gameObject, secondsAlive);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other) {
		mesh.enabled = false;
		col.enabled = false;
		GameObject explosion = Instantiate (explosionPrefab, this.transform.position, this.transform.rotation);
		explosion.transform.localScale = new Vector3 (10f, 10f, 10f);

		if (other.gameObject.CompareTag ("Boat1") || other.gameObject.CompareTag ("Boat2")) {
			var clip = explosionSFX [Random.Range (0, explosionSFX.Count)];
			audio.PlayOneShot (clip);
			mesh.enabled = false;
			StartCoroutine (DestroyAfterSound ());
		} else {
			Destroy (this.gameObject);
		}
	}

	IEnumerator DestroyAfterSound() {
		while (true) {
			if (!audio.isPlaying) {
				Destroy (this.gameObject);
			}
			yield return null;
		}
	}
}
