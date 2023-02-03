using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoubleJumpPlayerState : AbstractPlayerState
{
    public float Acceleration { get; set; } = 2f;
    public override void HandleMove(InputAction.CallbackContext ctx, GameObject player)
    {
        moveValue = ctx.ReadValue<Vector2>();
    }
    public override void HandleJump(InputAction.CallbackContext ctx, GameObject player)
    {
    }
    public override void HandleAttack(InputAction.CallbackContext ctx, GameObject player) { }

    public override void PretendFixedUpdate()
    {
        Vector3 accelerationForce = new Vector3(moveValue.x, 0, moveValue.y) * Acceleration;
        playerBody.AddForce(accelerationForce, ForceMode.Force);
    }

    public DoubleJumpPlayerState(GameObject player, PlayerInput context) : base(player, context) { }
}
