using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetTouchGround : MonoBehaviour
{
    private PlayerInput context;
    private void Start()
    {
        context = GetComponentInParent<PlayerInput>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Feet touch ground");
            context.SetPlayerState("GROUNDED");
        }
    }
}
