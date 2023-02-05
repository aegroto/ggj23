using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeadPlayerState : AbstractPlayerState
{
    override public void HandleMove(InputAction.CallbackContext ctx, GameObject player) { }
    override public void HandleJump(InputAction.CallbackContext ctx, GameObject player) { }
    override public void HandleAttack(InputAction.CallbackContext ctx, GameObject player) { }
    override public void PretendFixedUpdate() { }
    override public void PretendUpdate() { }

    public DeadPlayerState(GameObject player, PlayerInput context, Animator animator, PlayerAudio playerAudio) : base(player, context, animator, playerAudio) { }
}
