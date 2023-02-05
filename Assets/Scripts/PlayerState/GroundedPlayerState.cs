using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundedPlayerState : AbstractPlayerState
{
    public float Speed { get; set; } = 20f;
    public float MaxMovementForce { get; set; } = 30f;
    public float JumpForce { get; set; } = 400f;    
    private bool jump = false;
    
    public override void HandleMove(InputAction.CallbackContext ctx, GameObject player) {
        moveValue = ctx.ReadValue<Vector2>();
    }

    public override void HandleJump(InputAction.CallbackContext ctx, GameObject player) {
        if (ctx.performed)
        {
            //context refers to the States context, not the action context
            playerAudio.StopPlayingFootsteps();
            playerAudio.PlayJumpSound();
            jump = true;
        }
    }
    public override void HandleAttack(InputAction.CallbackContext ctx, GameObject player) {
        if (ctx.performed)
        {
            animator.SetTrigger("Attack");
        }
    }


    //Metodo da richiamare manualmente nel FixedUpdate della classe context PlayerInput
    public override void PretendFixedUpdate() { 
        Vector3 currentVelocity = playerBody.velocity;

        GameObject camera = GameObject.Find("Main Camera");

        if(moveValue.magnitude > 0) {
            Vector3 cameraAngles = camera.transform.rotation.eulerAngles;

            float cameraFactor = cameraAngles.y * Mathf.Deg2Rad;
            float xFactor = Mathf.Asin(moveValue.x);
            float yFactor = 0.0f;
            if(Mathf.Abs(moveValue.y) > 0.0f)
                yFactor = Mathf.Sign(moveValue.y) * Mathf.Acos(moveValue.y);
            float angle = cameraFactor + xFactor + yFactor;

            Transform meshTransform = playerBody.transform.Find("PlayerPivot").transform;
            Quaternion targetRotation = Quaternion.Euler(meshTransform.rotation.x, angle * Mathf.Rad2Deg, meshTransform.rotation.z);
            meshTransform.rotation = 
                Quaternion.Slerp(meshTransform.rotation, targetRotation, 0.1f);
            playerAudio.PlayFootsteps();
        }

        Vector3 targetVelocity = new Vector3(moveValue.x, 0, moveValue.y) * Speed;
        targetVelocity = camera.transform.TransformDirection(targetVelocity);
        targetVelocity = Vector3.ClampMagnitude(targetVelocity, MaxMovementForce);
        targetVelocity.y = currentVelocity.y;

        playerBody.velocity = targetVelocity;

        if (jump)
        {
            jump = false;
            playerBody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            animator.SetBool("TouchedGround", false);
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

    public GroundedPlayerState(GameObject player, PlayerInput context, Animator animator, PlayerAudio playerAudio) : base(player, context, animator, playerAudio) { }
}
