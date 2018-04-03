using UnityEngine;
using System.Collections;

public class FloaterNonKinematic : MonoBehaviour {
    public float waterLevel, floatHeight;
    public Vector3 buoyancyCentreOffset;
    public float bounceDamp;
    public float upliftMultiplier;

    private Rigidbody rb; 

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        Vector3 actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
        float forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);

        if (forceFactor > 0f) {
            Vector3 uplift = -Physics.gravity * (forceFactor - rb.velocity.y * bounceDamp);
            rb.AddForceAtPosition(uplift * upliftMultiplier, actionPoint);
        }
    }
}
