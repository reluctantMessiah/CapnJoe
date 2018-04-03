using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobShadow : MonoBehaviour {

    public LayerMask shadowLayerMask;

    public float rayDistance;

    private MeshRenderer shadow;
    private Color a = new Color(1f, 1f, 1f, 1f);
    private Color b = new Color(1f, 1f, 1f, 0f);
    private Vector3 shadowOffset = new Vector3(0f, 1f, 0f);

	// Use this for initialization
	void Start () {
        shadow = GetComponentInChildren<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, rayDistance, shadowLayerMask) && hitInfo.collider.tag != "Player") {
            shadow.enabled = true;
            shadow.transform.position = hitInfo.point + shadowOffset;
            float p = hitInfo.distance / rayDistance;
            shadow.material.SetColor("_TintColor", Color.Lerp(a, b, p));
        }
        else {
            shadow.enabled = false;
        }
    }

}
