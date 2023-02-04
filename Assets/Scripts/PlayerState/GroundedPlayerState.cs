using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundedPlayerState : AbstractPlayerState
{
    public float Speed { get; set; } = 10f;
    public float MaxMovementForce { get; set; } = 80f;
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

        GameObject camera = GameObject.Find("Main Camera");

        if(moveValue.magnitude > 0) {
            //Debug.Log(moveValue);
            Vector3 cameraAngles = camera.transform.rotation.eulerAngles;

            float cameraFactor = cameraAngles.y * Mathf.Deg2Rad;
            float xFactor = Mathf.Asin(moveValue.x);
            float yFactor = 0.0f;
            if(Mathf.Abs(moveValue.y) > 0.0f)
                yFactor = Mathf.Sign(moveValue.y) * Mathf.Acos(moveValue.y);
            float angle = cameraFactor + xFactor + yFactor;

            Quaternion targetRotation = Quaternion.Euler(0, angle * Mathf.Rad2Deg, 0);
            Transform meshTransform = playerBody.transform.Find("PlayerMesh").transform;
            meshTransform.rotation = 
                Quaternion.Slerp(meshTransform.rotation, targetRotation, 0.1f);
            Debug.Log(meshTransform.rotation);
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
