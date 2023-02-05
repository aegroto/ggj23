using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpPlayerState : AbstractPlayerState
{
    public float JumpForce { get; set; } = 300f;
    private bool doubleJump = false;
    private bool cancelJump = false;
    public float Acceleration { get; set; } = 3f;
    public float StartingYAtJump { get; set; }
    public float CancelJumpThreshold = 0.5f;

    public override void HandleMove(InputAction.CallbackContext ctx, GameObject player)
    {
        moveValue = ctx.ReadValue<Vector2>();
    }
    public override void HandleJump(InputAction.CallbackContext ctx, GameObject player)
    {
        if(ctx.performed)
        {
            playerAudio.PlayDoubleJumpSound();
            doubleJump = true;
        }
        if(ctx.canceled)
        {
            cancelJump = true;
        }
    }
    public override void HandleAttack(InputAction.CallbackContext ctx, GameObject player) { }

    public override void PretendFixedUpdate() {
        if (moveValue.sqrMagnitude != 0)
        {
            Vector3 accelerationForce = new Vector3(moveValue.x, 0, moveValue.y) * Acceleration;
            playerBody.AddForce(accelerationForce, ForceMode.Force);
        } 
        else
        {
            playerBody.velocity = new Vector3(0, playerBody.velocity.y, 0);
        }

        if (doubleJump)
        {
            doubleJump = false;
            playerBody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
            context.SetPlayerState("DOUBLEJUMP");
            return;
        }

        if (cancelJump && (player.transform.position.y - StartingYAtJump) >= CancelJumpThreshold)
        {
            cancelJump = false;
            playerBody.velocity = new Vector3(playerBody.velocity.x, 0, playerBody.velocity.z);
            return;
        }
    }

    public override void PretendUpdate()
    {
        if (playerBody.velocity.y < 0f)
            animator.SetFloat("VerticalSpeed", -0.5f);
        else animator.SetFloat("VerticalSpeed", 0.5f);
    }

    public JumpPlayerState(GameObject player, PlayerInput context, Animator animator, PlayerAudio playerAudio) : base(player, context, animator, playerAudio) { }
}
