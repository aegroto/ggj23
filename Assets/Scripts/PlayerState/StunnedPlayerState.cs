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
    public override void PretendUpdate() { }
    public StunnedPlayerState(GameObject player, PlayerInput context, Animator animator, PlayerAudio playerAudio) : base(player, context, animator, playerAudio) { }
}
