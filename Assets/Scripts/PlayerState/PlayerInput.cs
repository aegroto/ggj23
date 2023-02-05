using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerAudio playerAudio;
    [SerializeField] private Animator animator;
    private AbstractPlayerState playerState;
    private AbstractPlayerState[] playerStatesPool;

    private void Awake()
    {
        // DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        playerStatesPool = new AbstractPlayerState[5];
        playerStatesPool[0] = new GroundedPlayerState(gameObject, this, animator, playerAudio);
        playerStatesPool[1] = new JumpPlayerState(gameObject, this, animator, playerAudio);
        playerStatesPool[2] = new DoubleJumpPlayerState(gameObject, this, animator, playerAudio);
        playerStatesPool[3] = new StunnedPlayerState(gameObject, this, animator, playerAudio);
        playerStatesPool[4] = new DeadPlayerState(gameObject, this, animator, playerAudio);
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

    private void Update()
    {
        playerState.PretendUpdate();
    }

    public void SetPlayerState(string stateName)
    {
        switch (stateName)
        {
            
            case "GROUNDED":
                animator.SetFloat("VerticalSpeed", 0);
                playerStatesPool[0].SetMoveValue(playerState.GetMoveValue());
                playerState = playerStatesPool[0];
                break;
            case "JUMP":
                playerStatesPool[1].SetMoveValue(playerState.GetMoveValue());
                playerState = playerStatesPool[1];
                ((JumpPlayerState)playerState).StartingYAtJump = gameObject.transform.position.y;
                break;
            case "DOUBLEJUMP":
                playerStatesPool[2].SetMoveValue(playerState.GetMoveValue());
                playerState = playerStatesPool[2];
                ((DoubleJumpPlayerState)playerState).StartingYAtJump = gameObject.transform.position.y;
                break;
            case "STUNNED":
                gameObject.GetComponent <Rigidbody>().velocity = Vector3.zero;
                playerStatesPool[3].SetMoveValue(playerState.GetMoveValue());
                playerState = playerStatesPool[3];
                break;
            case "DEAD":
                playerState = playerStatesPool[4];
                break;
        }
    }
}
