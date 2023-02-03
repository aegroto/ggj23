using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

interface IPlayerState
{
    void HandleMove(InputAction.CallbackContext ctx, GameObject player);
    void HandleJump(InputAction.CallbackContext ctx, GameObject player);
    void HandleAttack(InputAction.CallbackContext ctx, GameObject player);
    void PretendFixedUpdate();
    void PretendUpdate();
}
