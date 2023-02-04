using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundedPlayerState : AbstractPlayerState
{
    public float Speed { get; set; } = 8f;
    public float MaxMovementForce { get; set; } = 4f;
    public float JumpForce { get; set; } = 12f;
    private bool jump = false;
    
    public override void HandleMove(InputAction.CallbackContext ctx, GameObject player) {
        moveValue = ctx.ReadValue<Vector2>();
    }

    public override void HandleJump(InputAction.CallbackContext ctx, GameObject player) {
        if (ctx.performed)
        {
            jump = true;
        }
    }

    public override void HandleAttack(InputAction.CallbackContext ctx, GameObject player) {
        if (ctx.performed) {
            player.GetComponentInChildren<MeshCollider>().enabled = true;
            animator.SetTrigger("Attack");
        }
    }

    //Metodo da richiamare manualmente nel FixedUpdate della classe context PlayerInput
    public override void PretendFixedUpdate() { 
        Vector3 currentVelocity = playerBody.velocity;

        Vector3 targetVelocity = new Vector3(moveValue.x, 0, moveValue.y) * Speed;

        if(moveValue.magnitude > 0) {
            GameObject camera = GameObject.Find("Main Camera");
            Vector3 cameraAngles = camera.transform.rotation.eulerAngles;
            Quaternion targetRotation = Quaternion.Euler(0, cameraAngles.y, 0);
            playerBody.MoveRotation(Quaternion.Slerp(playerBody.rotation, targetRotation, 0.1f));
        }

        targetVelocity = player.transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = targetVelocity - currentVelocity;
        velocityChange.y = 0;
        Vector3.ClampMagnitude(velocityChange, MaxMovementForce);

        playerBody.AddForce(velocityChange, ForceMode.VelocityChange);

        if (jump)
        {
            jump = false;
            playerBody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
            context.SetPlayerState("JUMP");
        }
    }



    public override void PretendUpdate()
    {
        if (moveValue.sqrMagnitude != 0f)
        {
            animator.SetFloat("Speed", 1f);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }

        
    }

    public GroundedPlayerState(GameObject player, PlayerInput context, Animator animator) : base(player, context, animator) { }
}
