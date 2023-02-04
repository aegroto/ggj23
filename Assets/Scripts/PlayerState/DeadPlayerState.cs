using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeadPlayerState : AbstractPlayerState
{
    public override void HandleMove(InputAction.CallbackContext ctx, GameObject player) {
        moveValue = Vector2.zero;
    }
    public override void HandleJump(InputAction.CallbackContext ctx, GameObject player) { }

    public override void HandleAttack(InputAction.CallbackContext ctx, GameObject player) { }

    
    public override void PretendFixedUpdate() {
        animator.SetTrigger("Dead");
        
    }

    public override void PretendUpdate() { }

    public DeadPlayerState(GameObject player, PlayerInput context, Animator animator) : base(player, context, animator) { }
}
