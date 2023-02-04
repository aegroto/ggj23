using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetTouchGround : MonoBehaviour
{
    private PlayerInput context;
    private Animator animator;
    private void Start()
    {
        context = GetComponentInParent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetTrigger("TouchedGround");
            context.SetPlayerState("GROUNDED");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exiting collision with " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Ground")
        {
            context.SetPlayerState("JUMP");
        }
    }
}
