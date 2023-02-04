using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpPlayerState : AbstractPlayerState
{
    public float JumpForce { get; set; } = 4f;
    private bool doubleJump = false;
    public float Acceleration { get; set; } = 3f;

    public override void HandleMove(InputAction.CallbackContext ctx, GameObject player)
    {
        moveValue = ctx.ReadValue<Vector2>();
    }
    public override void HandleJump(InputAction.CallbackContext ctx, GameObject player)
    {
        if(ctx.performed)
        {
            doubleJump = true;
        }
    }
    public override void HandleAttack(InputAction.CallbackContext ctx, GameObject player) { }

    public override void PretendFixedUpdate() {
        Vector3 accelerationForce = new Vector3(moveValue.x, 0, moveValue.y) * Acceleration;
        playerBody.AddForce(accelerationForce, ForceMode.Force);

        if (doubleJump)
        {
            doubleJump = false;
            playerBody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
            context.SetPlayerState("DOUBLEJUMP");
        }
    }

    public override void PretendUpdate()
    {
        if (playerBody.velocity.y < 0f)
            animator.SetFloat("VerticalSpeed", 0f);
        else animator.SetFloat("VerticalSpeed", 0.5f);
    }

    public JumpPlayerState(GameObject player, PlayerInput context, Animator animator) : base(player, context, animator) { }
}
