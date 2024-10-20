using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerInputHandler : MonoBehaviour
{

    private PlayerMovement mover;

    void Start()
    {
        mover = GetComponent<PlayerMovement>();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mover._playerDir = context.ReadValue<float>().Normalize();
        }
        else if (context.canceled)
        {
            mover._playerDir = 0;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("JUMP PLAYER INPUT IS OK");
        if (context.performed)
        {
            mover.HandleJump();
        }
    }

    public void OnAttack1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("IS USING ATTACK 1");
        }
    }

    public void OnAttack2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("IS USING ATTACK 2");
        }
    }
}
