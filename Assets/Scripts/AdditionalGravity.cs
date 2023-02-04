using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalGravity : MonoBehaviour {
    [SerializeField]
    private float value;

    [SerializeField]
    private Rigidbody rigidBody;

    void FixedUpdate() {
        rigidBody.AddForce(new Vector3(0, -Mathf.Abs(rigidBody.velocity.y) * value, 0));
        Debug.Log(rigidBody.velocity);
    }
}
