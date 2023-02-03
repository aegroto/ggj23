using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private AbstractPlayerState playerState;
    private AbstractPlayerState[] playerStatesPool;

    private void Start()
    {
        playerStatesPool = new AbstractPlayerState[3];
        playerStatesPool[0] = new GroundedPlayerState(gameObject, this);
        playerStatesPool[1] = new JumpPlayerState(gameObject, this);
        playerStatesPool[2] = new DoubleJumpPlayerState(gameObject, this);
        playerState = playerStatesPool[0];
    }

    public void OnMove(InputAction.CallbackContext ctx) {
        playerState.HandleMove(ctx, gameObject);
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        playerState.HandleJump(ctx, gameObject);
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        playerState.HandleAttack(ctx, gameObject);
    }

    private void FixedUpdate()
    {
        playerState.PretendFixedUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            this.SetPlayerState("GROUNDED");
        }
    }
    public void SetPlayerState(string stateName)
    {
        switch (stateName)
        {
            
            case "GROUNDED":
                playerStatesPool[0].SetMoveValue(playerState.GetMoveValue());
                playerState = playerStatesPool[0];
                break;
            case "JUMP":
                playerStatesPool[1].SetMoveValue(playerState.GetMoveValue());
                playerState = playerStatesPool[1];
                break;
            case "DOUBLEJUMP":
                playerStatesPool[2].SetMoveValue(playerState.GetMoveValue());
                playerState = playerStatesPool[2];
                break;
        }
    }

}
