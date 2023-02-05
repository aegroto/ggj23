using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AbstractPlayerState : IPlayerState
{
    protected GameObject player;
    protected Rigidbody playerBody;
    protected PlayerInput context;
    protected Vector2 moveValue;
    protected Animator animator;
    protected PlayerAudio playerAudio;
    
    public void SetMoveValue(Vector2 moveValue)
    {
        this.moveValue = moveValue;
    }

    public Vector2 GetMoveValue() { return this.moveValue; }

    abstract public void HandleMove(InputAction.CallbackContext ctx, GameObject player);
    abstract public void HandleJump(InputAction.CallbackContext ctx, GameObject player);
    abstract public void HandleAttack(InputAction.CallbackContext ctx, GameObject player);
    abstract public void PretendFixedUpdate();
    abstract public void PretendUpdate();
    public AbstractPlayerState(GameObject player, PlayerInput context, Animator animator, PlayerAudio playerAudio)
    {
        this.player = player;
        this.playerBody = player.GetComponent<Rigidbody>();
        this.context = context;
        this.animator = animator;
        this.playerAudio = playerAudio;
    }
}
