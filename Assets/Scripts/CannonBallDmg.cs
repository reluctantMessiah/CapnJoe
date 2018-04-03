using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallDmg : MonoBehaviour {

	public GameObject explosionPrefab;

	MeshRenderer mesh;
	SphereCollider col;

	// Use this for initialization
	void Start () {
		mesh = this.GetComponent<MeshRenderer> ();
		col = this.GetComponent<SphereCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(true)
        {
			mesh.enabled = false;
			col.enabled = false;
			GameObject explosion = Instantiate (explosionPrefab, this.transform.position, this.transform.rotation);
			explosion.transform.localScale = new Vector3 (4f, 4f, 4f);
            Destroy(this.gameObject);
        }
    }
}