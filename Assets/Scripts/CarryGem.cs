using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryGem : MonoBehaviour {

	public GameObject gemPrefab;

	PlayerInventory inventory;
	bool gemIsInstantiated = false;
	GameObject gemInstance;
	// Use this for initialization
	void Start () {
		inventory = this.GetComponent<PlayerInventory> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (inventory.HasGem () && !gemIsInstantiated) {
			gemInstance = Instantiate (gemPrefab);
			Vector3 pos = this.transform.position;
			pos.y += 4f;
			gemInstance.transform.position = pos;
			gemInstance.GetComponent<Rigidbody> ().isKinematic = true;
			gemInstance.GetComponent<BoxCollider> ().enabled = false;
			gemInstance.transform.parent = this.transform;               
			gemIsInstantiated = true;
		} else if (!inventory.HasGem () && gemIsInstantiated) {
			Destroy (gemInstance);
			gemIsInstantiated = false;
		}
	}

}
