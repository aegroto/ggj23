using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundedPlayerState : AbstractPlayerState
{
    public float Speed { get; set; } = 8f;
    public float MaxMovementForce { get; set; } = 4f;
    public float JumpForce { get; set; } = 6f;
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
    public override void HandleAttack(InputAction.CallbackContext ctx, GameObject player) { }


    //Metodo da richiamare manualmente nel FixedUpdate della classe context PlayerInput
    public override void PretendFixedUpdate() { 
        Vector3 currentVelocity = playerBody.velocity;

        GameObject camera = GameObject.Find("Main Camera");
        Vector3 cameraAngles = camera.transform.rotation.eulerAngles;

        float angle = cameraAngles.y; 
        Vector2 rotatedMoveValue = Quaternion.Euler(0, 0, -angle) * moveValue;

        Vector3 targetVelocity = new Vector3(rotatedMoveValue.x, 0, rotatedMoveValue.y) * Speed;

        targetVelocity = player.transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = targetVelocity - currentVelocity;
        Vector3.ClampMagnitude(velocityChange, MaxMovementForce);

        playerBody.AddForce(velocityChange, ForceMode.VelocityChange);

        if (jump)
        {
            jump = false;
            playerBody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
            context.SetPlayerState("JUMP");
        }
    }

    public GroundedPlayerState(GameObject player, PlayerInput context) : base(player, context) { }
}
