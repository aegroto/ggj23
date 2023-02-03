using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StunnedPlayerState : AbstractPlayerState
{
    override public void HandleMove(InputAction.CallbackContext ctx, GameObject player) {
        moveValue = ctx.ReadValue<Vector2>();
    }
    override public void HandleJump(InputAction.CallbackContext ctx, GameObject player) { }
    override public void HandleAttack(InputAction.CallbackContext ctx, GameObject player) { }
    override public void PretendFixedUpdate() { }
    public StunnedPlayerState(GameObject player, PlayerInput context) : base(player, context) { }
}
